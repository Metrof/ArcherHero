using PlayerStats;
using UnityEngine;
using UnityEngine.Serialization;

public class Entity : MonoBehaviour, IMovable, IDamageable
{
    [FormerlySerializedAs("_armorTypeExp")] [SerializeField] private ArmorType armorType;
    protected TypeDamage _typeDamage;

    private CharacterStats _stats;
    
    public int damage;
    public int speedAttack;
    public int currentHealth;
    
    
    public float Speed {  get; private set; }

    public virtual void Init(CharacterStats stats)
    {
        _stats = stats;
        currentHealth = _stats.MaxHP.CurrentValue;
    }

    public void TakeDamage(TypeDamage typeDamage, int damage)
    {
        int finaleDamage = DamageHandler.CalculateDamage(damage, typeDamage, armorType);

        currentHealth -= finaleDamage;

        Debug.Log($"Enemy took {finaleDamage} damage. Current health: {currentHealth}. Damage Type: {typeDamage}. Armor Type: {armorType}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {

    }
}
