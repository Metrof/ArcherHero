
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;


public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private LevelMultiplier[] _lvl;
    private DiContainer _diContainer;
    private EnemyPool _enemyPool;

    private LvlSwitchManager _lvlSwitchManager;

    [Inject]
    private void Construct(DiContainer diContainer, EnemyPool enemyPool, LvlSwitchManager lvlSwitchManager)
    {
        _diContainer = diContainer;
        _enemyPool = enemyPool;
        _lvlSwitchManager = lvlSwitchManager;
        _diContainer.Bind<EnemySpawnManager>().FromInstance(this).AsSingle();
    }
    private void OnEnable()
    {
        _lvlSwitchManager.OnLevelChanged += SpawnEnemies;
    }

    private void OnDisable()
    {
        _lvlSwitchManager.OnLevelChanged -= SpawnEnemies;
    }

    public void SpawnEnemies(int lvl)
    {
        _enemyPool.ClearEnemyList();
        foreach (EnemySpawnPoint spawnPoint in _lvl[lvl]._enemySpawnPoints)
        {
            Enemy enemyPrefab = spawnPoint.GetEnemyPrefab();
            
            Vector3 spawnPosition = spawnPoint.GetSpawnPosition();
            
            Enemy enemy =
                _diContainer.InstantiatePrefabForComponent<Enemy>(enemyPrefab, spawnPosition, Quaternion.identity, null);
            
            _enemyPool.AddEnemy(enemy);
            
            ApplyStatsMultiplier(enemy,  _lvl[lvl]._statMultiplier);
        }
    }

    public void SpawnEnemies(LevelMultiplier bossEnemy)
    {
        _enemyPool.ClearEnemyList();
        foreach (EnemySpawnPoint spawnPoint in bossEnemy._enemySpawnPoints)
        {
            Enemy enemyPrefab = spawnPoint.GetEnemyPrefab();
            
            Vector3 spawnPosition = spawnPoint.GetSpawnPosition();
            
            Enemy enemy =
                _diContainer.InstantiatePrefabForComponent<Enemy>(enemyPrefab, spawnPosition, Quaternion.identity, null);
            
            _enemyPool.AddEnemy(enemy);
            
            ApplyStatsMultiplier(enemy,  bossEnemy._statMultiplier);
        }
    }

    private void ApplyStatsMultiplier(Enemy enemy, float multiplier)
    {
        enemy.damage = Mathf.RoundToInt(enemy.damage * multiplier);
        enemy.currentHealth = Mathf.RoundToInt(enemy.currentHealth * multiplier);
        enemy.speedAttack = Mathf.RoundToInt(enemy.speedAttack * multiplier);
    }
}