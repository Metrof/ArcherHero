
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class RangeEnemy : Enemy
{
    [SerializeField] private TypeDamage _bulletType;
    [SerializeField] private Transform _enemyBulletSpawnPoint; 
    [SerializeField] private Transform _targetAttack;
    [SerializeField] private float _timeToChangeDirection = 5f;
    [SerializeField] private float _movementBoundsX = 10f;
    [SerializeField] private float _movementBoundsZ = 5f;
    
    private CancellationToken _cancellationToken;
    private NavMeshAgent _agent;
    private Vector3 _targetPosition;
    private bool isMoving = false;

    [SerializeField] Projectile _projectile;

    private Weapon _weapon;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        StartRandomMovement().Forget();
        //_weapon = new Weapon();
        //_weapon.StartAttack(_targetAttack, _enemyBulletSpawnPoint, _bulletType, 10, 60);
    }
    
    private async UniTaskVoid StartRandomMovement()
    {   
        _cancellationToken = new CancellationToken();
        while (!_cancellationToken.IsCancellationRequested)
        {
            _targetPosition = new Vector3(Random.Range(-_movementBoundsX, _movementBoundsX), 0f, Random.Range(-_movementBoundsZ, _movementBoundsZ));

            isMoving = true;
            _agent.SetDestination(_targetPosition);
            
            await UniTask.WaitUntil(() => !isMoving);

            await UniTask.Delay((int)(_timeToChangeDirection * 1000), cancellationToken: _cancellationToken). SuppressCancellationThrow(); 
        }
    }
    private void RotateTowardsTargetAttack()
    {
        if (_targetAttack == null) return;
        
        Vector3 direction = _targetAttack.position - transform.position;
        direction.y = 0f;
            
        Quaternion rotation = Quaternion.LookRotation(direction);
            
        transform.rotation = rotation;
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
