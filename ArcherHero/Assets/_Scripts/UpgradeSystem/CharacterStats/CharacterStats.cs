using UnityEngine;

namespace PlayerStats
{

[CreateAssetMenu(fileName = "CharacterStats", menuName = "ScriptableObjects/CharacterStats")]
public class CharacterStats : ScriptableObject
{
    [SerializeField] private StatInfo _maxHP;
    [SerializeField] private StatInfo _damage;
    [SerializeField] private StatInfo _attackSpeed;
    [SerializeField] private StatInfo _movementSpeed;

    public StatInfo MaxHP => _maxHP;
    public StatInfo Damage => _damage;
    public StatInfo AttackSpeed => _attackSpeed;
    public StatInfo MovementSpeed => _movementSpeed;
}
}