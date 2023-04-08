using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : UnitModel
{
    private int _currentLvl;
    private float _exp;
    private float _expForNextLvl;

    private List<EnemyView> _lvlEnemyPull;
    private EnemyView _currentTarget;

    public void SetPull(List<EnemyView> lvlEnemyPull)
    {
        _lvlEnemyPull = lvlEnemyPull;
    }
    private void ChangeTarget()
    {

    }
}
