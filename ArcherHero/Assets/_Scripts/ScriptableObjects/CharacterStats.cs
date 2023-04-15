using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "ScriptableObjects/CharacterStats", order = 3)]
public class CharacterStats : ScriptableObject
{
    [Header("HP")]
    [SerializeField] private int _maxHP;
    [SerializeField] private float _hpChange;

    [Header("Speed")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;

    [Header("Attack")]
    [SerializeField] private float _attackDellay;

    public int MaxHp => _maxHP;
    public float HPChange => _hpChange;
    public float MovementSpeed => _movementSpeed;
    public float RotationSpeed => _rotationSpeed;
    public float AttackDellay => _attackDellay;

    public void AddStats(CharacterStats stats)
    {
        _maxHP += stats.MaxHp;
        _hpChange += stats.HPChange;
        _movementSpeed += stats.MovementSpeed;
        _rotationSpeed += stats.RotationSpeed;
        _attackDellay += stats.AttackDellay;
    }
}