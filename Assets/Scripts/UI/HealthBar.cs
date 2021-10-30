using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider = null;
    [SerializeField] private Image _filler = null;
    [SerializeField] private Gradient _gradient = null;

    [SerializeField] private Color _poisonedColor = Color.green;

    private float _maximumHealth = 100;
    private float _currentHealth = 100;

    private bool _isPoisoned = false;

    private void Start()
    {
        _maximumHealth= Player.Instance.MaximumHealth;
        _currentHealth = _maximumHealth;
    }

    public void IncreaseHealth(int health)
    {
        if (_currentHealth + health > _maximumHealth)
            _currentHealth = _maximumHealth;
        else
            _currentHealth += health;

        if (_isPoisoned)
            _filler.color = _poisonedColor;
        else
            _filler.color = _gradient.Evaluate(_currentHealth / _maximumHealth);

        _slider.value = _currentHealth;
    }

    public void DecreaseHealth(int health)
    {
        if (_currentHealth > health)
            _currentHealth -= health;
        else
            _currentHealth = 0;

        if (_isPoisoned)
            _filler.color = _poisonedColor;
        else
            _filler.color = _gradient.Evaluate(_currentHealth / _maximumHealth);

        _slider.value = _currentHealth;
    }
}