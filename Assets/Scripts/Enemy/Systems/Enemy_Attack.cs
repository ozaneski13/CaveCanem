using System.Collections;
using UnityEngine;
using System;

public class Enemy_Attack : MonoBehaviour
{
    [SerializeField] private Animator _animator = null;

    [SerializeField] private Enemy _enemy = null;
    [SerializeField] private Enemy_Movement _enemyMovement = null;

    private Player _player = null;

    public Action OnAnimCompleted;

    private IEnumerator _attackRoutine = null;

    private int _damage;

    private float _durationBetweenAttacks;

    private bool _isAttacking = false;

    private bool _isHappy = false;

    private bool _canAttack = true;

    private void Awake()
    {
        _damage = _enemy.Damage;
        _durationBetweenAttacks = _enemy.DurationBetweenAttacks;

        RegisterToEvents();
    }

    private void Start()
    {
        _player = Player.Instance;

        RegisterToLateEvents();
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
        _enemyMovement.OnAttack += Attack;
    }

    private void UnregisterFromEvents()
    {
        _enemy.OnEnemyHappy -= MakeHappy;
        _enemyMovement.OnAttack -= Attack;
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

    private void Attack()
    {
        if (_isAttacking || _isHappy || !_canAttack)
            return;

        _attackRoutine = AttackRoutine();
        StartCoroutine(_attackRoutine);
    }

    private IEnumerator AttackRoutine()
    {
        _isAttacking = true;

        _animator.SetBool("Attack", true);
        Player.Instance.TakeDamage(_damage);

        yield return new WaitForSeconds(_durationBetweenAttacks);

        _animator.SetBool("Attack", false);
        _isAttacking = false;
    }

    private void MakeHappy() => _isHappy = true;

    private void OnPlayerDeath() => _canAttack = false;
}