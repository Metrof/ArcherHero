using PlayerStats;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public sealed class Player : Entity
{
    public event Action<bool> OnPlayerDie;
    [SerializeField] private Transform _spawnProjectile;
    [SerializeField] private List<Skill> _skills;

    CharacterController _characterController;
    Controller _controller;

    private Weapon _weapon;
    private ProjectilePool _projectilePool;
    private EnemyPool _enemyPool;
    private ChangeProjectileType _changeProjectile;
    private ChangeProjectilePattern _changeProjectilePattern;
    private CharacterStats _defaultStats;
    private PlayerSkills _playerSkills;

    private CharacterController CurrentCharacterController
    {
        get
        {
            if (_characterController == null)
            {
                if (!TryGetComponent(out _characterController))
                {
                    _characterController = gameObject.AddComponent<CharacterController>();
                    return _characterController;
                }
                else
                {
                    return _characterController;
                }
            }
            return _characterController;
        }
        set { _characterController = value; }
    }

    public float ColliderRadius { get { return CurrentCharacterController.radius; } }

    private Vector2 _contextDir;

    public Vector2 MoveDirection { get { return new Vector2(_contextDir.x, _contextDir.y); } }

    [Inject]
    private void Construct(ProjectilePool projectilePool, EnemyPool enemyPool, CharacterStats stats)
    {
        _defaultStats = stats;
        _projectilePool = projectilePool;
        _enemyPool = enemyPool;
    }

    private void Awake()
    {
        _controller = new Controller();
        _weapon = new Weapon(_projectilePool.GetPool(ProjectileOwner.Player, typeDamage));
        _playerSkills = new PlayerSkills(this, _controller, _skills);

        damage = _defaultStats.Damage.CurrentValue;
        speedAttack = _defaultStats.AttackSpeed.CurrentValue;
        currentHealth = _defaultStats.MaxHP.CurrentValue;
        _moveSpeed = _defaultStats.MovementSpeed.CurrentValue;
    }

    public override void Init()
    {
        _playerSkills.ResetDelay();
        base.Init();
    }

    private void OnEnable()
    {
        _controller.Enable();
        _controller.Player.Move.performed += Move;
        _controller.Player.Move.canceled += StartWeaponAttack;
        _playerSkills.SubscribeToSkills();

        _defaultStats.MaxHP.OnChangeUpgradeLvlEvent += ChangeMaxHP;
    }

    private void OnDisable()
    {
        _playerSkills.StopDelay();
        _playerSkills.UnsubscribeToSkills();
        _controller.Player.Move.performed -= Move;
        _controller.Player.Move.canceled -= StartWeaponAttack;
        _controller.Disable();

        _defaultStats.MaxHP.OnChangeUpgradeLvlEvent -= ChangeMaxHP;
    }

    public void PlayerEnable() 
    {
        CurrentCharacterController.enabled = true;
        IsImmortal = false;
        _controller.Enable(); 
    }

    public void PlayerDisable() 
    { 
        IsImmortal = true;
        _controller.Disable();
        CurrentCharacterController.enabled = false;
    }

    public void Move(InputAction.CallbackContext context)
    {
        StopAttack();
        _contextDir = context.ReadValue<Vector2>();
    }

    public void StartWeaponAttack(InputAction.CallbackContext context)
    {
        _contextDir = Vector2.zero;
        _weapon?.StartAttack(GetEnemy, _spawnProjectile, damage, speedAttack);
    }

    public void StopAttack()
    {
        _weapon.StopAttack();
    }

    public void SetProjectilePattern(ProjectileCreationType creation, ProjectileMovementType movement, ProjectileHitType hit, float second = 0)
    {
        _changeProjectilePattern?.Stop();
        _changeProjectilePattern = new ChangeProjectilePattern(_weapon, creation, movement, hit, second);
    }

    public void SetNewProjectile(TypeDamage type, float seconds = 0)
    {
        _changeProjectile?.Stop();
        _changeProjectile = new ChangeProjectileType(ProjectileOwner.Player, _weapon, _projectilePool, typeDamage, type, seconds);
    }

    private Transform GetEnemy()
    {
        return _enemyPool.GetNearestEnemy(transform.position);
    }

    private void Update()
    {
        if (_controller.Player.Move.IsPressed())
        {
            Vector3 moveDir = new Vector3(_contextDir.x, 0, _contextDir.y);
            CurrentCharacterController.Move(moveDir * Time.deltaTime * _moveSpeed);

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDir, RotationSpeed * Time.deltaTime, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    protected override void Die()
    {
        _playerSkills.StopDelay();
        _changeProjectilePattern?.Stop();
        _changeProjectile?.Stop();
        OnPlayerDie?.Invoke(false);
        StopAttack();
        base.Die();
    }

    private void ChangeMaxHP(StatInfo info)
    {
        currentHealth = info.CurrentValue;
    }
}
