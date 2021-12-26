using System.Collections;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    //[SerializeField] private Animator _animator = null;

    [SerializeField] private Enemy _enemy = null;
    [SerializeField] private Enemy_Movement _enemyMovement = null;

    private IEnumerator _attackRoutine = null;

    private int _damage;

    private float _durationBetweenAttacks;

    private bool _isAttacking = false;

    private bool _isHappy = false;

    private void Awake()
    {
        _damage = _enemy.Damage;
        _durationBetweenAttacks = _enemy.DurationBetweenAttacks;

        RegisterToEvents();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();

        UnregisterFromEvents();
    }

    private void RegisterToEvents()
    {
        _enemy.OnEnemyHappy += MakeHappy;
        _enemyMovement.OnAttack += Attack;
    }

    private void UnregisterFromEvents()
    {
        _enemy.OnEnemyHappy -= MakeHappy;
        _enemyMovement.OnAttack -= Attack;
    }

    private void Attack()
    {
        if (_isAttacking || _isHappy)
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

    private void MakeHappy() => _isHappy = true;
}