
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;


public class EnemySpawnManager : MonoBehaviour
{
    public LevelMultiplier[] _lvl;
    private int _currentLevel;
    public LvlSwithcManager lvlSwithcManager;
    private DiContainer _diContainer;
    private EnemyPool _enemyPool;
    
    [Inject]
    private void Construct(DiContainer diContainer, EnemyPool enemyPool)
    {
        _diContainer = diContainer;
        _enemyPool = enemyPool;
    }
    
    private void Start()
    {   
        SpawnEnemies(_currentLevel);
    }
    private void OnEnable()
    {
        lvlSwithcManager.OnLevelChanged += OnLevelChangedHandler;
    }

    private void OnDisable()
    {
        lvlSwithcManager.OnLevelChanged -= OnLevelChangedHandler;
    }

    private void OnLevelChangedHandler(int levelIndex)
    {
        SpawnEnemies(levelIndex);
    }
    
    private void SpawnEnemies(int currentLevel)
    {
        foreach (EnemySpawnPoint spawnPoint in _lvl[currentLevel]._enemySpawnPoints)
        {
            Enemy enemyPrefab = spawnPoint.GetEnemyPrefab();
            
            Vector3 spawnPosition = spawnPoint.GetSpawnPosition();
            
            Enemy enemy =
                _diContainer.InstantiatePrefabForComponent<Enemy>(enemyPrefab, spawnPosition, Quaternion.identity, null);
            
            _enemyPool.AddEnemy(enemy);
            
            ApplyStatsMultiplier(enemy,  _lvl[currentLevel]._statMultiplier);
        }
    }

    private void ApplyStatsMultiplier(Enemy enemy, float multiplier)
    {
        enemy.damage = Mathf.RoundToInt(enemy.damage * multiplier);
        enemy.currentHealth = Mathf.RoundToInt(enemy.currentHealth * multiplier);
        enemy.speedAttack = Mathf.RoundToInt(enemy.speedAttack * multiplier);
    }
}