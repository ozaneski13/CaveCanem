using UnityEngine;
using UnityEngine.UI;

public class UICountsUpdater : MonoBehaviour
{
    [SerializeField] private CollectableListener _collectableListener = null;

    [SerializeField] private Text _healthCount = null;
    [SerializeField] private Text _coinCount = null;

    private int _currentHealthCount = 0;
    private int _currentCoinCount = 0;

    private void Awake()
    {
        RegisterToEvents();
    }

    private void Start()
    {
        _currentHealthCount = Player.Instance.HealthCount;
        _currentCoinCount = Player.Instance.Coin;

        _healthCount.text = _currentHealthCount.ToString();
        _coinCount.text = _currentCoinCount.ToString();
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }

    private void RegisterToEvents()
    {
        _collectableListener.OnCollectableCollected += IncreaseCoinCount;
    }

    private void UnregisterFromEvents()
    {
        _collectableListener.OnCollectableCollected -= IncreaseCoinCount;
    }

    private void IncreaseHealthCount()
    {
        _currentHealthCount++;
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

    private void IncreaseCoinCount()
    {
        _currentCoinCount++;
        _coinCount.text = _currentCoinCount.ToString();
    }

    private void DecreaseCoinCount()
    {
        if (_currentCoinCount - 1 < 0)
        {
            _currentCoinCount = 0;
            //todo market place.
        }

        else
        {
            _currentCoinCount--;
            _coinCount.text = _currentCoinCount.ToString();
        }
    }
}