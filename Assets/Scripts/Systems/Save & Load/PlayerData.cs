//Add aid kit, dog food and so on.

[System.Serializable]
public class PlayerData
{
    public int _level;
    public int _healthCount;
    public int _coin;
    public int _maximumHealth;
    public int _boneCount;
    public int _foodCount;

    public PlayerData(Player player)
    {
        _level = player.Level;
        _healthCount = player.HealthCount;
        _coin = player.Coin;
        _maximumHealth = player.MaximumHealth;
        _boneCount = player.BoneCount;
        _foodCount = player.FoodCount;
    }
}