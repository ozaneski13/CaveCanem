using System.Collections;
using UnityEngine;

//Add click sound.
//Limit flashlight control depends on day or night.

public class Player_Flashlight : MonoBehaviour
{
    [SerializeField] private float _batteryTime = 60f;
    [SerializeField] private Light _flashlight = null;

    private bool _isFlashlightOn = false;
    private bool _isEnoughBattery = true;

    private float _remainTime = 0f;

    private IEnumerator _timerRoutine = null;

    private void Awake()
    {
        _remainTime = _batteryTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            ToggleFlashlight();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void ToggleFlashlight()
    {
        if (_isFlashlightOn)
        {
            _flashlight.enabled = false;

            if (_timerRoutine != null)
                StopCoroutine(_timerRoutine);

            _isFlashlightOn = false;
        }

        else if (!_isFlashlightOn && _isEnoughBattery)
        {
            _flashlight.enabled = true;

            _timerRoutine = TimerRoutine();
            StartCoroutine(_timerRoutine);

            _isFlashlightOn = true;
        }
    }

    private IEnumerator TimerRoutine()
    {
        while (_remainTime > 0)
        {
            _remainTime -= Time.deltaTime;
            yield return null;
        }

        _isEnoughBattery = false;

        ToggleFlashlight();
    }
}