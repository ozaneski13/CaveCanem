using System;
using UnityEngine;

//Add other things like first aid count, dog food and so on.

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator = null;

    public static Player Instance;

    public Action<int> OnDamage;
    public Action<int> OnHeal;

    public Action OnKill;

    #region Singleton
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        LoadSave();
    }
    #endregion

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

    public void LoadSave()
    {
        _coin = SaveSystem.LoadPlayer()._coin;
        _healthCount = SaveSystem.LoadPlayer()._healthCount;
        _maximumHealth = SaveSystem.LoadPlayer()._maximumHealth;
        _level = SaveSystem.LoadPlayer()._level;

        _currentMaximumHealth = _maximumHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_currentMaximumHealth > damage)
            _currentMaximumHealth -= damage;
        else
        {
            if (HealthCount > 0)
            {
                _currentMaximumHealth = _maximumHealth;
                _healthCount--;

                OnHeal?.Invoke(_maximumHealth);
            }

            else
            {
                _currentMaximumHealth = 0;

                _animator.SetBool("Die", true);

                OnKill?.Invoke();
            }
        }

        OnDamage?.Invoke(damage);
    }

    private void GetHeal(int heal)
    {
        OnHeal?.Invoke(heal);
    }
}