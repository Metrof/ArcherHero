using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool 
{
    private List<Enemy> _enemyList = new List<Enemy>();
    private const int _defaultLayer = 0;

    Transform _nearestEnemy;
    public void AddEnemy(Enemy enemy)
    {
        if (enemy != null) _enemyList.Add(enemy);
    }

    public Transform GetNearestEnemy(Vector3 playerPos)
    {
        if (_enemyList.Count == 0) return null;
        _nearestEnemy = _enemyList[0].transform;
        foreach (var enemy in _enemyList)
        {
            CheckEnemy(enemy, playerPos);
        }
        return _nearestEnemy;
    }
    private void CheckEnemy(Enemy enemy, Vector3 playerPos)
    {
        if (Vector3.Distance(playerPos, enemy.transform.position) < Vector3.Distance(playerPos, _nearestEnemy.transform.position))
        {
            RaycastHit hit;
            Ray ray = new Ray(playerPos, enemy.transform.position - playerPos);
            Physics.Raycast(ray, out hit);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer != _defaultLayer) _nearestEnemy = enemy.transform;
            }
        }
    }
}
