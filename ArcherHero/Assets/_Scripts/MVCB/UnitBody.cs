using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBody : MonoBehaviour
{
    public event Action OnDeath;

    [SerializeField] private int _maxHP = 100;
    [SerializeField] private float _movementSpeed = 2;
    [SerializeField] private float _rotationSpeed = 2;
    [SerializeField] private float _attackSpeed = 2;

    private float _currentHP;

    public int MaxHP { get { return _maxHP; } private set { _maxHP = value; } }
    public float CurrentHP { 
        get { return _currentHP; } 
        private set 
        {
            _currentHP = value;

            if (_currentHP < 0)
            {
                _currentHP = 0;
                OnDeath?.Invoke();
            }
        } 
    }

    public float MovementSpeed { get { return _movementSpeed; } private set { _movementSpeed = value; } }
    public float RotationSpeed { get { return _rotationSpeed; } private set { _rotationSpeed = value; } }
    public float AttackSpeed { get { return _attackSpeed; } private set { _attackSpeed = value; } }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    public void TakeDamage(float damage)
    {
        CurrentHP -= damage;
    }
}
