using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    public float Speed => _speed;

    [SerializeField] private float _interestTime = 5f;
    public float InterestTime => _interestTime;

    [SerializeField] private bool _isPoisonous = false;
    public bool IsPoisonous => _isPoisonous;
}