using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : UnitModel
{
    public PlayerModel(float mapSize, int lauerNum, Material material) : base(mapSize, lauerNum, material)
    {
    }
    public override void ChangeTarget(Vector3 myPos)
    {
        _currentTarget = FindNearestTarget.GetNearestTarget(myPos, UnitManager.Instance.EnemiesTransforms);
    }
}
