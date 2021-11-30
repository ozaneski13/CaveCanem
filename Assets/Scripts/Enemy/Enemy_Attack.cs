using System.Collections;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    //[SerializeField] private Animator _animator = null;

    [SerializeField] private int _damage = 10;
    [SerializeField] private float _durationBetweenAttacks = 3f;

    private IEnumerator _attackRoutine = null;

    private bool _isAttacking = false;

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void Attack()
    {
        if (_isAttacking)
            return;

        _attackRoutine = AttackRoutine();
        StartCoroutine(_attackRoutine);
    }

    private IEnumerator AttackRoutine()
    {
        _isAttacking = true;

        //_animator.SetBool("Attack", true);
        Player.Instance.TakeDamage(_damage);

        yield return new WaitForSeconds(_durationBetweenAttacks);

        _isAttacking = false;
    }
}