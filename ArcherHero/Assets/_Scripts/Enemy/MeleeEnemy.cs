using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : Enemy
{
    [SerializeField] private Transform _targetAttack;


    private void Start()
    {
        GameObject gObject = GameObject.FindGameObjectWithTag("Player");
        _targetAttack = gObject.transform;
        
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
            
            await UniTask.Delay(TimeSpan.FromSeconds(5), cancellationToken: _cancellationToken.Token). SuppressCancellationThrow(); 
        }
    }

   
}
