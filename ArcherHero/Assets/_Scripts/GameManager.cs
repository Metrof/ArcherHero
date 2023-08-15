using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private LvlSwitchTriggerZone _lvlSwitchTriggerZone;

    private LvlSwithcManager _lvlSwitchManager;
    private EnemySpawnManager _enemySpawnManager;

    Player _player;
    LvlDoor _door;

    [Inject]
    private void Construct(Player player, EnemySpawnManager enemySpawnManager, EnemyPool enemyPool, LvlDoor lvlDoor)
    {
        _door = lvlDoor;
        _player = player;
        _enemySpawnManager = enemySpawnManager;
        _lvlSwitchManager = _enemySpawnManager.LvlSwithcManager;
        enemyPool.OnLastEnemyDie += LvlEnd;
        _player.OnPlayerDie += LvlEnd;
    }
    private void Awake()
    {
        if (_lvlSwitchTriggerZone != null)
        {
            _lvlSwitchTriggerZone.OnPlayerEnter += _lvlSwitchManager.SwitchToNextLevel;
        }
    }
    private void Start()
    {
        _door.CloseDoor();
        _enemySpawnManager.SpawnEnemies();
        _player.transform.position = _playerSpawnPoint.position;
        _player.Init();
    }

    private void LvlEnd(bool isPlayerWin)
    {
        if (isPlayerWin)
        {
            _door.OpenDoor();
        }
    }
}
