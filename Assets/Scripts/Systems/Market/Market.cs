using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Market : MonoBehaviour
{
    [SerializeField] private MarketBorder _marketBorder = null;

    [SerializeField] private GameObject _marketUI = null;

    [SerializeField] private Text[] _priceTexts = null;

    [SerializeField] private int _foodPrice = 0;
    [SerializeField] private int _bonePrice = 0;
    [SerializeField] private int _healthPrice = 0;

    public Action<int> OnFoodBought;
    public Action<int> OnBoneBought;
    public Action<int> OnHealthBought;

    private Player _player = null;

    private int _coins;

    private bool _isPassed = false;

    private void Awake()
    {
        UpdatePrices();

        RegisterToEvents();
    }

    private void Start()
    {
        _player = Player.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _player.gameObject || _isPassed)
            return;

        _coins = _player.Coin;
        _marketUI.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != _player.gameObject || _isPassed)
            return;

        _marketUI.SetActive(false);
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }

    private void UpdatePrices()
    {
        _priceTexts[0].text = _foodPrice.ToString();
        _priceTexts[1].text = _bonePrice.ToString();
        _priceTexts[2].text = _healthPrice.ToString();
    }

    private void RegisterToEvents()
    {
        _marketBorder.OnBorderPassed += Passed;
    }

    private void UnregisterFromEvents()
    {
        _marketBorder.OnBorderPassed -= Passed;
    }

    public void BuyFood()
    {
        _coins = _player.Coin;

        if (_coins < _foodPrice)
            return;

        _player.Coin -= _foodPrice;
        _coins -= _foodPrice;
        OnFoodBought?.Invoke(_foodPrice);
    }

    public void BuyBone()
    {
        _coins = _player.Coin;

        if (_coins < _bonePrice)
            return;

        _player.Coin -= _bonePrice;
        _coins -= _bonePrice;
        OnBoneBought?.Invoke(_bonePrice);
    }

    public void BuyHealth()
    {
        _coins = _player.Coin;

        if (_coins < _healthPrice)
            return;

        _player.Coin -= _healthPrice;
        _coins -= _healthPrice;
        OnHealthBought?.Invoke(_healthPrice);
    }

    private void Passed() => _isPassed = true;
}