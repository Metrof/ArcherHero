using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerStats;

[Serializable]
public class StatInfo 
{
    public event Action<StatInfo> OnChangeUpgradeLvlEvent;

    [SerializeField] private Image _icon;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _unlockedLvl;
    [SerializeField] private List<StatUpgradeInfo> _upgradeList;

    public Image Icon { get => _icon; }
    public string Name { get => _name; }
    public string Description { get => _description; }
    public bool IsUnlocked { get => _unlockedLvl >= _upgradeList.Count - 1; }
    public int UnlockedLvl { get => _unlockedLvl; }
    public int MaxUpgradeLvl { get => _upgradeList.Count - 1; }
    public int CurrentValue { get => GetValue(_unlockedLvl); }
    public int NextValue { get => IsUnlocked ? CurrentValue : GetValue(_unlockedLvl + 1); } 
    public int UpgradePrice { get => IsUnlocked ? -1 : _upgradeList[_unlockedLvl + 1].UnlockedCost; }
   
    public void InitializeUnlockedLvl(int value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }
        if (value > _upgradeList.Count - 1)
        {
            _unlockedLvl = _upgradeList.Count - 1;
        }
        else
        {
            _unlockedLvl = value;
        }
    }

    public void UpgradeUP()
    {
        if (!IsUnlocked)
        {
            _unlockedLvl++;
            
            OnChangeUpgradeLvlEvent?.Invoke(this);
        }
    }

    private int GetValue(int index)
    {
        return _upgradeList[index].Value;
    }
}
