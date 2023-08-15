using PlayerStats;
using UnityEngine;
using UnityEngine.Serialization;

public class Entity : MonoBehaviour, IMovable, IDamageable
{
    [FormerlySerializedAs("_armorTypeExp")] [SerializeField] private ArmorType armorType;
    [SerializeField] private float _moveSpeed = 1000;
    [SerializeField][Range(0, 1)] private float _rotationSpeed = 0.05f;
    [SerializeField] protected TypeDamage _typeDamage;
    
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
        int finaleDamage = DamageHandler.CalculateDamage(damage, typeDamage, armorType);

        currentHealth -= finaleDamage;

        //Debug.Log($"Enemy took {finaleDamage} damage. Current health: {currentHealth}. Damage Type: {typeDamage}. Armor Type: {armorType}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {

    }
}
