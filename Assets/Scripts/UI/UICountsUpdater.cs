using UnityEngine;
using UnityEngine.UI;

public class UICountsUpdater : MonoBehaviour
{
    [SerializeField] private CollectableListener _collectableListener = null;

    [SerializeField] private Text _healthCount = null;
    [SerializeField] private Text _coinCount = null;
    [SerializeField] private Text _boneCount = null;
    [SerializeField] private Text _foodCount = null;

    private int _currentHealthCount = 0;
    private int _currentCoinCount = 0;
    private int _currentBoneCount = 0;
    private int _currentFoodCount = 0;

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
        _currentBoneCount = Player.Instance.BoneCount;
        _currentFoodCount = Player.Instance.FoodCount;

        _healthCount.text = _currentHealthCount.ToString();
        _coinCount.text = _currentCoinCount.ToString();
        _boneCount.text = _currentBoneCount.ToString();
        _foodCount.text = _currentFoodCount.ToString();

        _player.OnHealthCountDecreased += DecreaseHealthCount;
        _player.PlayerFeed.OnBoneUsed += DecreaseBoneCount;
        _player.PlayerFeed.OnFoodUsed += DecreaseFoodCount;
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
        _collectableListener.OnFoodCollected += IncreaseFoodCount;
    }

    private void UnregisterFromEvents()
    {
        _collectableListener.OnCoinCollected -= IncreaseCoinCount;
        _collectableListener.OnBoneCollected -= IncreaseBoneCount;
        _collectableListener.OnHealthCollected -= IncreaseHealthCount;
        _collectableListener.OnFoodCollected -= IncreaseFoodCount;

        _player.OnHealthCountDecreased -= DecreaseHealthCount;
        _player.PlayerFeed.OnBoneUsed -= DecreaseBoneCount;
        _player.PlayerFeed.OnFoodUsed -= DecreaseFoodCount;
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
        _boneCount.text = _currentBoneCount.ToString();
    }

    private void IncreaseFoodCount()
    {
        _currentFoodCount++;
        _foodCount.text = _currentFoodCount.ToString();
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
            _boneCount.text = _currentBoneCount.ToString();
        }
    }

    private void DecreaseFoodCount()
    {
        if (_currentFoodCount - 1 < 0)
        {
            _currentFoodCount = 0;
            //todo market place.
        }

        else
        {
            _currentFoodCount--;
            _foodCount.text = _currentFoodCount.ToString();
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