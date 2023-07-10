using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : UnitModel
{
    public EnemyModel(int lauerNum, Material material) : base(lauerNum, material)
    {

    }
    public override void ChangeTarget(Vector3 myPos)
    {
        _currentTarget = UnitManager.Instance.PlayerTransform;
    }
}
