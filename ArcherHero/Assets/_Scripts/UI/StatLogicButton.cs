using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;

public class StatLogicButton : MonoBehaviour
{
    [SerializeField] private Button _hpButton;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _currentText;
    [SerializeField] private TextMeshProUGUI _upgradeText;
    [SerializeField] private TextMeshProUGUI _nextValueText;
    [SerializeField] private StatInfo _statInfo;

    void Start()
    {
        _hpButton.onClick.AddListener(AddStat);
        UpdateText();
    }

    private void AddStat()
    {  
        _statInfo.UpgradeUP();
        UpdateText();
    }

    private void UpdateText()
    {
        string statName = _statInfo.Name;
        int currentLevel = _statInfo.UnlockedLvl;
        int maxLevel = _statInfo.MaxUpgradeLvl;
        int upgradePrice = _statInfo.UpgradePrice;
        int nextValue = _statInfo.NextValue;

        _currentText.text = $"Current {statName} {_statInfo.CurrentValue}";
        _upgradeText.text = $"{currentLevel} out of {maxLevel}";

        if (currentLevel < maxLevel)
        {
            _priceText.text = upgradePrice > 0 ? $"Price: {upgradePrice}" : "Max Level Reached";
            _nextValueText.text = $"Next {statName} {nextValue}";
        }
        else
        {   
            _priceText.text = "Level Reached";
            _nextValueText.text = "Max";
            _hpButton.interactable = false;
        }  
    }
}
