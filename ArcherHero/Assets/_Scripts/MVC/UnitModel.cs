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

    protected float _maxHP;
    protected float _movementSpeed;
    protected float _rotationSpeed;
    protected float _attackDellay;

    protected float _currentHP;
    protected int _layerNum;
    protected float _mapDiagonalSize;
    protected List<Transform> _lvlEnemyPull;
    protected Transform _currentTarget;
    protected Material _material;

    protected CharacterStatsE _spawnStats;
    public float MaxHP { get { return _maxHP; } private set { _maxHP = value; } }
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
    public bool ThereIsTarget { get { return _currentTarget != null; } }

    public UnitModel(int layerNum, Material material)
    {
        _layerNum = layerNum;
        _material = material;
    }
    public void SetPull(List<Transform> lvlEnemyPull)
    {
        _lvlEnemyPull = lvlEnemyPull;
        OnStartAttack?.Invoke();
    }
    public void SetStats(CharacterStatsE stats)
    {
        _spawnStats = stats;
        MaxHP = stats.MaxHp;
        AttackDellay = stats.AttackDellay;
        MovementSpeed = stats.MovementSpeed;
        RotationSpeed = stats.RotationSpeed;

        CurrentHP = MaxHP;
    }
    public virtual void ChangeStats(CharacterStatsE stats)
    {
        MaxHP += stats.MaxHp;
        CurrentHP += stats.HPChange;
        AttackDellay += stats.AttackDellay;
        MovementSpeed += stats.MovementSpeed;
        RotationSpeed += stats.RotationSpeed;
    }
    public void Restats()
    {
        SetStats(_spawnStats);
    }
    public virtual void ChangeTarget(Vector3 myPos)
    {
        
    }
    public virtual void Attack(Transform shotPos)
    {
        OnAttackModel?.Invoke();
        if (_currentTarget == null) return;
        Projectile projectile = ProjectilePull.GetProjectile(_layerNum, _material);
        projectile.MoveToDirection(_currentTarget.position - shotPos.position, shotPos);
    }
}
