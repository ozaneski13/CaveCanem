using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//Add click sound.
//Limit flashlight control depends on day or night.

public class Player_Flashlight : MonoBehaviour
{
    [SerializeField] private Light _flashlightLight = null;
    [SerializeField] private float _batteryTime = 60f;

    private IEnumerator _timerRoutine = null;
    private IEnumerator _flashlightAnimRoutine = null;
    private IEnumerator _flashlightLowBatteryAnim = null;

    private Player _player = null;

    private bool _isFlashlightOn = false;
    private bool _isEnoughBattery = true;
    private bool _canUse = true;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "TutorialLevel")
            _canUse = false;
    }

    private void Start()
    {
        _player = Player.Instance;

        RegisterToEvents();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _canUse)
            ToggleFlashlight();
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();

        StopAllCoroutines();
    }

    private void RegisterToEvents()
    {
        _player.OnDeath += OnDeath;
    }

    private void UnregisterFromEvents()
    {
        if (_player != null)
            _player.OnDeath -= OnDeath;
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
            if (_timerRoutine != null)
                StopCoroutine(_timerRoutine);

            yield return new WaitForSeconds(0.5f);

            _flashlightLight.enabled = false;

            _isFlashlightOn = false;
        }

        else if (!_isFlashlightOn && _isEnoughBattery)
        {
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

    public bool CanUse() => _canUse = true;

    private void OnDeath() => _canUse = false;
}