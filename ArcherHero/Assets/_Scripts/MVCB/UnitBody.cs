using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBody : MonoBehaviour
{
    public delegate void AttackDelegate(Vector3 shotPos);
    public event AttackDelegate OnAttack;

    public event Action OnDeath;

    [SerializeField] private int _maxHP = 100;
    [SerializeField] private float _movementSpeed = 2;
    [SerializeField] private float _rotationSpeed = 2;
    [SerializeField] private float _attackDellay = 2;

    private float _currentHP;

    public int MaxHP { get { return _maxHP; } private set { _maxHP = value; } }
    public Vector3 Position { get { return transform.position; } }
    public float CurrentHP { 
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
    public float MovementSpeed { get { return _movementSpeed; } private set { _movementSpeed = value; } }
    public float RotationSpeed { get { return _rotationSpeed; } private set { _rotationSpeed = value; } }
    public float AttackSpeed { get { return _attackDellay; } private set { _attackDellay = value; } }

    private void Start()
    {
        _currentHP = MaxHP;
    }
    public void Teleportation(Vector3 newPos)
    {
        transform.position = newPos;
    }


    public void StartAttacking()
    {
        StartCoroutine(AttackCorotine());
    }
    public void StopAttacking()
    {
        StopCoroutine(AttackCorotine());
    }
    IEnumerator AttackCorotine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackDellay);
            OnAttack?.Invoke(Position);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Projectile projectile))
        {
            foreach (var effect in projectile.Effects)
            {
                effect.GetEffect(this);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHP -= damage;
    }
}
