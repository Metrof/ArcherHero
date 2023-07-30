
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;


public class XPBar : MonoBehaviour
{   
    [SerializeField] private Button _button;
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
       ChangeBar();
       _heroLvl.OnExperienceAddedEvent += ChangeXP;
       _heroLvl.OnIncreasedLevelEvent += ChangeLvl;
       _button.onClick.AddListener(AddXP);       
    }

    private void ChangeXP(int changeXP)
    {
        _carrentXPText.text = $"{ changeXP.ToString()} / {_heroLvl.ToLevelUP}";
        slider.value = (float)_heroLvl.CurrentExperience / _heroLvl.ToLevelUP;
    }
    private void ChangeLvl(int changeLvl)
    {
        _carrentLevelText.text = changeLvl.ToString();
    }

    public void ChangeBar()
    {   
        _carrentLevelText.text =$"{_heroLvl.CurrentLvl}";
        _carrentXPText.text =  $"{_heroLvl.CurrentExperience} / {_heroLvl.ToLevelUP}";
        
        slider.value = (float)_heroLvl.CurrentExperience / _heroLvl.ToLevelUP;
    }   

    public void AddXP()
    {
        _heroLvl.AddExperience( 123 );
    }
}
