using UnityEngine;

public class DummySaveFile : MonoBehaviour
{
    [SerializeField] private int _coinCount = 0;
    [SerializeField] private int _maximumHealth = 100;
    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private int _healthCount = 0;
    [SerializeField] private int _boneCount = 0;
    [SerializeField] private int _foodCount = 0;

    private void Start()
    {
        Player player = new Player();

        player.Coin = _coinCount;
        player.Level = _currentLevel;
        player.MaximumHealth = _maximumHealth;
        player.HealthCount = _healthCount;
        player.BoneCount = _boneCount;
        player.FoodCount = _foodCount;

        SaveSystem.SavePlayer(player);
    }
}