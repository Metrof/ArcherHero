using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitModel 
{
    public delegate void AttackCorotineDelegate();
    public event AttackCorotineDelegate OnStartAttack;
    public event AttackCorotineDelegate OnStopAttack;

    public event Action OnAttackModel;


    protected int _layerNum;
    protected float _mapDiagonalSize;
    protected List<UnitBody> _lvlEnemyPull;
    protected UnitBody _currentTarget;

    public UnitModel(float mapSize, int layerNum)
    {
        _mapDiagonalSize = mapSize;
        _layerNum = layerNum;
    }
    public void SetPull(List<UnitBody> lvlEnemyPull)
    {
        _lvlEnemyPull = lvlEnemyPull;
        OnStartAttack?.Invoke();
    }
    public virtual void ChangeTarget(Vector3 myPos)
    {
        if (_lvlEnemyPull.Count <= 0) 
        {
            _currentTarget = null;
            return;
        }

        float minMagnitude = _mapDiagonalSize;
        float currentMagnitude;
        foreach (var enemy in _lvlEnemyPull)
        {
            currentMagnitude = Vector3.Distance(enemy.transform.position, myPos);
            if (currentMagnitude < minMagnitude)
            {
                minMagnitude = currentMagnitude;
                _currentTarget = enemy;
            }
        }
    }
    public virtual void Attack(Vector3 shotPos)
    {
        OnAttackModel?.Invoke();
        if (_currentTarget == null) 
        {
            OnStopAttack?.Invoke();
            return;
        }
        Projectile projectile = ProjectilePull.GetProjectile(_layerNum);
        projectile.MoveToDirection(_currentTarget.Position - shotPos, shotPos);
    }
}
