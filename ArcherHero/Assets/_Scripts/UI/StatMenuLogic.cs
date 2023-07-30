using PlayerStats;
using UnityEngine;
using Zenject;

public class StatMenuLogic : MonoBehaviour
{
    [SerializeField] private StatButton _prefabButton;
    [SerializeField] private RectTransform _containerButtons;
    private CharacterStats _characterStats;
    
    [Inject]
    void Construct(CharacterStats characterStats)
    { 
        _characterStats = characterStats;  
    }

    void Start()
    {
        //SubscribeButtons();
        //UpdateButtons();
        CreateButtons();
    }
    private void CreateButtons()
    {
        foreach(var stat in _characterStats.Stats)
        {
            StatButton button = Instantiate(_prefabButton, _containerButtons);

            UpdateButton(stat);

            button.Button.onClick.AddListener(stat.UpgradeUP);
            stat.OnChangeUpgradeLvlEvent += UpdateButton;

            void UpdateButton(StatInfo info)
            {
                button.NextValueText.text = info.NextValue.ToString();
                button.PriceText.text = info.UpgradePrice.ToString();

                button.CurrentText.text = $"Current {info.Name} {info.CurrentValue}";
                button.LimitUpgradesText.text = $"{info.UnlockedLvl} out of {info.MaxUpgradeLvl}";

                if (!info.IsUnlocked)
                {
                    button.PriceText.text = info.UpgradePrice > 0 ? $"Price: {info.UpgradePrice}" : "Max Level Reached";
                    button.NextValueText.text = $"Next {info.Name} {info.NextValue}";
                }
                else
                {
                    button.PriceText.text = "Level Reached";
                    button.NextValueText.text = "Max";
                    button.Button.interactable = false;
                }
            }

        }
    }

    //private void AddDamage(StatInfo info)
    //{
    //    UpdateButtonInfo(info, _buttonDamage);
    //}

    //private void UpdateButtons()
    //{
    //    UpdateButtonInfo(_characterStats.Damage, _buttonDamage);

    //}

    //private static void UpdateButtonInfo(StatInfo statInfo, UI_Button uiButton)
    //{
    //    string statName = statInfo.Name;
    //    int currentLevel = statInfo.UnlockedLvl;
    //    int maxLevel = statInfo.MaxUpgradeLvl;
    //    int upgradePrice = statInfo.UpgradePrice;
    //    int nextValue = statInfo.NextValue;

    //    uiButton.CurrentText.text = $"Current {statName} {statInfo.CurrentValue}";
    //    uiButton.LimitUpgradesText.text = $"{currentLevel} out of {maxLevel}";

    //    if (!statInfo.IsUnlocked)
    //    {
    //        uiButton.PriceText.text = upgradePrice > 0 ? $"Price: {upgradePrice}" : "Max Level Reached";
    //        uiButton.NextValueText.text = $"Next {statName} {nextValue}";
    //    }
    //    else
    //    {
    //        uiButton.PriceText.text = "Level Reached";
    //        uiButton.NextValueText.text = "Max";
    //        uiButton.Button.interactable = false;
    //    }
    //}

    //private void OnDisable()
    //{
    //    UnDescribeButtons();
    //}

    //private void SubscribeButtons()
    //{
    //    _buttonDamage.Button.onClick.AddListener(_characterStats.Damage.UpgradeUP);
    //    _characterStats.Damage.OnChangeUpgradeLvlEvent += AddDamage;
    //}

    //private void UnDescribeButtons()
    //{
    //    _buttonDamage.Button.onClick.RemoveListener(_characterStats.Damage.UpgradeUP);
    //    _characterStats.Damage.OnChangeUpgradeLvlEvent -= AddDamage;
    //}
}
