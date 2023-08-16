using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private LvlSwitchTriggerZone _lvlSwitchTriggerZone;

    private LvlSwitchManager _lvlSwitchManager;

    Player _player;
    LvlDoor _door;

    [Inject]
    private void Construct(Player player, LvlSwitchManager lvlSwitchManager, EnemyPool enemyPool, LvlDoor lvlDoor)
    {
        _door = lvlDoor;
        _player = player;
        _lvlSwitchManager = lvlSwitchManager;
        enemyPool.OnLastEnemyDie += LvlEnd;
        _player.OnPlayerDie += LvlEnd;
    }
    private void Awake()
    {
        if (_lvlSwitchTriggerZone != null)
        {
            _lvlSwitchTriggerZone.OnPlayerEnter += StartNewLvl;
        }
    }
    private void Start()
    {
        StartNewLvl();
    }

    private void LvlEnd(bool isPlayerWin)
    {
        if (isPlayerWin)
        {
            _door.OpenDoor();
        }
    }
    private void StartNewLvl()
    {
        _lvlSwitchManager.SwitchLvl();

        _player.transform.position = _playerSpawnPoint.position;
        Physics.SyncTransforms();
        _player.Init();

        _door.CloseDoor();
    }
}
