using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public sealed class Player : Entity
{
    public event Action<bool> OnPlayerDie;
    [SerializeField] private Transform _spawnProjectile;
    [SerializeField] private Skill _dash;

    CharacterController _characterController;
    Controller _controller;

    private Skill _firstSkill;
    private Skill _secondSkill;

    private Weapon _weapon;
    private ProjectilePool _projectilePool;
    private EnemyPool _enemyPool;

    private CharacterController CharacterController
    {
        get
        {
            if (_characterController == null)
            {
                if(!TryGetComponent(out _characterController))
                {
                    _characterController = gameObject.AddComponent<CharacterController>();
                    return _characterController;
                } else 
                { 
                    return _characterController; 
                }
            }
            return _characterController;
        }
        set { _characterController = value; }
    }

    public float ColliderRadius { get { return CharacterController.radius; } }

    Vector2 _contextDir;

    public Vector2 MoveDirection { get {  return new Vector2(_contextDir.x, _contextDir.y); } }

    public Weapon Weapon { get { return _weapon; } }

    public ProjectilePool ProjectilePool { get { return _projectilePool; } }

    [Inject]
    private void Construct(ProjectilePool projectilePool, EnemyPool enemyPool)
    {
        _projectilePool = projectilePool;
        _enemyPool = enemyPool;
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _controller = new Controller();
        _weapon = new Weapon(_projectilePool.GetPool(ProjectileOwner.Player, typeDamage));
    }
    public override void Init()
    {
        _weapon?.StartAttack(GetEnemy, _spawnProjectile, damage, speedAttack);
        SetFirstSkill(_dash);
    }
    private void OnEnable()
    {
        _controller.Enable();
        _controller.Player.Move.performed += Move;
        _controller.Player.Move.canceled += StartWeaponAttack;
        _controller.Player.ActivateFirstSkill.performed += ActivateFirstSkill;
        _controller.Player.ActivateSecondSkill.performed += ActivateSecondSkill;
    }
    private void OnDisable()
    {
        _controller.Player.Move.performed -= Move;
        _controller.Player.Move.canceled -= StartWeaponAttack;
        _controller.Player.ActivateFirstSkill.performed -= ActivateFirstSkill;
        _controller.Player.ActivateSecondSkill.performed -= ActivateSecondSkill;
        _controller.Disable();
    }
    public void PlayerEnable() { _controller.Enable(); }
    public void PlayerDisable() { _controller.Disable(); }
    public void Move(InputAction.CallbackContext context)
    {
        _weapon?.StopAttack();
        _contextDir = context.ReadValue<Vector2>();
    }
    public void StartWeaponAttack(InputAction.CallbackContext context)
    {
        _contextDir = Vector2.zero;
        _weapon?.StartAttack(GetEnemy, _spawnProjectile, damage, speedAttack); 
    }
    public void ActivateFirstSkill(InputAction.CallbackContext context)
    {
        _firstSkill?.Activate(this);
    }
    public void ActivateSecondSkill(InputAction.CallbackContext context)
    {
        _secondSkill?.Activate(this);
    }
    public void SetFirstSkill(Skill skill)
    {
        _firstSkill = skill;
    }
    public void SetSecondSkill(Skill skill)
    {
        _secondSkill = skill;
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
            CharacterController.Move(moveDir * Time.deltaTime * Speed);

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDir, RotationSpeed * Time.deltaTime, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
    protected override void Die()
    {
        OnPlayerDie?.Invoke(false);
        _weapon.StopAttack();
        base.Die();
    }
}
