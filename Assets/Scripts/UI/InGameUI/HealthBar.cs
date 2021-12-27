using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider = null;
    [SerializeField] private Image _filler = null;
    [SerializeField] private Gradient _normalGradient = null;
    [SerializeField] private Gradient _poisonedGradient = null;

    private float _maximumHealth = 100;
    private float _currentHealth = 100;

    private bool _isPoisoned = false;

    private void Start()
    {
        RegisterToEvents();

        _maximumHealth = Player.Instance.MaximumHealth;
        _currentHealth = _maximumHealth;
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }

    private void RegisterToEvents()
    {
        Player.Instance.OnDamage += DecreaseHealth;
        Player.Instance.OnHeal += IncreaseHealth;
    }

    private void UnregisterFromEvents()
    {
        Player.Instance.OnDamage -= DecreaseHealth;
        Player.Instance.OnHeal -= IncreaseHealth;
    }

    private void IncreaseHealth(int health)
    {
        if (_currentHealth + health > _maximumHealth)
            _currentHealth = _maximumHealth;
        else
            _currentHealth += health;

        if (_isPoisoned)
            _filler.color = _poisonedGradient.Evaluate(_currentHealth / _maximumHealth);
        else
            _filler.color = _normalGradient.Evaluate(_currentHealth / _maximumHealth);

        _slider.value = _currentHealth;
    }

    private void DecreaseHealth(int hit)
    {
        if (_currentHealth > hit)
            _currentHealth -= hit;
        else
            _currentHealth = 0;

        if (_isPoisoned)
            _filler.color = _poisonedGradient.Evaluate(_currentHealth / _maximumHealth);
        else
            _filler.color = _normalGradient.Evaluate(_currentHealth / _maximumHealth);

        _slider.value = _currentHealth;
    }
}