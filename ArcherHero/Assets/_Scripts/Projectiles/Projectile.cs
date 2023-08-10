using System;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [SerializeField] private TypeDamage _typeDamage;
    [SerializeField] private int _moveSpeedProjectile;

    private ObjectPool<Projectile> _projectilePool;
    private int _damage;
    private Vector3 _targetPosition;
    private IProjectileMovement _projectileMovement;
    private IProjectileHit _projectileHit;

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

    public void Initialize(int damage, Vector3 targetPosition, IProjectileMovement projectileMovement, IProjectileHit projectileHit )
    {
        _projectileHit = projectileHit;
        _projectileMovement = projectileMovement;
        Damage = damage;
        _targetPosition = targetPosition;
    }

    private void Update()
    {
        ProjectileMovement.Move(transform, _targetPosition, _moveSpeedProjectile);
    }

    private void OnTriggerEnter(Collider other)
    {
        _projectileHit.Hit(other, this);

        ProjectilePool.Release(this);
    }

    private void CheckMovementNull(IProjectileMovement projectileMovement)
    {
        if (_projectileMovement == null)
        {
            throw new ArgumentNullException("ProjectileMovement can not be NULL");
        }
    }
}
