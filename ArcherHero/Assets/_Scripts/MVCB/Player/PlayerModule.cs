using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : UnitModel
{
    private int _currentLvl;
    private float _exp;
    private float _expForNextLvl;

    public PlayerModel(float mapSize) : base(mapSize)
    {
    }
}
