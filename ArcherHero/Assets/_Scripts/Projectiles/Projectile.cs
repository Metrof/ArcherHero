using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    public event Action OnHitEvent;
    public event Action OnEnabledEvent;

    [SerializeField] private TypeDamage _typeDamage;
    [SerializeField] private int _moveSpeedProjectile;

    [NonSerialized]
    public Sequence MoveSequence;
    [NonSerialized]
    public int Count = 0;

    private ObjectPool<Projectile> _projectilePool;
    private int _damage;
    private Transform _target;
    private IProjectileMovement _projectileMovement;
    private IProjectileHit _projectileHit;

    public ProjectileOwner Owner { get; set; }
    public int MoveSpeedProjectile { get => _moveSpeedProjectile; }
    public float StartPositionY { get; private set; }

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

        StartPositionY = transform.position.y;
    }

    private void OnEnable()
    {
        OnEnabledEvent?.Invoke();
    }

    public void Move()
    {
        NewMoveSequence();
        ProjectileMovement.Move(this, _target.position, _moveSpeedProjectile);
    }

    public void MoveToPoint(Vector3 endPosition)
    {
        NewMoveSequence();
        ProjectileMovement.Move(this, endPosition, _moveSpeedProjectile);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnHitEvent?.Invoke();
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

    public Sequence NewMoveSequence()
    {
        MoveSequence?.Kill();
        MoveSequence = DOTween.Sequence();

        return MoveSequence;
    }
}
