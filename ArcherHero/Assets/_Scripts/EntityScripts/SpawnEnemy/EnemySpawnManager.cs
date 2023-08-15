
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;


public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private LevelMultiplier[] _lvl;
    [SerializeField] private LvlSwithcManager lvlSwithcManager;
    private int _currentLevel;
    private DiContainer _diContainer;
    private EnemyPool _enemyPool;
    
    [Inject]
    private void Construct(DiContainer diContainer, EnemyPool enemyPool)
    {
        _diContainer = diContainer;
        _enemyPool = enemyPool;
        diContainer.Bind<EnemySpawnManager>().FromInstance(this).AsSingle();
    }
    
    /*private void OnEnable()
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
    }*/
    
    public LvlSwithcManager LvlSwithcManager { get { return lvlSwithcManager; } }
    public void SpawnEnemies()
    {
        foreach (EnemySpawnPoint spawnPoint in _lvl[_currentLevel]._enemySpawnPoints)
        {
            Enemy enemyPrefab = spawnPoint.GetEnemyPrefab();
            
            Vector3 spawnPosition = spawnPoint.GetSpawnPosition();
            
            Enemy enemy =
                _diContainer.InstantiatePrefabForComponent<Enemy>(enemyPrefab, spawnPosition, Quaternion.identity, null);
            
            _enemyPool.AddEnemy(enemy);
            
            ApplyStatsMultiplier(enemy,  _lvl[_currentLevel]._statMultiplier);
        }
    }

    private void ApplyStatsMultiplier(Enemy enemy, float multiplier)
    {
        enemy.damage = Mathf.RoundToInt(enemy.damage * multiplier);
        enemy.currentHealth = Mathf.RoundToInt(enemy.currentHealth * multiplier);
        enemy.speedAttack = Mathf.RoundToInt(enemy.speedAttack * multiplier);
    }
}