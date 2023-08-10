
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{   
    // перенести сюда targetAttack по игроку (zenject)
    
    protected NavMeshAgent _agent;
    protected Vector3 _targetMovePosition;
    protected Transform _targetAttack;
    protected CancellationTokenSource _cancellationToken;
    
    protected override void Die()
    {
        _cancellationToken.Cancel();
        Destroy(gameObject);
        base.Die();
    }

}