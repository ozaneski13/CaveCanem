using UnityEngine;

public class Player : MonoBehaviour
{
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

    private int _health;
    public int Health
    {
        get { return _health; }
        set { _health = value; }
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
        _health = SaveSystem.LoadPlayer()._health;
        _level = SaveSystem.LoadPlayer()._level;
    }
}