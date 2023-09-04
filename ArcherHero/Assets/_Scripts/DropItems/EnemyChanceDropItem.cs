using System.Collections.Generic;
using UnityEngine;

public class EnemyChanceDropItem : MonoBehaviour
{
    [SerializeField]
    private List<DataItemAndDropRate> _data;
    [SerializeField]
    private Enemy _owner;

    private void OnEnable()
    {
        _owner.OnEnemyDie += DropItem;
    }

    private void OnDisable()
    {
        _owner.OnEnemyDie -= DropItem;
    }

    void DropItem(Enemy enemy)
    {
        for (int i = 0; i < _data.Count; i++)
        {
            float randomChance = Random.Range(0f, 1f);

            if (randomChance <= _data[i].DropChance)
            {
                Instantiate(_data[i].DropItem, transform.position, Quaternion.identity);
                break;
            }
        }
    }
}
