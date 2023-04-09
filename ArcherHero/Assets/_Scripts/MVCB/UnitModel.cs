using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitModel 
{
    protected float _mapDiagonalSize;
    protected List<UnitBody> _lvlEnemyPull;
    protected UnitBody _currentTarget;

    public UnitModel(float mapSize)
    {
        _mapDiagonalSize = mapSize;
    }
    public void SetPull(List<UnitBody> lvlEnemyPull)
    {
        _lvlEnemyPull = lvlEnemyPull;
    }
    public virtual void ChangeTarget(Vector3 myPos)
    {
        if (_lvlEnemyPull.Count <= 0) return;

        float minMagnitude = _mapDiagonalSize;
        float currentMagnitude;
        foreach (var enemy in _lvlEnemyPull)
        {
            currentMagnitude = Vector3.Distance(myPos, enemy.transform.position);
            if (currentMagnitude < minMagnitude)
            {
                _currentTarget = enemy;
            }
        }
    }
    public virtual void Attack() { }
}
