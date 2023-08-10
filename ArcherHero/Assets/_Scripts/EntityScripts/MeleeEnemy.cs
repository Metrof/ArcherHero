using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;


public class MeleeEnemy : Enemy
{
    [SerializeField] private int _timeToUpdateMoveTarget;
    [SerializeField] private float _attackDistance;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        
        MoveToTarget().Forget();
    }

    private async UniTaskVoid MoveToTarget()
    {
        _cancellationToken = new CancellationTokenSource();
        while (!_cancellationToken.IsCancellationRequested)
        {
            var position = _targetAttack.position;
            _targetMovePosition = new Vector3(position.x, 0, position.z);
            
            _agent.SetDestination(_targetMovePosition);
            _agent.stoppingDistance = _attackDistance;
            
            if (Vector3.Distance( transform.position, _targetAttack.position) <= _attackDistance)
            {   
                MeleeAttack();
                await UniTask.Delay(TimeSpan.FromSeconds(AttackDelay(speedAttack)), cancellationToken: _cancellationToken.Token). 
                    SuppressCancellationThrow();
            }
            else
            {
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
        Debug.Log("MeleeAttack");
    }
}
