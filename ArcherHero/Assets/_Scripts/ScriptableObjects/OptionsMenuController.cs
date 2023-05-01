using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ScriptableObjects;


public class OptionsMenuController : MonoBehaviour
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
    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] private MoneyCounter _moneyCounter;

    public CharacterStats GetCharacterStats() => _characterStats;


    private void Start()
    {
        UpgradeText();
        _hpButton.onClick.AddListener(AddHp);
        _damageButton.onClick.AddListener(AddDamage);
        _speedAttack.onClick.AddListener(AddSpeedAttack);  
    }

    public void AddHp()
    {  
       Debug.Log("AddHp");
       _characterStats.UpgradeHp(UpgradeReaction);
    }
    public void AddDamage()
    {  
       Debug.Log("AddDamage");
       _characterStats.UpgradeDamage(UpgradeReaction);
    }
    public void AddSpeedAttack()
    {  
       Debug.Log("SpeedAttack");
       _characterStats.UpgradeSpeedAttack(UpgradeReaction);
    }

    private void UpgradeReaction(CharacterStorePurchaseCallback callback)
    {
        switch (callback)
        {
            case CharacterStorePurchaseCallback.Upgraded:
                UpgradeText();
                _warningText.text = ("Upgrade");
                _moneyCounter.UpdateMoneyCounter();
                break;
            case CharacterStorePurchaseCallback.NotEnoughMoney:
                Debug.Log("NotEnoughMoney");
                _warningText.text = ("NotEnoughMoney");
                break;
            case CharacterStorePurchaseCallback.StatIsFull:
                Debug.Log("StatIsFull");
                _warningText.text = ("StatIsFull");
                UpgradeText();
                _moneyCounter.UpdateMoneyCounter();
                break;
        }
    }
    private void UpgradeText()
    {
        _hpCurrentText.text = $" HP = {_characterStats.MaxHp}";
        _hpUpgradeText.text = $"{_characterStats.HpUpgradeCounter} out of {_characterStats.HpUpgradeLimit}";
        _hpPriceText.text = $"Price : {_characterStats.HpUpgradePrice}";
        _hpValueText.text =$"HP +  {_characterStats.HpUpgradeValue}";

        _damageCurrentText.text = $" Damage = {_characterStats.Damage}";
        _damageUpgradeText.text = $"{_characterStats.DamageUpgradeCounter} out of {_characterStats.DamageUpgradeLimit}";
        _damagePriceText.text = $"Price : {_characterStats.DamageUpgradePrice}";
        _damageValueText.text = $"Damage + {_characterStats.DamageUpgradeValue}";
        
        _speedAttackUpgradeText.text = $"{_characterStats.SpeedAttackUpgradeCounter} out of {_characterStats.SpeedAttackUpgradeLimit}";
        _speedAttackPriceText.text = $"Price : {_characterStats.SpeedAttackUpgradePrice}";
        _speedAttackValueText.text = $"Speed Attack + {_characterStats.SpeedAttackUpgradeValue}";
        _speedAttackCurrentText.text = $"Speed Attack = {_characterStats.SpeedAttack}";      
    }
}
 