using PlayerStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IMovable, IDamageable
{
    [SerializeField] private ArmorTypeExp _armorTypeExp;

    private CharacterStats _stats;

    private int _currentHealth;

    public float Speed {  get; private set; }

    public virtual void Init(CharacterStats stats)
    {
        _stats = stats;
        _currentHealth = _stats.MaxHP.CurrentValue;
    }

    public void TakeDamage(TypeDamage typeDamage, int damage)
    {
        int finaleDamage = DamageHandler.CalculateDamage(damage, typeDamage, _armorTypeExp);

        _currentHealth -= finaleDamage;

        Debug.Log($"Enemy took {finaleDamage} damage. Current health: {_currentHealth}. Damage Type: {typeDamage}. Armor Type: {_armorTypeExp}");

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {

    }
}
