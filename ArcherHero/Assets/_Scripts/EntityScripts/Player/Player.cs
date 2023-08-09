using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Player : Entity
{
    [SerializeField] private Transform _spawnProjectile;

    Controller _controller;
    private Tween _moveTween;

    private Weapon _weapon;
    private ProjectilePool _projectilePool;
    private EnemyPool _enemyPool;

    Vector2 _contextDir;
    [Inject]
    private void Construct(ProjectilePool projectilePool, EnemyPool enemyPool)
    {
        _projectilePool = projectilePool;
        _enemyPool = enemyPool;
    }

    private void Awake()
    {
        _controller = new Controller();
        _weapon = new Weapon(_projectilePool.GetPool(ProjectileOwner.Player, _typeDamage));
    }
    private void OnEnable()
    {
        _controller.Enable();
        _controller.Player.Move.performed += Move;
        _controller.Player.Move.canceled += StartWeaponAttack;
    }
    private void OnDisable()
    {
        _controller.Player.Move.canceled -= StartWeaponAttack;
        _controller.Player.Move.performed -= Move;
        _controller.Disable();
    }
    public void Move(InputAction.CallbackContext context)
    {
        _weapon.StopAttack();
        _contextDir = context.ReadValue<Vector2>();

        //.OnComplete() => transform.gameObject.SetActive(false); метод отрабатывает при завершении цикла Tweena
    }
    public void StartWeaponAttack(InputAction.CallbackContext context)
    {
        _weapon.StartAttack(GetEnemy, _spawnProjectile, damage, speedAttack); // fixThis
    }

    private Transform GetEnemy()
    {
        return _enemyPool.GetNearestEnemy(transform.position);
    }

    private void Update()
    {
        _moveTween.Kill();
        if (_controller.Player.Move.IsPressed())
        {
            Vector3 moveDir = new Vector3(_contextDir.x, 0, _contextDir.y);
            _moveTween = transform.DOMove(moveDir, Time.deltaTime * Speed).SetSpeedBased().SetEase(Ease.Linear).SetRelative();

            transform.LookAt(Vector3.LerpUnclamped(transform.forward + transform.position, moveDir + transform.position, RotationSpeed));
        }
    }
    protected override void Die()
    {
        _weapon.StopAttack();
        base.Die();
    }
}
