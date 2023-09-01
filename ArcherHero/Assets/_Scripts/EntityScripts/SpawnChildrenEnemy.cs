
using Zenject;
using UnityEngine;

public class SpawnChildrenEnemy : MonoBehaviour
{
    [SerializeField] private LevelMultiplier _lvl;
    private EnemySpawnManager _enemySpawnManager;

    [Inject]
    private void Construct(EnemySpawnManager enemySpawnManager)
    {
        _enemySpawnManager = enemySpawnManager;
    }
    
    public void OnBossDestroyed()
    {
        _enemySpawnManager.SpawnEnemies(_lvl);
    }
}
