using System.Collections;
using UnityEngine;
using DG.Tweening;

//Add click sound.
//Limit flashlight control depends on day or night.

public class Player_Flashlight : MonoBehaviour
{
    [SerializeField] private float _batteryTime = 60f;
    [SerializeField] private Light _flashlightLight = null;
    [SerializeField] private Transform _flashlight = null;
    [SerializeField] private float _rotationDuration = 1f;

    private bool _isFlashlightOn = false;
    private bool _isEnoughBattery = true;

    private IEnumerator _timerRoutine = null;
    private IEnumerator _flashlightAnimRoutine = null;
    private IEnumerator _flashlightLowBatteryAnim = null;

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
        _flashlightAnimRoutine = FlashlightAnimRoutine();
        StartCoroutine(_flashlightAnimRoutine);
    }

    private IEnumerator TimerRoutine()
    {
        float remainTime = _batteryTime;

        while (remainTime > 0)
        {
            remainTime -= Time.deltaTime;
            yield return null;
        }

        _isEnoughBattery = false;

        _flashlightLowBatteryAnim = FlashlightLowBatteryAnim();
        StartCoroutine(_flashlightLowBatteryAnim);
    }

    private IEnumerator FlashlightAnimRoutine()
    {
        if (_isFlashlightOn)
        {
            _flashlight.DORotate(new Vector3(90f, _flashlight.rotation.y, _flashlight.rotation.z), _rotationDuration);

            if (_timerRoutine != null)
                StopCoroutine(_timerRoutine);

            _flashlightLight.enabled = false;

            _isFlashlightOn = false;
        }

        else if (!_isFlashlightOn && _isEnoughBattery)
        {
            _flashlight.DORotate(new Vector3(0f, _flashlight.rotation.y, _flashlight.rotation.z), _rotationDuration);

            _flashlightLight.enabled = true;

            _isFlashlightOn = true;

            _timerRoutine = TimerRoutine();
            StartCoroutine(_timerRoutine);
        }

        yield return null;
    }

    private IEnumerator FlashlightLowBatteryAnim()
    {
        _flashlightLight.enabled = false;

        yield return new WaitForSeconds(0.5f);

        _flashlightLight.enabled = true;

        yield return new WaitForSeconds(0.5f);

        _flashlightLight.enabled = false;

        yield return new WaitForSeconds(0.5f);

        _flashlightLight.enabled = true;

        yield return new WaitForSeconds(0.5f);

        _flashlightLight.enabled = false;

        ToggleFlashlight();
    }
}