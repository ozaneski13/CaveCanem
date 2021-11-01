using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Movement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent = null;

    [SerializeField] private float _lookRadius = 10f;
    [SerializeField] private float _rotationVariable = 5f;
    [SerializeField] private float _interestTime = 3f;

    private Transform _player = null;

    private Vector3 _startingPosition = Vector3.zero;

    private IEnumerator _chaseRoutine = null;

    private float _distance = 0f;

    private bool _isChasing = false;

    private void Start()
    {
        _player = Player.Instance.transform;

        _startingPosition = transform.position;
    }

    private void Update()
    {
        _distance = Vector3.Distance(_player.position, transform.position);

        if (_distance <= _lookRadius)
        {
            if (!_isChasing)
            {
                _chaseRoutine = ChaseRoutine();
                StartCoroutine(_chaseRoutine);
            }

            if (_distance <= _navMeshAgent.stoppingDistance)
            {
                //Enemy_Attack.Attack();
                FaceTarget();
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

        float passedTime = 0f;

        while (passedTime < _interestTime)
        {
            passedTime += Time.deltaTime;

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
}