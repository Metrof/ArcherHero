using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool 
{
    private List<Enemy> _enemyList = new List<Enemy>(); 

    public void AddEnemy(Enemy enemy)
    {
        _enemyList.Add(enemy);
    }

    public Transform GetNearestEnemy(Vector3 playerPos)
    {
        if (_enemyList.Count == 0) return null;
        Transform nearestEnemy = _enemyList[0].transform;
        foreach (var enemy in _enemyList)
        {
            if (Vector3.Distance(playerPos, enemy.transform.position) < Vector3.Distance(playerPos, nearestEnemy.transform.position))
            {
                nearestEnemy = enemy.transform;
            }
        }
        return nearestEnemy;
    }
}
