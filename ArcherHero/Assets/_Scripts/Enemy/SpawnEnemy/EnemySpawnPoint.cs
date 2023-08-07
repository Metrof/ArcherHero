
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    
    [SerializeField] private Enemy _enemyPrefab;
    
    public Vector3 GetSpawnPosition()
    {
        return transform.position;
    }

    public Enemy GetEnemyPrefab()
    {
        return _enemyPrefab;
    }
}
