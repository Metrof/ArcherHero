using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPoint;
    private EnemySpawnManager _enemySpawnManager;

    Player _player;

    [Inject]
    private void Construct(Player player, EnemySpawnManager enemySpawnManager)
    {
        _player = player;
        _enemySpawnManager = enemySpawnManager;
    }

    private void Start()
    {   
        _player.transform.position = _playerSpawnPoint.position;
        _enemySpawnManager.SpawnEnemies();
    }
}
