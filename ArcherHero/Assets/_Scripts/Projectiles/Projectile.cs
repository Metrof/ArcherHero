using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private TypeDamage _typeDamage;
    [SerializeField] private int _moveSpeedProjectile;

    private int _damage;
    private Vector3 _targetPosition;
    private IProjectileMovement _projectileMovement;

    public TypeDamage TypeDamage { get => _typeDamage; }
    public int Damage { get
        {
            return _damage;
        }
        set
        {
            _damage = value < 0 ? 0 : value;
        }
    }
    public IProjectileMovement ProjectileMovement
    {
        get
        {
            CheckNull(_projectileMovement);
            return _projectileMovement;
        }
        set
        {
            CheckNull(value);
            _projectileMovement = value;
        }
    }

    public void Initialize(int damage, IProjectileMovement projectileMovement)
    {
        ProjectileMovement = projectileMovement;
        Damage = damage;
    }

    private void Update()
    {
        ProjectileMovement.Move(transform, _targetPosition, _moveSpeedProjectile);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_typeDamage, Damage);
        }

        Destroy(gameObject);
    }

    private void CheckNull(IProjectileMovement projectileMovement)
    {
        if (_projectileMovement == null)
        {
            throw new ArgumentNullException("ProjectileMovement can not be NULL");
        }
    }
}
