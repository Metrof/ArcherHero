using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [SerializeField] private TypeDamage _typeDamage;
    [SerializeField] private int _moveSpeedProjectile;

    [NonSerialized]
    public Sequence MoveSequence;

    private ObjectPool<Projectile> _projectilePool;
    private int _damage;
    private Transform _target;
    private IProjectileMovement _projectileMovement;
    private IProjectileHit _projectileHit;

    private int _count = 0;

    public float StartPositionY { get; private set; }
    public int Count
    {
        get
        {
            return _count++;
        }
        set
        {
            _count = value;
        }
    }

    public TypeDamage TypeDamage { get => _typeDamage; }

    public ObjectPool<Projectile> ProjectilePool
    {
        get => _projectilePool;
        set => _projectilePool ??= value;
    }

    public int Damage
    {
        get => _damage;
        set => _damage = value < 0 ? 0 : value;
    }

    public IProjectileHit ProjectileHit { get => _projectileHit; }

    public IProjectileMovement ProjectileMovement
    {
        get
        {
            return _projectileMovement;
        }
        set
        {
            CheckMovementNull(value);
            _projectileMovement = value;
        }
    }

    public void Initialize(int damage, Transform target, IProjectileMovement projectileMovement, IProjectileHit projectileHit)
    {
        _projectileHit = projectileHit;
        _projectileMovement = projectileMovement;
        Damage = damage;
        _target = target;
    }

    public void Move()
    {
        StartPositionY = transform.position.y;

        MoveSequence = DOTween.Sequence();
        ProjectileMovement.Move(this, _target, _moveSpeedProjectile);
    }

    private void OnTriggerEnter(Collider other)
    {
        _projectileHit.Hit(other, this);
    }

    private void CheckMovementNull(IProjectileMovement projectileMovement)
    {
        if (_projectileMovement == null)
        {
            throw new ArgumentNullException("ProjectileMovement can not be NULL");
        }
    }

    private void OnDisable()
    {
        MoveSequence?.Kill();
        Count = 0;
    }
}
