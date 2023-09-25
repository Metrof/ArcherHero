using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _menuButtons;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private LvlSwitchTriggerZone _lvlSwitchTriggerZone;
    [SerializeField] private CinemachineVirtualCamera _startVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera _gameVirtualCamera;

    private LvlSwitchManager _lvlSwitchManager;

    Player _player;
    LvlDoor _door;
    EnemyPool _enemyPool;
    AudioManager _audioManager;

    [Inject]
    private void Construct(Player player, LvlSwitchManager lvlSwitchManager, EnemyPool enemyPool, LvlDoor lvlDoor, AudioManager audioManager)
    {
        _door = lvlDoor;
        _player = player;
        _lvlSwitchManager = lvlSwitchManager;
        _enemyPool = enemyPool;
        _audioManager = audioManager;   
    }
    private void OnEnable()
    {
        _lvlSwitchManager.OnLevelOver += GameOver;
        _enemyPool.OnLastEnemyDie += LvlEnd;
        _player.OnPlayerDie += LvlEnd;
    }
    private void OnDisable()
    {
        _lvlSwitchManager.OnLevelOver -= GameOver;
        _enemyPool.OnLastEnemyDie -= LvlEnd;
        _player.OnPlayerDie -= LvlEnd;
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
        _player.PlayerDisable();
        ReturnPlayerToStartPos();
        if (_gameVirtualCamera != null)
        {
            _gameVirtualCamera.Follow = _player.transform;
        }
        if (_startVirtualCamera != null)
        {
            _startVirtualCamera.LookAt = _player.transform;
        }
    }
    private void LvlEnd(bool isPlayerWin)
    {
        if (isPlayerWin)
        {
            _door.OpenDoor();
            _audioManager.Play(3);
        }
        else
        {
            GameOver();
        }
    }
    public void StartGame()
    {
        _startVirtualCamera.Priority = 0;
        _gameVirtualCamera.Priority = 1;
        StartNewLvl();

        _player.PlayerEnable();
        _menuButtons.SetActive(false);
    }
    private void GameOver()
    {
        _startVirtualCamera.Priority = 1;
        _gameVirtualCamera.Priority = 0;

        _door.CloseDoor();
        _player.PlayerDisable();
        _lvlSwitchManager.StayLvlZero();
        _menuButtons.SetActive(true);
        _enemyPool.ClearEnemyList();
        ReturnPlayerToStartPos();
        _player.transform.rotation = Quaternion.Euler(0, 160, 0);
    }
    private void StartNewLvl()
    {
        _lvlSwitchManager.SwitchLvl();
        ReturnPlayerToStartPos();

        _audioManager.Play(4);

        _door.CloseDoor();
    }
    private void ReturnPlayerToStartPos()
    {
        _player.transform.position = _playerSpawnPoint != null ? _playerSpawnPoint.position : Vector3.zero;
        Physics.SyncTransforms();
    }
}
