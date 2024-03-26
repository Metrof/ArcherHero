using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : UnitModel
{
    public delegate void HPDelegate(float hp, float maxHp);
    public event HPDelegate OnTakeDamage;
    public PlayerModel(UnitManager unitManager, int lauerNum, Material material) : base(unitManager, lauerNum, material)
    {
    }
    public override void ChangeStats(CharacterStatsE stats)
    {
        base.ChangeStats(stats);
        OnTakeDamage?.Invoke(_currentHP, _maxHP);
    }
    public override void ChangeTarget(Vector3 myPos)
    {
        _currentTarget = FindNearestTarget.GetNearestTarget(myPos, _unitManager.WaveEnemiesTransforms);
    }
}
