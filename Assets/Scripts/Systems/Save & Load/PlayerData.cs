[System.Serializable]
public class PlayerData
{
    public int _level;
    public int _health;
    public int _coin;

    public PlayerData(Player player)
    {
        _level = player.Level;
        _health = player.Health;
        _coin = player.Coin;
    }
}