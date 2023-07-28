
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PerkMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI perkNameText;
    [SerializeField] private TextMeshProUGUI perkDescriptionText;
    [SerializeField] private TextMeshProUGUI perkPriceText;
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private Button _exitButton;

    private List<Button> _perkButtons;

    [SerializeField] private CharacterSkills _characteSkills;

    private PerkManager.PerkType _currentPerkType;
   
    private PerkManager _perkManager;
    private int _perkPrice;
    
    
    
    
    private void Awake()
    {
        _perkManager = FindObjectOfType<PerkManager>();
       
    }
   void Start()
    {
       _purchaseButton.onClick.AddListener(ConfirmPurchase);
       _exitButton.onClick.AddListener(ClosePerkMenu);
    }
    public void OpenPerkMenu(PerkManager.PerkType perkType, string perkName, string perkDescription, int perkPrice, List<Button> perkButtons)
    {   
        _perkPrice = perkPrice;
        _currentPerkType = perkType;
        _perkButtons = perkButtons;
        perkNameText.text = perkName;
        perkDescriptionText.text = perkDescription;
        perkPriceText.text = "Price: " + perkPrice.ToString();

        gameObject.SetActive(true);
    }

    public void ClosePerkMenu()
    {     
        gameObject.SetActive(false);
    }

    public void ConfirmPurchase()
    {       
        if (_perkPrice <= _characteSkills.SkillPoints)
        {
            _perkManager.PurchasePerk(_currentPerkType, _perkPrice);
                   
            ButtonOpen();

            PerkButtonController[] perkButtonControllers = FindObjectsOfType<PerkButtonController>();

            foreach (PerkButtonController buttonController in perkButtonControllers)
            {
                buttonController.UpdateButtonState();
            }

            ClosePerkMenu();
        }
        else
        {
            perkPriceText.text = $" not enough XpPoint ";
        }
    }

    public void ButtonOpen()
    {  
       foreach (Button _perkButton in _perkButtons)
        {
            PerkButtonController buttonController = _perkButton.GetComponent<PerkButtonController>();
           
            if (buttonController != null && _perkManager.GetPerkStatus(buttonController._perkType) == PerkManager.PerkStatus.NotAvailable)
            {
                buttonController.AvialablePerk();
            }           
        } 
    }  
}