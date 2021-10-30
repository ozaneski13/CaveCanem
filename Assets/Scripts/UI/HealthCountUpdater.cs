using UnityEngine;
using UnityEngine.UI;

public class HealthCountUpdater : MonoBehaviour
{
    [SerializeField] private Text _healthCount = null;

    private int _currentHealthCount = 0;

    private void Start()
    {
        _currentHealthCount = Player.Instance.HealthCount;
        _healthCount.text = _currentHealthCount.ToString();
    }

    private void DecreaseHealthCount()
    {
        if (_currentHealthCount - 1 < 0)
        {
            _currentHealthCount = 0;
            //todo end the game.
        }

        else
        {
            _currentHealthCount--;
            _healthCount.text = _currentHealthCount.ToString();
        }
    }
}