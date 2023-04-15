using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitModel 
{
    public delegate void AttackCorotineDelegate();
    public event AttackCorotineDelegate OnStartAttack;
    public event AttackCorotineDelegate OnStopAttack;

    public event Action OnAttackModel;
    public event Action OnDeath;

    protected int _maxHP;
    protected float _movementSpeed;
    protected float _rotationSpeed;
    protected float _attackDellay;

    protected float _currentHP;
    protected int _layerNum;
    protected float _mapDiagonalSize;
    protected List<Transform> _lvlEnemyPull;
    protected Transform _currentTarget;
    public int MaxHP { get { return _maxHP; } private set { _maxHP = value; } }
    public float CurrentHP
    {
        get { return _currentHP; }
        private set
        {
            _currentHP = value;

            if (_currentHP <= 0)
            {
                _currentHP = 0;
                OnDeath?.Invoke();
            }
        }
    }
    public float AttackDellay { get { return _attackDellay; } private set { _attackDellay = value; } }
    public float MovementSpeed { get { return _movementSpeed; } private set { _movementSpeed = value; } }
    public float RotationSpeed { get { return _rotationSpeed; } private set { _rotationSpeed = value; } }

    public UnitModel(float mapSize, int layerNum)
    {
        _mapDiagonalSize = mapSize;
        _layerNum = layerNum;
    }
    public void SetPull(List<Transform> lvlEnemyPull)
    {
        _lvlEnemyPull = lvlEnemyPull;
        OnStartAttack?.Invoke();
    }
    public void SetStats(CharacterStats stats)
    {
        MaxHP = stats.MaxHp;
        AttackDellay = stats.AttackDellay;
        MovementSpeed = stats.MovementSpeed;
        RotationSpeed = stats.RotationSpeed;

        CurrentHP = MaxHP;
    }
    public void ChangeStats(CharacterStats stats)
    {
        MaxHP += stats.MaxHp;
        CurrentHP += stats.HPChange;
        AttackDellay += stats.AttackDellay;
        MovementSpeed += stats.MovementSpeed;
        RotationSpeed += stats.RotationSpeed;
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
        projectile.MoveToDirection(_currentTarget.position - shotPos, shotPos);
    }
}
