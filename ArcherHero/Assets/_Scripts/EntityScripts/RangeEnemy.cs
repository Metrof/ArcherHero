
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Zenject;

[RequireComponent(typeof(Animator))]
public class RangeEnemy : Enemy
{   
    [Header("Movement Settings")]
    [SerializeField] private float _timeToChangeDirection = 5f;
    [SerializeField] private float _movementBoundsX = 10f;
    [SerializeField] private float _movementBoundsZ = 5f;
    
    [Space]
    [SerializeField] private Transform _spawnProjectile;

    private const string _animationAttack = "Attack";
    private const string _animationDead = "Dead";

    private bool isMoving = false;
    private Weapon _weapon;
    private ProjectilePool _projectilePool;
    private Animator _animator;
    
    
    
    [Inject]
    private void Construct(ProjectilePool projectilePool)
    {
        _projectilePool = projectilePool;
    }
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        StartRandomMovement().Forget();
        
        _weapon = new Weapon(_projectilePool.GetPool(ProjectileOwner.SimpleEnemy, typeDamage));
    }
    
    private async UniTaskVoid StartRandomMovement()
    {   
        _cancellationToken = new CancellationTokenSource();
        while (!_cancellationToken.IsCancellationRequested)
        {
            _targetMovePosition = new Vector3(Random.Range(-_movementBoundsX, _movementBoundsX), 0f, Random.Range(-_movementBoundsZ, _movementBoundsZ));
            
            isMoving = true;
            _agent.SetDestination(_targetMovePosition);
            
            await UniTask.WaitUntil(() => !isMoving);
            
            await UniTask.Delay(TimeSpan.FromSeconds(_timeToChangeDirection), cancellationToken: _cancellationToken.Token). SuppressCancellationThrow(); 
            _weapon.StopAttack();
        }
    }

    protected override void Die()
    {
        if (_cancellationToken.IsCancellationRequested)
        {
            return;
        }

        _weapon.StopAttack();
        base.Die();

        _animator.SetTrigger(_animationDead);
    }


    private void RotateTowardsTargetAttack()
    {
        if (_targetAttack == null) return;
        
        Vector3 direction = _targetAttack.position - transform.position;
        direction.y = 0f;
            
        Quaternion rotation = Quaternion.LookRotation(direction);
            
        transform.rotation = rotation;


        _weapon.StartAttack(AttackTarget, _spawnProjectile, damage, speedAttack);
    }

    private Transform AttackTarget()
    {
        _animator.SetTrigger(_animationAttack);
        return _targetAttack;
    }

    private void Update()
    {
        if (isMoving && !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            isMoving = false;
            RotateTowardsTargetAttack();
        }
    }
}
