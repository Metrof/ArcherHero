using UnityEngine;


[System.Serializable]
public class StatUpgradeInfo
{
    [SerializeField] private int _value;
    [SerializeField] private int _unlockCost;

    public int Value => _value;

    public int UnlockedCost => _unlockCost;
}
