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
            CheckNull(value);
            _projectileMovement = value;
        }
    }

    public void Initialize(int damage, IProjectileMovement projectileMovement, Vector3 targetPosition)
    {
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
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_typeDamage, Damage);
        }

        ProjectilePool.Release(this);
    }

    private void CheckNull(IProjectileMovement projectileMovement)
    {
        if (_projectileMovement == null)
        {
            throw new ArgumentNullException("ProjectileMovement can not be NULL");
        }
    }
}
