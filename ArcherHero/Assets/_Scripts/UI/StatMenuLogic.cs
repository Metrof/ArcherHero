using PlayerStats;
using UnityEngine;
using Zenject;
using TMPro;

public class StatMenuLogic : MonoBehaviour
{
    [SerializeField] private StatButton _prefabButton;
    [SerializeField] private RectTransform _containerButtons;
    [SerializeField] private TextMeshProUGUI _textInfo;
    private CharacterStats _characterStats;
    private ResourceBank _resourceBank;
    
    [Inject]
    void Construct(CharacterStats characterStats, ResourceBank resourceBank)
    { 
        _characterStats = characterStats; 
        _resourceBank = resourceBank; 
    }

    void Start()
    {
        CreateButtons();
    }
    private void CreateButtons()
    {
        foreach(var stat in _characterStats.Stats)
        {
            StatButton button = Instantiate(_prefabButton, _containerButtons);

            UpdateButton(stat);

            button.Button.onClick.AddListener(() => BuyStat(stat));

            stat.OnChangeUpgradeLvlEvent += UpdateButton;


            void UpdateButton(StatInfo info)
            {
                button.NextValueText.text = info.NextValue.ToString();
                button.PriceText.text = info.UpgradePrice.ToString();

                button.CurrentText.text = $"Current {info.Name} {info.CurrentValue}";
                button.LimitUpgradesText.text = $"{info.UnlockedLvl} out of {info.MaxUpgradeLvl}";

                _textInfo.text = $" Money: " + _resourceBank.GetResourceValue(ResourceType.Money).ToString();



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

        void BuyStat(StatInfo stat)
        {   
            if(_resourceBank.TrySpendResource(ResourceType.Money, stat.UpgradePrice))
            {
                stat.UpgradeUP();
            }
            else
            {
                _textInfo.text = $" not enough money ";
            }
            
        }
    }
}
