using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "ScriptableObjects/CharacterStatsE", order = 3)]
public class CharacterStatsE : ScriptableObject
{
    [Header("HP")]
    [SerializeField] private float _maxHP;
    [SerializeField] private float _hpChange;

    [Header("Speed")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;

    [Header("Attack")]
    [SerializeField] private float _attackDellay;

    public float MaxHp => _maxHP;
    public float HPChange => _hpChange;
    public float MovementSpeed => _movementSpeed;
    public float RotationSpeed => _rotationSpeed;
    public float AttackDellay => _attackDellay;

    public void AddStats(CharacterStatsE stats)
    {
        _maxHP += stats.MaxHp;
        _hpChange += stats.HPChange;
        _movementSpeed += stats.MovementSpeed;
        _rotationSpeed += stats.RotationSpeed;
        _attackDellay += stats.AttackDellay;
    }
    public void ScaleStats(float scale)
    {
        _maxHP *= scale;
    }
}