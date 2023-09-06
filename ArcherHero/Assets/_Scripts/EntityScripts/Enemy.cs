
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Entity
{   
    public event Action<Enemy> OnEnemyDie; 
    protected NavMeshAgent _agent;
    protected Vector3 _targetMovePosition;
    protected Transform _targetAttack;
    protected CancellationTokenSource _cancellationToken;
    private ResourceBank _resourceBank;
    [SerializeField] int _pointsForDestruction;
    
    
    [Inject]
    private void Construct(Player player, ResourceBank resourceBank)
    {
        _targetAttack = player.transform;
        _resourceBank = resourceBank;
    }

    private void AddResourcesForEnemyDestruction()
    {   
        _resourceBank.AddResource(ResourceType.Money, _pointsForDestruction);
    }

    protected override void Die()
    {
        _cancellationToken.Cancel();
        AddResourcesForEnemyDestruction();
        base.Die();
    }

    public void DestroyGO()
    {
        OnEnemyDie?.Invoke(this);
        Destroy(gameObject);
    }
    public void DestroyEnemy()
    {
        _cancellationToken.Cancel();
        Destroy(gameObject);
    }
}