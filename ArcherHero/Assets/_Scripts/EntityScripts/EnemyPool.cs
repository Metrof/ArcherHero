using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool 
{
    public event Action<bool> OnLastEnemyDie;
    private List<Enemy> _enemyList = new List<Enemy>();

    private float _maxRayDist = 20;
    private LayerMask _ignoreLayerMask = (1 << 0) | (1 << 6);
    private LayerMask _checkLayerMask = 1 << 6;

    Transform _nearestEnemy = null;
    public void AddEnemy(Enemy enemy)
    {
        if (enemy != null)
        {
            enemy.OnEnemyDie += DeleteEnemy;
            _enemyList.Add(enemy);
        }
    }
    private void DeleteEnemy(Enemy enemy)
    {
        _enemyList.Remove(enemy);
        if (_enemyList.Count == 0) OnLastEnemyDie?.Invoke(true);
    }
    public Transform GetNearestEnemy(Vector3 playerPos)
    {
        if (_enemyList.Count == 0) return null;
        _nearestEnemy = null;
        foreach (var enemy in _enemyList)
        {
            CheckEnemy(enemy, playerPos);
        }
        return _nearestEnemy;
    }
    private void CheckEnemy(Enemy enemy, Vector3 playerPos)
    {
        Vector3 nearestEnemyPos = _nearestEnemy != null ? _nearestEnemy.position : enemy.transform.position;

        if (Vector3.Distance(playerPos, enemy.transform.position) <= Vector3.Distance(playerPos, nearestEnemyPos))
        {
            RaycastHit hit;

            Ray ray = new Ray(playerPos, enemy.transform.position - playerPos);
            Physics.Raycast(ray, out hit, _maxRayDist, _ignoreLayerMask);
            if (hit.collider != null)
            {
                if ((_checkLayerMask.value & (1 << hit.collider.gameObject.layer)) != 0)
                {
                    _nearestEnemy = enemy.transform;
                }
            }
        }
    }
}
