using UnityEngine;
using UnityEngine.AI;

public class Enemy_Movement : MonoBehaviour
{
    [SerializeField] private Transform _player = null;
    [SerializeField] private NavMeshAgent _navMeshAgent = null;

    [SerializeField] private float _lookRadius = 10f;
    [SerializeField] private float _rotationVariable = 5f;

    private float _distance = 0f;

    private void Update()
    {
        _distance = Vector3.Distance(_player.position, transform.position);

        if (_distance <= _lookRadius)
        {
            _navMeshAgent.SetDestination(_player.position);

            if (_distance <= _navMeshAgent.stoppingDistance)
            {
                //Enemy_Attack.Attack();
                FaceTarget();
            }
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (_player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationVariable);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _lookRadius);
    }
}