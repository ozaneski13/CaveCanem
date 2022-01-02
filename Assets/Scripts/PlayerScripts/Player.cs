using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CollectableListener _collectableListener = null;

    [SerializeField] private Player_Feed _playerFeed = null;

    [SerializeField] private Animator _animator = null;

    [SerializeField] private ParticleSystem _particleSystem = null;

    public Player_Feed PlayerFeed
    {
        get { return _playerFeed; }
        set { _playerFeed = value; }
    }

    public static Player Instance;

    public Action<int> OnDamage;
    public Action<int> OnHeal;

    public Action OnDeath;
    public Action OnHealthCountDecreased;

    private bool _isDead = false;

    private int _coin;
    public int Coin
    {
        get { return _coin; }
        set { _coin = value; }
    }

    private int _healthCount;
    public int HealthCount
    {
        get { return _healthCount; }
        set { _healthCount = value; }
    }

    private int _maximumHealth;
    public int MaximumHealth
    {
        get { return _maximumHealth; }
        set { _maximumHealth = value; }
    }

    private int _currentMaximumHealth;

    private int _level;
    public int Level
    {
        get { return _level; }
        set { _level = value; }
    }

    private int _boneCount;
    public int BoneCount
    {
        get { return _boneCount; }
        set { _boneCount = value; }
    }

    private int _foodCount;
    public int FoodCount
    {
        get { return _foodCount; }
        set { _foodCount = value; }
    }

    #region Singleton
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        LoadSave();
    }
    #endregion

    private void Start()
    {
        RegisterToEvents();
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }

    private void RegisterToEvents()
    {
        _collectableListener.OnCoinCollected += GetCoin;
        _collectableListener.OnHealthCollected += GetHealth;
    }

    private void UnregisterFromEvents()
    {
        _collectableListener.OnCoinCollected -= GetCoin;
        _collectableListener.OnHealthCollected -= GetHealth;
    }

    public void LoadSave()
    {
        _coin = SaveSystem.LoadPlayer()._coin;
        _healthCount = SaveSystem.LoadPlayer()._healthCount;
        _maximumHealth = SaveSystem.LoadPlayer()._maximumHealth;
        _level = SaveSystem.LoadPlayer()._level;
        _boneCount = SaveSystem.LoadPlayer()._boneCount;
        _foodCount = SaveSystem.LoadPlayer()._foodCount;

        _currentMaximumHealth = _maximumHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_isDead)
            return;

        _particleSystem.Play();

        if (_currentMaximumHealth > damage)
            _currentMaximumHealth -= damage;
        else
        {
            if (HealthCount > 0)
            {
                _currentMaximumHealth = _maximumHealth;
                _healthCount--;

                OnHealthCountDecreased?.Invoke();
                OnHeal?.Invoke(_maximumHealth);
            }

            else
            {
                _currentMaximumHealth = 0;

                _animator.SetBool("Die", true);

                _isDead = true;

                OnDeath?.Invoke();
            }
        }

        OnDamage?.Invoke(damage);
    }

    private void GetCoin()
    {
        Coin++;
    }

    private void GetHealth()
    {
        HealthCount++;
    }

    /*private void GetHeal(int heal)
    {
        OnHeal?.Invoke(heal);
    }*/
}