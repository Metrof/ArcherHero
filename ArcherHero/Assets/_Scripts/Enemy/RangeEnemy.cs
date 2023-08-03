
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class RangeEnemy : Enemy
{
    [SerializeField] private GameObject enemyBulletPrefab; 
    [SerializeField] private Transform enemyBulletSpawnPoint; 
    [SerializeField] private float shootInterval = 2.0f;
    [SerializeField] private Transform player;
    
    private float _timer = 0.0f;
    
    
    public float _waitTime = 5f;
    public float movementBoundsX = 10f;
    public float movementBoundsZ = 5f;
    
    private NavMeshAgent _agent;
    private Vector3 _targetPosition;
    private bool isMoving = false;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        StartRandomMovement().Forget();
    }
    
    private async UniTaskVoid StartRandomMovement()
    {
        while (true)
        {
            _targetPosition = new Vector3(Random.Range(-movementBoundsX, movementBoundsX), 0f, Random.Range(-movementBoundsZ, movementBoundsZ));
            
            isMoving = true;
            _agent.SetDestination(_targetPosition);
            
            while (isMoving)
            {
                await UniTask.Yield();
            }
            
            await UniTask.Delay((int)(_waitTime * 1000)); 
        }
    }
    private void RotateTowardsPlayer()
    {
        if (player == null) return;
        
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;
            
        Quaternion rotation = Quaternion.LookRotation(direction);
            
        transform.rotation = rotation;
    }
    

   

    private void Update()
    {
        
        if (isMoving && !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            isMoving = false; 
            
            RotateTowardsPlayer();
            TimerShoot(); 
        }
    }

    private void TimerShoot()
    {
        if (player != null)
        {
            _timer += Time.deltaTime;
            if (_timer >= shootInterval)
            {
                ShootAtPlayer();
                _timer = 0.0f;
            }
        }
    }

    private void ShootAtPlayer()
    {
        
        GameObject bullet = Instantiate(enemyBulletPrefab, enemyBulletSpawnPoint.position, Quaternion.identity);
        
        
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        
        BulletExp bulletMovement = bullet.GetComponent<BulletExp>();
        if (bulletMovement != null)
        {
            bulletMovement.SetMoveDirection(directionToPlayer);
        }
        
        //Vector3 direction = (player.position - enemyBulletSpawnPoint.position).normalized;

        
        //bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;

        
        //Destroy(bullet, 3.0f);
    }
}
