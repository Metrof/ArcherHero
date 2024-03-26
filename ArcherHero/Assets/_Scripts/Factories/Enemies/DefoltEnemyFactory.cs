using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class DefoltEnemyFactory : AbstractEnemyFactory
{
    public DefoltEnemyFactory(EnemyTypes factoryType, Vector3 enemyPullPos) : base(factoryType, enemyPullPos)
    {

    }

    public override EnemyController CreateEnemy(DiContainer container, EnemyController enemyPref, Transform enemyAnchor, CharacterStatsE stats, float scaleStats)
    {
        EnemyController enemyController = container.InstantiatePrefabForComponent<EnemyController>(enemyPref, enemyAnchor);
        var model = new EnemyModel(enemyController.UnitManager, enemyController.gameObject.layer, enemyController.GetComponent<Renderer>().material);
        var view = enemyController.GetComponent<EnemyView>();
        var enemyStats = stats;
        enemyStats.ScaleStats(scaleStats);

        enemyController.Init(view, model, _pullPos);
        enemyController.SetEnemyType(enemyController.EnemyType);
        enemyController.SetNewModelParram(enemyStats);
        enemyController.LvlStart();

        return enemyController;
    }
}
