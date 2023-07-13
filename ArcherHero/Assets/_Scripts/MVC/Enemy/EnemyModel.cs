using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : UnitModel
{
    public EnemyModel(UnitManager unitManager, int lauerNum, Material material) : base(unitManager, lauerNum, material)
    {
        _unitManager = unitManager;
    }
    public override void ChangeTarget(Vector3 myPos)
    {
        _currentTarget = _unitManager.PlayerTransform;
    }
}
