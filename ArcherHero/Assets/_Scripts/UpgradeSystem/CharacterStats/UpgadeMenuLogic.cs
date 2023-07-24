using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;

public class UpgadeMenuLogic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hpUpgradeText;
    [SerializeField] private TextMeshProUGUI _damageUpgradeText;
    [SerializeField] private TextMeshProUGUI _speedAttackUpgradeText;
    [SerializeField] private TextMeshProUGUI _hpPriceText;
    [SerializeField] private TextMeshProUGUI _hpValueText;
    [SerializeField] private TextMeshProUGUI _damagePriceText;
    [SerializeField] private TextMeshProUGUI _damageValueText;
    [SerializeField] private TextMeshProUGUI _speedAttackPriceText;
    [SerializeField] private TextMeshProUGUI _speedAttackValueText;
    [SerializeField] private TextMeshProUGUI _hpCurrentText;
    [SerializeField] private TextMeshProUGUI _damageCurrentText;
    [SerializeField] private TextMeshProUGUI _speedAttackCurrentText;
    [SerializeField] private TextMeshProUGUI _warningText;


    [SerializeField] private Button _hpButton;
    [SerializeField] private Button _speedAttack;
    [SerializeField] private Button _damageButton;

    [SerializeField] private StatInfo _statInfo;

    private void Start()
    {
        _hpButton.onClick.AddListener(AddHp);
        //_damageButton.onClick.AddListener(AddDamage);
        //_speedAttack.onClick.AddListener(AddSpeedAttack);  
    }

    private void AddHp()
    {
        _statInfo.UpgradeUP();
        Debug.Log(_statInfo.UnlockedLvl);
    }
}

