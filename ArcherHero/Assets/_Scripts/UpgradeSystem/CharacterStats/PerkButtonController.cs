
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkButtonController : MonoBehaviour
{
    public PerkManager.PerkType _perkType; 
    public Button _perkButton; 
    private PerkManager _perkManager;
    private Image _buttonImage;
    [SerializeField] private List<Button> _perkButtons;
    [SerializeField] private int _perkPrice;
    [SerializeField] private PerkMenu _perkMenu;
    [SerializeField] private string _perkName;
    [SerializeField] private string _perkDiscription;
 

    private void Start()
    {   
        _perkManager = FindObjectOfType<PerkManager>();
        _buttonImage = _perkButton.GetComponent<Image>();
        
         
        UpdateButtonState();

        _perkButton.onClick.AddListener(OpenPurchasePerkMenu);
    }
    public void OpenPurchasePerkMenu()
    {    
        string perkName = _perkName;
        string perkDescription = _perkDiscription;
       
        _perkMenu.OpenPerkMenu(_perkType, perkName, perkDescription, _perkPrice, _perkButtons);
    }
  
    public void AvialablePerk()
    {
        _perkManager.AvailablePerk(_perkType);
    }

    public void UpdateButtonState()
    {   
        PerkManager.PerkStatus perkStatus = _perkManager.GetPerkStatus(_perkType);

        switch (perkStatus)
        {   
            case PerkManager.PerkStatus.NotAvailable:
                _buttonImage.sprite = _perkManager.NotAvailableIkon;
                _perkButton.enabled = false;
                break;
            case PerkManager.PerkStatus.Available:
                _buttonImage.sprite = _perkManager.AvailableIkon;
                _perkButton.enabled = true;
                break;
            case PerkManager.PerkStatus.Purchased:
                _buttonImage.sprite = _perkManager.PurchasedIkon;
                _perkButton.enabled = false;
                break;
        }
    }
}
