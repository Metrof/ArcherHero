using System;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public LevelMultiplier[] _lvl;
    public int _currentLevel;


    private void Start()
    {
        StartLevel(_currentLevel);
    }

    private void StartLevel(int lvl)
    {
        SpawnEnemies(lvl);
    }

    private void SpawnEnemies(int currentLevel)
    {
        foreach (EnemySpawnPoint spawnPoint in _lvl[currentLevel - 1]._enemySpawnPoints)
        {
            GameObject enemyPrefab = spawnPoint.GetEnemyPrefab();
            
            
            if (enemyPrefab == null)
            {
                Debug.LogError("Enemy Prefab is not!");
            }

            Vector3 spawnPosition = spawnPoint.GetSpawnPosition();
           
            
            
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

           
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                float statMultiplier = _lvl[currentLevel - 1]._statMultiplier;
  
                enemyComponent.damage *= statMultiplier;
            }
            else
            {
                Debug.LogError("Enemy component not found on the enemy prefab!");
            }
        }
    }









            //ApplyStatsMultiplier(enemy, _lvl.enemyStatsMultiplier);
        
    

    /*private void ApplyStatsMultiplier(GameObject enemy, object enemyStatsMultiplier)
    {
        throw new NotImplementedException();
    }*/
}