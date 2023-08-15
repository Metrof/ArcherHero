using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Player : Entity
{
    public event Action<bool> OnPlayerDie;
    [SerializeField] private Transform _spawnProjectile;

    CharacterController _characterController;
    Controller _controller;

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

    Vector2 _contextDir;
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
        _weapon = new Weapon(_projectilePool.GetPool(ProjectileOwner.Player, _typeDamage));
    }
    public override void Init()
    {
        _weapon?.StartAttack(GetEnemy, _spawnProjectile, damage, speedAttack);
    }
    private void OnEnable()
    {
        _controller.Enable();
        _controller.Player.Move.performed += Move;
        _controller.Player.Move.canceled += StartWeaponAttack;
    }
    private void OnDisable()
    {
        _controller.Player.Move.performed -= Move;
        _controller.Player.Move.canceled -= StartWeaponAttack;
        _controller.Disable();
    }
    public void Move(InputAction.CallbackContext context)
    {
        _weapon?.StopAttack();
        _contextDir = context.ReadValue<Vector2>();
    }
    public void StartWeaponAttack(InputAction.CallbackContext context)
    {
        _weapon?.StartAttack(GetEnemy, _spawnProjectile, damage, speedAttack); 
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

            transform.LookAt(Vector3.LerpUnclamped(transform.forward + transform.position, moveDir + transform.position, RotationSpeed));
        }
    }
    protected override void Die()
    {
        OnPlayerDie?.Invoke(false);
        _weapon.StopAttack();
        base.Die();
    }
}
