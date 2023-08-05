
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    
    [SerializeField] private GameObject _enemyPrefab;
    
    public Vector3 GetSpawnPosition()
    {
        return transform.position;
    }

    public GameObject GetEnemyPrefab()
    {
        return _enemyPrefab;
    }
}
