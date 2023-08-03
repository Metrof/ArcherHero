
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int _currentHealth = 1000;
    [SerializeField] private ArmorTypeExp _armorTypeExp;
    //private IDamageable _damageableImplementation;
    
    public void TakeDamage(int damageAmount, DamageTypeExp damageTypeExp)
    {
        int damage = DamageHandler.CalculateDamage(damageAmount, damageTypeExp, _armorTypeExp);
        
        _currentHealth -= damage;
        
        Debug.Log($"Enemy took {damage} damage. Current health: {_currentHealth}. Damage Type: {damageTypeExp}. Armor Type: {_armorTypeExp}");

        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        Debug.Log("Dead enemy");
    }
}