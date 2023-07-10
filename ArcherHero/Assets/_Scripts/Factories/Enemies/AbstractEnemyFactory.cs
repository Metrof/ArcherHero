using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemyFactory 
{
    protected EnemyTypes _enemyType;
    protected Vector3 _pullPos;
    //В будущем создать класс поведения врагов и поменять на него тип переменной "enemyBehavior"
    protected EnemyController _enemyBehavior;
    protected EnemyController _createdEnemy;

    public EnemyTypes EnemyType { get { return _enemyType; } }

    public AbstractEnemyFactory(EnemyTypes factoryType, Vector3 enemyPullPos)
    {
        _pullPos = enemyPullPos;
        _enemyType = factoryType;
    }
    public abstract void CreateEnemy(GameObject enemyPref, CharacterStatsE stats, float scaleStats);
    public abstract EnemyController GetEnemy();
}
