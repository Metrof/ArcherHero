using PlayerStats;
using UnityEngine;
using UnityEngine.Serialization;

public class Entity : MonoBehaviour, IMovable, IDamageable
{
    [FormerlySerializedAs("_armorTypeExp")] [SerializeField] private ArmorType armorType;

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
        int finaleDamage = DamageHandler.CalculateDamage(damage, typeDamage, armorType);

        _currentHealth -= finaleDamage;

        Debug.Log($"Enemy took {finaleDamage} damage. Current health: {_currentHealth}. Damage Type: {typeDamage}. Armor Type: {armorType}");

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {

    }
}
