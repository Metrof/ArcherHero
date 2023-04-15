
using TMPro;
using UnityEngine;
using ScriptableObjects;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] private TextMeshProUGUI _moneyText;

    private void Start()
    {
        UpdateMoneyCounter();
    }

    public void UpdateMoneyCounter()
    {
        _moneyText.text = $"Money: {_characterStats.Money}";
    }
}
