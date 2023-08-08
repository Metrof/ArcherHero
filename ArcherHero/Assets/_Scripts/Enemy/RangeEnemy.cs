
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Zenject;


public class RangeEnemy : Enemy
{
    [SerializeField] private float _timeToChangeDirection = 5f;
    [SerializeField] private float _movementBoundsX = 10f;
    [SerializeField] private float _movementBoundsZ = 5f;
    [SerializeField] private Transform _spawnProjectile;
    
    private bool isMoving = false;
    private Weapon _weapon;
    private ProjectilePool _projectilePool;
<<<<<<< Updated upstream

    public TypeDamage _typeDamage;
    
    

=======
    
>>>>>>> Stashed changes
    [Inject]
    private void Construct(ProjectilePool projectilePool)
    {
        _projectilePool = projectilePool;
    }
    
    private void Start()
<<<<<<< Updated upstream
    {   
=======
    {
>>>>>>> Stashed changes
        GameObject gObject = GameObject.FindGameObjectWithTag("Player");
        _targetAttack = gObject.transform;
        
        _agent = GetComponent<NavMeshAgent>();
        StartRandomMovement().Forget();
<<<<<<< Updated upstream
        _weapon = new Weapon(_projectilePool.GetPool(ProjectileOwner.SimpleEnemy, _typeDamage));
        _weapon.StartAttack(() => _targetAttack, _spawnProjectile, 30 , 30);
=======
        _weapon = new Weapon(_projectilePool.GetPool(ProjectileOwner.SimpleEnemy, typeDamage));
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream

=======
            
>>>>>>> Stashed changes
            await UniTask.Delay(TimeSpan.FromSeconds(_timeToChangeDirection), cancellationToken: _cancellationToken.Token). SuppressCancellationThrow(); 
            _weapon.StopAttack();
        }
    }

    protected override void Die()
    {   
        _cancellationToken.Cancel();
        _weapon.StopAttack();
        base.Die();
    }


    private void RotateTowardsTargetAttack()
    {
        if (_targetAttack == null) return;
        
        Vector3 direction = _targetAttack.position - transform.position;
        direction.y = 0f;
            
        Quaternion rotation = Quaternion.LookRotation(direction);
            
        transform.rotation = rotation;
        
        _weapon.StartAttack(() => _targetAttack, _spawnProjectile, damage , speedAttack);
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
