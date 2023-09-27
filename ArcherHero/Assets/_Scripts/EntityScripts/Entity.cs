using System;
using UnityEngine;
using Zenject;


public class Entity : MonoBehaviour, IMovable, IDamageable
{   
    public event Action<int> OnTakeDamage;

    [Header("Type Armament")]
    [SerializeField] private TypeArmor typeArmor;
    [SerializeField] protected TypeDamage typeDamage;
    
    [Header("Movement")]
    [SerializeField] protected float _moveSpeed = 1000;
    [SerializeField] private float _rotationSpeed = 0.05f;
    
    [Header("Stats")]
    public int damage;
    public int speedAttack;
    public int currentHealth;

    public bool IsImmortal { get ; protected set; }
    public float Speed { get { return _moveSpeed; } private set { _moveSpeed = value; } }
    public float RotationSpeed { get { return _rotationSpeed; } private set { _rotationSpeed = value; } }

    private AudioManager _audioManager; 

    [Inject]
    private void Construct(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }
    public virtual void Init()
    {

    }   

    public void TakeDamage(TypeDamage typeDamage, int damage)
    {
        if (IsImmortal)
        {
            return;
        }

        int finaleDamage = DamageHandler.CalculateDamage(damage, typeDamage, typeArmor);
        currentHealth -= finaleDamage;
        OnTakeDamage?.Invoke(currentHealth);

        _audioManager.Play(0);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        _audioManager.Play(2);      
    }
}
