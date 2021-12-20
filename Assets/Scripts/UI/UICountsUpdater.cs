using UnityEngine;
using UnityEngine.UI;

public class UICountsUpdater : MonoBehaviour
{
    [SerializeField] private CollectableListener _collectableListener = null;

    [SerializeField] private Text _healthCount = null;
    [SerializeField] private Text _coinCount = null;
    [SerializeField] private Text _boneCount = null;

    private int _currentHealthCount = 0;
    private int _currentCoinCount = 0;
    private int _currentBoneCount = 0;

    private Player _player = null;

    private void Awake()
    {
        RegisterToEvents();
    }

    private void Start()
    {
        _player = Player.Instance;

        _currentHealthCount = Player.Instance.HealthCount;
        _currentCoinCount = Player.Instance.Coin;

        _healthCount.text = _currentHealthCount.ToString();
        _coinCount.text = _currentCoinCount.ToString();

        _player.OnHealthCountDecreased += DecreaseHealthCount;
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }

    private void RegisterToEvents()
    {
        _collectableListener.OnCoinCollected += IncreaseCoinCount;
        _collectableListener.OnBoneCollected += IncreaseBoneCount;
        _collectableListener.OnHealthCollected += IncreaseHealthCount;
    }

    private void UnregisterFromEvents()
    {
        _collectableListener.OnCoinCollected -= IncreaseCoinCount;
        _collectableListener.OnBoneCollected -= IncreaseBoneCount;
        _collectableListener.OnHealthCollected -= IncreaseHealthCount;

        _player.OnHealthCountDecreased -= DecreaseHealthCount;
    }

    private void IncreaseHealthCount()
    {
        _currentHealthCount++;
        _healthCount.text = _currentHealthCount.ToString();
    }

    private void IncreaseCoinCount()
    {
        _currentCoinCount++;
        _coinCount.text = _currentCoinCount.ToString();
    }

    private void IncreaseBoneCount()
    {
        _currentBoneCount++;
        //_boneCount.text = _currentBoneCount.ToString();
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

    private void DecreaseBoneCount()
    {
        if (_currentBoneCount - 1 < 0)
        {
            _currentBoneCount = 0;
            //todo market place.
        }

        else
        {
            _currentBoneCount--;
            //_boneCount.text = _currentBoneCount.ToString();
        }
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