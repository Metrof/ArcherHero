
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class XPBar : MonoBehaviour
{   
    //[SerializeField] private CharacterSkills _characterSkills;
    [SerializeField] private TextMeshProUGUI _carrentXPText;
    [SerializeField] private TextMeshProUGUI _carrentLevelText;
    private HeroLVL _heroLvl;

    
    [Inject]
    void Construct (HeroLVL heroLVL)
    {
        _heroLvl = heroLVL;
    }
    
    public Slider slider;
    
    void Awake()
    {
        Slider slider = GetComponent<Slider>();
    }
    void Start()
    {
       // UpgradeText();
       ChangeBar();
    }
   /* public void UpgradeText()
    {
        _carrentLevelText.text = $"{_carrentLevelText}";
        _carrentLevelText.text =$"{_characterSkills.CurrentLevel}";
        _carrentXPText.text =  $"{_characterSkills.CurrentXP} / {_characterSkills.XPToLevelUp}";
    }*/
    
    public void ChangeBar()
    {   
        _carrentLevelText.text =$"{_heroLvl.CurrentLvl}";
        _carrentXPText.text =  $"{_heroLvl.CurrentLvl} / {_heroLvl.ToLevelUP}";
        
        //slider.value = (float)_characterSkills.CurrentXP / _characterSkills.XPToLevelUp;

        //_characterSkills.GainExperience(5);  // прибавляю опыт
    }   
}
