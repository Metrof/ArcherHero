using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPBar : MonoBehaviour
{   
    [SerializeField] private CharacterSkills _characterSkills;
    [SerializeField] private TextMeshProUGUI _carrentXPText;
    [SerializeField] private TextMeshProUGUI _carrentLevelText;
    
    public CharacterSkills GetCharacterSkills() => _characterSkills;
    
    public Slider slider;
    
    void Awake()
    {
        Slider slider = GetComponent<Slider>();
        UpgradeText();
    }
    public void UpgradeText()
    {
        _carrentLevelText.text = $"{_carrentLevelText}";
        _carrentLevelText.text =$"{_characterSkills.CurrentLevel}";
        _carrentXPText.text =  $"{_characterSkills.CurrentXP} / {_characterSkills.XPToLevelUp}";
    }
    
    public void ChangeBar()
    {   
        _carrentLevelText.text =$"{_characterSkills.CurrentLevel}";
        _carrentXPText.text =  $"{_characterSkills.CurrentXP} / {_characterSkills.XPToLevelUp}";
        
        slider.value = (float)_characterSkills.CurrentXP / _characterSkills.XPToLevelUp;

        _characterSkills.GainExperience(5);  // прибавляю опыт
    }   
}
