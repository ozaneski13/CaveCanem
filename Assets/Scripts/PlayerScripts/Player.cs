using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CollectableListener _collectableListener = null;

    [SerializeField] private Player_Feed _playerFeed = null;

    [SerializeField] private Animator _animator = null;

    [SerializeField] private ParticleSystem _particleSystem = null;

    [SerializeField] private float _poisonTimer = 4f;

    [SerializeField] private float _poisonThickSpeed = 1f;

    public Action<int, bool> OnDamage;
    public Action<int> OnHeal;

    public Action OnDeath;
    public Action OnCured;
    public Action OnHealthCountDecreased;

    private SFXManager _sfxManager = null;

    private IEnumerator _poisonRoutine = null;

    private bool _isDead = false;
    private bool _isPoisoned = false;

    private int _currentMaximumHealth;

    private float _timer = 0f;

    public Player_Feed PlayerFeed
    {
        get { return _playerFeed; }
        set { _playerFeed = value; }
    }

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

    public static Player Instance;
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
        _sfxManager = SFXManager.Instance;

        RegisterToEvents();
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();

        if (_poisonRoutine != null)
            StopCoroutine(_poisonRoutine);
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

    public void TakeDamage(int damage, bool isPoisoned)
    {
        if (_isDead)
            return;

        _particleSystem.Play();

        AudioSource scream = _sfxManager.GetHumanScream();

        if (!scream.isPlaying)
            scream.Play();

        if (_currentMaximumHealth > damage)
        {
            if (isPoisoned)
            {
                GetPoisoned(damage);

                return;
            }

            _currentMaximumHealth -= damage;
        }

        else
        {
            if (HealthCount > 0)
            {
                _currentMaximumHealth = _maximumHealth;
                _healthCount--;

                OnHealthCountDecreased?.Invoke();
                OnHeal?.Invoke(_maximumHealth);

                if (isPoisoned)
                {
                    GetPoisoned(damage);
                    
                    return;
                }
            }

            else
            {
                _currentMaximumHealth = 0;

                _animator.SetBool("Die", true);

                _isDead = true;

                OnDeath?.Invoke();
            }
        }

        OnDamage?.Invoke(damage, isPoisoned);
    }

    private void GetCoin() => Coin++;

    private void GetHealth() => HealthCount++;

    private void GetPoisoned(int damage)
    {
        if (_isPoisoned)
            return;

        _poisonRoutine = PoisonRoutine(damage);
        StartCoroutine(_poisonRoutine);
    }

    private IEnumerator PoisonRoutine(int damage)
    {
        _timer = 0f;
        _isPoisoned = true;

        while (_timer < _poisonTimer)
        {
            _timer += Time.deltaTime;

            if (_currentMaximumHealth > damage)
            {
                _currentMaximumHealth -= damage;
                OnDamage?.Invoke(damage, _isPoisoned);
            }

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

            yield return new WaitForSeconds(_poisonThickSpeed);

            _timer += _poisonThickSpeed;
        }
        
        OnCured?.Invoke();
        _isPoisoned = false;
    }
}