using UnityEngine;

public class DummySaveFile : MonoBehaviour
{
    [SerializeField] private int _coinCount = 0;
    [SerializeField] private int _maximumHealth = 100;
    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private int _healthCount = 0;

    void Start()
    {
        Player player = new Player();

        player.Coin = _coinCount;
        player.Level = _currentLevel;
        player.MaximumHealth = _maximumHealth;
        player.HealthCount = _healthCount;

        SaveSystem.SavePlayer(player);
    }
}