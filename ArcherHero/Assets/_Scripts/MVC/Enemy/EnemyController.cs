using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyController : UnitController<EnemyView, EnemyModel>
{
    [SerializeField] private EnemyTypes _type = EnemyTypes.DefoltRobot;
    [SerializeField] private int _expForDie = 100;
    [SerializeField] private int _goldForDie = 20;

    public EnemyTypes EnemyType { get { return _type; } }

    private DataHolderTestZ _holderTestZ;
    private UnitManager _unitManager;

    public UnitManager UnitManager { get { return _unitManager; } }

    [Inject]
    private void Construct(DataHolderTestZ holderTestZ, UnitManager unitManager)
    {
        _holderTestZ = holderTestZ;
        _unitManager = unitManager;
    }

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
        _holderTestZ.AddLvlMinedBounty(GetBounty());
        _unitManager.CheckLastEnemy();
    }
}
