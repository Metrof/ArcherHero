
using UnityEngine;


public class EnemySpawnManager : MonoBehaviour
{
    public LevelMultiplier[] _lvl;
    public int _currentLevel;
   


    private void Start()
    {   
        SpawnEnemies(_currentLevel);
    }
    
    private void SpawnEnemies(int currentLevel)
    {
        foreach (EnemySpawnPoint spawnPoint in _lvl[currentLevel - 1]._enemySpawnPoints)
        {
            Enemy enemyPrefab = spawnPoint.GetEnemyPrefab();


            if (enemyPrefab == null)
            {
                Debug.LogError("Enemy Prefab is not!");
            }

            Vector3 spawnPosition = spawnPoint.GetSpawnPosition();



            Enemy enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            
            ApplyStatsMultiplier(enemy,  _lvl[currentLevel - 1]._statMultiplier);
        }
    }

    private void ApplyStatsMultiplier(Enemy enemy, float value)
    {
        float statMultiplier = value;
            
        enemy.damage *= statMultiplier;
    }
}