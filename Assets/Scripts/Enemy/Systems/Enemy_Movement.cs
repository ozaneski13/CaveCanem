using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy_Movement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent = null;

    [SerializeField] private Enemy _enemy = null;

    private Transform _player = null;

    private Vector3 _startingPosition = Vector3.zero;

    private IEnumerator _chaseRoutine = null;

    private float _lookRadius;
    private float _rotationVariable;
    private float _interestTime;
    private float _passedTime = 0f;
    private float _distance;

    private bool _isChasing = false;
    private bool _canMove = true;

    public Action OnAttack;

    private void Awake()
    {
        _lookRadius = _enemy.LookRadius;
        _rotationVariable = _enemy.RotationVariable;
        _interestTime = _enemy.InterestTime;

        if (SceneManager.GetActiveScene().name == "TutorialLevel")
            _canMove = false;
    }

    private void Start()
    {
        _player = Player.Instance.transform;

        _startingPosition = transform.position;
    }

    private void Update()
    {
        if (_canMove)
        {
            _distance = Vector3.Distance(_player.position, transform.position);

            if (_distance <= _lookRadius)
            {
                if (!_isChasing)
                {
                    _chaseRoutine = ChaseRoutine();
                    StartCoroutine(_chaseRoutine);
                }

                else if (_isChasing)
                    _passedTime = 0f;
            }
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void FaceTarget()
    {
        Vector3 direction = (_player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationVariable);
    }

    private IEnumerator ChaseRoutine()
    {
        _isChasing = true;

        _passedTime = 0f;

        while (_passedTime < _interestTime)
        {
            _passedTime += Time.deltaTime;

            if (_distance <= _navMeshAgent.stoppingDistance)
            {
                OnAttack?.Invoke();
                FaceTarget();
            }

            else
                _navMeshAgent.SetDestination(_player.position);

            yield return null;
        }

        _isChasing = false;

        _navMeshAgent.SetDestination(_startingPosition);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _lookRadius);
    }

    public void CanMove() => _canMove = true;
}