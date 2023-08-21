using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class MeleeEnemy : Enemy
{   
    [Header("Movement Settings")]
    [SerializeField] private int _timeToUpdateMoveTarget;
    [SerializeField] private float _attackDistance;

    private const string _animationMove = "Move";
    private const string _animationAttack = "Attack";
    private const string _animationDead = "Dead";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        
        MoveToTarget().Forget();
    }

    private async UniTaskVoid MoveToTarget()
    {
        _cancellationToken = new CancellationTokenSource();

        _animator.SetBool(_animationMove, true);

        while (!_cancellationToken.IsCancellationRequested)
        {
            var position = _targetAttack.position;
            _targetMovePosition = new Vector3(position.x, 0, position.z);
            
            _agent.SetDestination(_targetMovePosition);
            _agent.stoppingDistance = _attackDistance;
            
            if (Vector3.Distance( transform.position, _targetAttack.position) <= _attackDistance)
            {
                _animator.SetBool(_animationMove, false);
                _animator.SetTrigger(_animationAttack);

                MeleeAttack();
                await UniTask.Delay(TimeSpan.FromSeconds(AttackDelay(speedAttack)), cancellationToken: _cancellationToken.Token). 
                    SuppressCancellationThrow();
            }
            else
            {
                _animator.SetBool(_animationMove, true);

                await UniTask.Delay(TimeSpan.FromSeconds(_timeToUpdateMoveTarget), cancellationToken: _cancellationToken.Token). 
                    SuppressCancellationThrow();
            }
        }
    }
    private double AttackDelay(float speedAttack)
    {
        return 1 / speedAttack;
    }

    private void MeleeAttack()
    {
        if (_targetAttack != null)
        {
            IDamageable damageable = _targetAttack.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(typeDamage, damage);
            }
        }
    }

    protected override void Die()
    {
        if (_cancellationToken.IsCancellationRequested)
        {
            return;
        }

        base.Die();
        _animator.SetTrigger(_animationDead);
    }
}
