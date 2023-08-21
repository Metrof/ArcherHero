
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Enemy : Entity
{
    public event Action<Enemy> OnEnemyDie; 
    protected NavMeshAgent _agent;
    protected Vector3 _targetMovePosition;
    protected Transform _targetAttack;
    protected CancellationTokenSource _cancellationToken;
    
    
    [Inject]
    private void Construct(Player player)
    {
        _targetAttack = player.transform;
    }
    protected override void Die()
    {
        _cancellationToken.Cancel();
        base.Die();
    }

    public void DestroyGO()
    {
        OnEnemyDie?.Invoke(this);

        Destroy(gameObject);
    }
}