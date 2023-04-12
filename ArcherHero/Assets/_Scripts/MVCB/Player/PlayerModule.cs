using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : UnitModel
{
    public PlayerModel(float mapSize, int lauerNum) : base(mapSize, lauerNum)
    {
    }
    public override void Attack(Vector3 shotPos)
    {
        base.Attack(shotPos);
    }
}
