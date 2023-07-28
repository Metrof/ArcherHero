using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class StatButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _nextValueText;
    [SerializeField] private TextMeshProUGUI _limitUpgrade;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _currentText;

    public Button Button => _button;
    public TextMeshProUGUI NextValueText => _nextValueText;
    public TextMeshProUGUI LimitUpgradesText => _limitUpgrade;
    public TextMeshProUGUI PriceText => _priceText;
    public TextMeshProUGUI CurrentText => _currentText;

} 
