using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : UnitController<EnemyView, EnemyModel>
{
    public delegate void EnemyControllerDelegate();
    public event EnemyControllerDelegate OnLastEnemyDeath;
    [SerializeField] private EnemyTypes _type;

    public EnemyTypes Type { get { return _type; } }


    public void SetEnemyType(EnemyTypes type)
    {
        _type = type;
    }
    protected override void Death()
    {
        base.Death();
        TransformToDefoltPos();
        if(!UnitManager.Instance.AnySurvivingEnemies()) OnLastEnemyDeath?.Invoke();
    }
}
