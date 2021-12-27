using UnityEngine;
using UnityEngine.UI;

public class Enemy_B : Bilboard
{
    [SerializeField] private Enemy _enemy = null;
    [SerializeField] private Text _enemyIndicatorText = null;

    [SerializeField] private Color[] _colors = null;

    private void Awake()
    {
        int enemyType = (int)_enemy.EnemyType;

        _enemyIndicatorText.color = _colors[enemyType];
        _enemyIndicatorText.text = _enemy.EnemyType.ToString();

        _enemyIndicatorText.SetAllDirty();

        RegisterToEvents();
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }

    private void RegisterToEvents()
    {
        _enemy.OnEnemyHappy += MakeFriendly;
    }

    private void UnregisterFromEvents()
    {
        _enemy.OnEnemyHappy -= MakeFriendly;
    }

    private void MakeFriendly()
    {
        _enemyIndicatorText.color = _colors[(int)EEnemy.Friendly];
        _enemyIndicatorText.text = EEnemy.Friendly.ToString();
    }
}