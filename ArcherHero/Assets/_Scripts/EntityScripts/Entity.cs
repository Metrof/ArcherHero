
using System;
using UnityEngine;


public class Entity : MonoBehaviour, IMovable, IDamageable
{   
    public event Action<int> OnTakeDamage;

    [Header("Type Armament")]
    [SerializeField] private TypeArmor typeArmor;
    [SerializeField] protected TypeDamage typeDamage;
    
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 1000;
    [SerializeField] private float _rotationSpeed = 0.05f;
    
    [Header("Stats")]
    public int damage;
    public int speedAttack;
    public int currentHealth;
    

    public float Speed { get { return _moveSpeed; } private set { _moveSpeed = value; } }
    public float RotationSpeed { get { return _rotationSpeed; } private set { _rotationSpeed = value; } }

    public virtual void Init()
    {

    }

    public void TakeDamage(TypeDamage typeDamage, int damage)
    {
        int finaleDamage = DamageHandler.CalculateDamage(damage, typeDamage, typeArmor);
        currentHealth -= finaleDamage;
        OnTakeDamage?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {

    }
}
