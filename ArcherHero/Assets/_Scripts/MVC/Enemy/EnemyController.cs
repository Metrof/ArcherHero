using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : UnitController<EnemyView, EnemyModel>
{
    public delegate void EnemyControllerDelegate();
    public event EnemyControllerDelegate OnLastEnemyDeath;
    [SerializeField] private EnemyTypes _type;
    [SerializeField] private int _expForDie;
    [SerializeField] private int _goldForDie;

    public EnemyTypes Type { get { return _type; } }


    public void SetEnemyType(EnemyTypes type)
    {
        _type = type;
        transform.position = Vector3.zero;
    }
    private PlayerBountyStruct GetBounty()
    {
        return new PlayerBountyStruct(_expForDie, _goldForDie);
    }
    protected override void Death()
    {
        base.Death();
        TransformToDefoltPos();
        DataHolder.AddLvlMinedBounty(GetBounty());
        if(!UnitManager.Instance.AnySurvivingEnemies()) OnLastEnemyDeath?.Invoke();
    }
}
