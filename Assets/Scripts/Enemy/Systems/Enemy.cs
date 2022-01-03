using UnityEngine;
using System;

public abstract class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private EEnemy _enemyType;
    public EEnemy EnemyType => _enemyType;

    [SerializeField] private int _damage;
    public int Damage => _damage;

    [SerializeField] private float _durationBetweenAttacks;
    public float DurationBetweenAttacks => _durationBetweenAttacks;

    [SerializeField] private float _lookRadius;
    public float LookRadius => _lookRadius;

    [SerializeField] private float _rotationVariable;
    public float RotationVariable => _rotationVariable;

    [SerializeField] private float _interestTime;
    public float InterestTime => _interestTime;

    public Action OnEnemyHappy;
    public Action<string, EEnemy> OnWrongFeed;

    public void GetFeeded(Collectable collectable)
    {
        switch(_enemyType)
        {
            case EEnemy.Aggressive:
                if (collectable is Bone)
                {
                    _enemyType = EEnemy.Friendly;
                    OnEnemyHappy?.Invoke();
                }
                else
                    OnWrongFeed?.Invoke("Bone", _enemyType);
                break;

            case EEnemy.Friendly:
                break;

            case EEnemy.Hungry:
                if (collectable is Food)
                {
                    _enemyType = EEnemy.Friendly;
                    OnEnemyHappy?.Invoke();
                }
                else
                    OnWrongFeed?.Invoke("Food", _enemyType);
                break;
            case EEnemy.Rabid:
                if (collectable is Food)
                {
                    _enemyType = EEnemy.Friendly;
                    OnEnemyHappy?.Invoke();
                }
                else
                    OnWrongFeed?.Invoke("Food", _enemyType);
                break;
        }
    }
}