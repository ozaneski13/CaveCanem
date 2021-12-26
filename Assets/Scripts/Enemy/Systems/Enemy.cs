using UnityEngine;

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

    public void GetFeeded(Collectable collectable)
    {
        switch(_enemyType)
        {
            case EEnemy.Aggressive:
                if (collectable is Bone)
                    //MakeHappy
                    ;
                break;
            case EEnemy.Friendly:
                break;
            case EEnemy.Hungry:
                //if (collectable is Food)
                    //MakeHappy
                    ;
                break;
        }
    }
}