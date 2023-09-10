using UnityEngine;

[System.Serializable]
public class DataItemAndDropRate
{
    [SerializeField]
    private DropItem _dropItemPrefab;

    [SerializeField, Range(0f, 1f)]
    private float _dropChance;

    public float DropChance { get => _dropChance; }
    public DropItem DropItem { get => _dropItemPrefab; }
}
