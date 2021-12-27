using System;
using UnityEngine;

public class MarketBorder : MonoBehaviour
{
    public Action OnBorderPassed;

    private Player _player = null;

    private bool _isPassed = false;

    private void Start()
    {
        _player = Player.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _player.gameObject || _isPassed)
            return;

        _isPassed = true;
        OnBorderPassed?.Invoke();
    }
}