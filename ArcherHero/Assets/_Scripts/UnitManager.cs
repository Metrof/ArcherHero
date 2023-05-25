using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : Singleton<UnitManager>
{
    private List<EnemyController> _enemies = new();
    private List<Transform> _enemysTransforms = new();

    private List<EnemyController> _waveEnemies = new();
    private List<Transform> _waveEnemysTransforms = new();

    private PlayerController _player;
    private Transform _playerTransforms;

    public List<Transform> EnemiesTransforms
    {
        get => _enemysTransforms;
    }
    public List<EnemyController> Enemies
    {
        get => _enemies;
    }

    public List<Transform> WaveEnemiesTransforms
    {
        get => _waveEnemysTransforms;
    }
    public List<EnemyController> WaveEnemies
    {
        get => _waveEnemies;
    }

    public Transform PlayerTransform
    {
        get => _playerTransforms;
    }
    public PlayerController Player
    {
        get => _player;
    }

    public void SetPlayerAtUnitManager(PlayerController player)
    {
        if(_player != null) _player.OnEnablePerson -= DeleteUnitFromTargetList;
        _player = null;
        _playerTransforms = null;

        _playerTransforms = player.transform;
        _player = player;

        _player.OnEnablePerson += DeleteUnitFromTargetList;
    }
    public void SetEnemysAtUnitManager(List<EnemyController> lvlEnemies)
    {
        _enemies = new List<EnemyController>();
        _enemysTransforms = new List<Transform>();
        foreach (var enemy in lvlEnemies)
        {
            _enemysTransforms.Add(enemy.transform);
            _enemies.Add(enemy);
        }
    }
    private void SubscribeDiedEvent(bool subscribe)
    {
        if (WaveEnemies.Count == 0) return;

        if (subscribe)
        {
            foreach (var enemy in WaveEnemies)
            {
                enemy.OnEnablePerson += DeleteUnitFromTargetList;
            }
        }
        else
        {
            foreach (var enemy in WaveEnemies)
            {
                enemy.WaveStop();
                enemy.OnEnablePerson -= DeleteUnitFromTargetList;
            }
            WaveEnemies.Clear();
        }
    }
    private void DeleteUnitFromTargetList(Transform unitTransform)
    {
        if (WaveEnemiesTransforms.Contains(unitTransform))
        {
            WaveEnemiesTransforms.Remove(unitTransform);
        }
        else
        {
            _playerTransforms = null;
        }
    }

    public void TeleportWaveEnemys(SpawnPoint[] spawnPoints)
    {
        Enemies.AddRange(WaveEnemies);
        List<EnemyController> waveEnemys = new();
        EnemyController enemyController = null;
        foreach (var point in spawnPoints)
        {
            foreach (var enemy in Enemies)
            {
                if (point.Type == enemy.Type)
                {
                    waveEnemys.Add(enemy);
                    enemyController = enemy;
                    enemy.Teleportation(point.Transform.position);
                    break;
                }
            }
            Enemies.Remove(enemyController);
        }
        SetEnemyWave(waveEnemys);
    }
    private void SetEnemyWave(List<EnemyController> waveEnemy)
    {
        SubscribeDiedEvent(false);
        _waveEnemies = waveEnemy;
        SubscribeDiedEvent(true);
        foreach (var enemy in _waveEnemies)
        {
            _waveEnemysTransforms.Add(enemy.transform);
        }
        WaveStart();
    }
    private void WaveStart()
    {
        Player.WaveStart();
        foreach (var enemy in WaveEnemies)
        {
            enemy.WaveStart();
        }
    }
    public void LvlEnd()
    {
        SubscribeDiedEvent(false);
        Player.WaveStop();
    }
    public bool AnySurvivingEnemies()
    {
        return WaveEnemiesTransforms.Count != 0;
    }
}
