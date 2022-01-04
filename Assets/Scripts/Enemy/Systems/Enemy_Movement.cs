using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy_Movement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent = null;

    [SerializeField] private Enemy _enemy = null;

    [SerializeField] private Animator _animator = null;

    private Player _player = null;

    private Vector3 _startingPosition = Vector3.zero;

    private IEnumerator _chaseRoutine = null;

    private float _lookRadius;
    private float _rotationVariable;
    private float _interestTime;
    private float _passedTime = 0f;
    private float _distance;

    private bool _isChasing = false;
    private bool _canMove = true;

    private bool _isHappy = false;

    private bool _isPlayerDead = false;

    public Action OnAttack;

    private void Awake()
    {
        if (_enemy.EnemyType == EEnemy.Friendly)
            _isHappy = true;
        _lookRadius = _enemy.LookRadius;
        _rotationVariable = _enemy.RotationVariable;
        _interestTime = _enemy.InterestTime;

        if (SceneManager.GetActiveScene().name == "TutorialLevel")
            _canMove = false;

        RegisterToEvents();
    }

    private void Start()
    {
        _player = Player.Instance;

        _startingPosition = transform.position;

        RegisterToLateEvents();
    }

    private void Update()
    {
        if (_canMove && !_isHappy && !_isPlayerDead)
        {
            _distance = Vector3.Distance(_player.transform.position, transform.position);

            if (_distance <= _lookRadius)
            {
                if (!_isChasing)
                {
                    _chaseRoutine = ChaseRoutine();
                    StartCoroutine(_chaseRoutine);
                }

                else if (_isChasing)
                    _passedTime = 0f;

                if (_navMeshAgent.velocity.magnitude > 0.01 && _navMeshAgent.velocity.magnitude < 3f)
                {
                    _animator.SetBool("Walk", true);
                    _animator.SetBool("Run", false);
                }

                else if (_navMeshAgent.velocity.magnitude > 3f) 
                {
                    _animator.SetBool("Walk", false);
                    _animator.SetBool("Run", true);
                }

                else
                {
                    _animator.SetBool("Idle", true);
                    _animator.SetBool("Walk", false);
                }
            }
        }
        else if(_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            _animator.SetBool("Idle", true);
            _animator.SetBool("Walk", false);
            _animator.SetBool("Run", false);
            //_navMeshAgent.SetDestination(_enemy.transform.position);
            

        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();

        UnregisterFromEvents();

        UnregisterFromLateEvents();
    }

    private void RegisterToEvents()
    {
        _enemy.OnEnemyHappy += MakeHappy;
    }

    private void UnregisterFromEvents()
    {
        if (_enemy != null)
            _enemy.OnEnemyHappy -= MakeHappy;
    }

    private void RegisterToLateEvents()
    {
        _player.OnDeath += OnPlayerDeath;
    }

    private void UnregisterFromLateEvents()
    {
        if (_player != null)
            _player.OnDeath -= OnPlayerDeath;
    }

    private void FaceTarget()
    {
        Vector3 direction = (_player.transform.position - transform.position).normalized;
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
                _navMeshAgent.SetDestination(_player.transform.position);

            yield return null;
        }

        _isChasing = false;

        _navMeshAgent.SetDestination(_startingPosition);
        _animator.SetBool("Walk", true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _lookRadius);
    }

    public void CanMove() => _canMove = true;

    private void MakeHappy() => _isHappy = true;

    private void OnPlayerDeath()
    {
        _isPlayerDead = true;

        StopCoroutine(_chaseRoutine);

        _navMeshAgent.SetDestination(_startingPosition);

        _isChasing = false;
    }
}