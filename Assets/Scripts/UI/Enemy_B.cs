using UnityEngine;
using UnityEngine.UI;

public class Enemy_B : Bilboard
{
    [SerializeField] private Enemy _enemy = null;
    [SerializeField] private Text _enemyIndicatorText = null;

    [SerializeField] private Color[] _colors = null;

    private void Awake()
    {
        int enemyType = _enemy.EnemyType;

        _enemyIndicatorText.color = _colors[enemyType];

        switch (enemyType)
        {
            case 0:
                _enemyIndicatorText.text = "Aggressive";
                break;

            case 1:
                _enemyIndicatorText.text = "Hungry";
                break;

            default:
                _enemyIndicatorText.text = "Friendly";
                break;
        }

        _enemyIndicatorText.SetAllDirty();
    }
}