using UnityEngine;

//Add other things like first aid count, dog food and so on.

public class Player : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar = null;

    public static Player Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        LoadSave();
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

    public void LoadSave()
    {
        _coin = SaveSystem.LoadPlayer()._coin;
        _healthCount = SaveSystem.LoadPlayer()._healthCount;
        _maximumHealth = SaveSystem.LoadPlayer()._maximumHealth;
        _level = SaveSystem.LoadPlayer()._level;
    }

    private void TakeDamage(int damage)
    {
        _healthBar.DecreaseHealth(damage);
    }

    private void GetHeal(int heal)
    {
        _healthBar.IncreaseHealth(heal);
    }
}