
using UnityEngine;
using UnityEngine.Serialization;


public class EnemySpawnManager : MonoBehaviour
{
    public LevelMultiplier[] _lvl;
    private int _currentLevel;
    public LvlSwithcManager lvlSwithcManager;
   
    
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
            
            Enemy enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            
            ApplyStatsMultiplier(enemy,  _lvl[currentLevel]._statMultiplier);
        }
    }

    private void ApplyStatsMultiplier(Enemy enemy, float value)
    {
        float statMultiplier = value;
            
        enemy.damage *= statMultiplier;
    }
}