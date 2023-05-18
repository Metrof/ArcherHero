using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PerkManager : MonoBehaviour
{   
    
    public enum PerkStatus
    {
        NotAvailable,
        Available,
        Purchased
       
    }
    
    public enum PerkType
    {
        Perk1,
        Perk2,
        Perk3,
        Perk4,
        Perk5,
        Perk6,
        Perk7,
        Perk8,
        Perk9,
        Perk10,
        Perk11,
        Perk12,
        Perk13,
        Perk14
    }
    private Dictionary<PerkType, PerkStatus> _perkStates ;

    [SerializeField] private Sprite _availableIkon;
    [SerializeField] private Sprite _notAvailableIkon;
    [SerializeField] private Sprite _purchasedIkon;

    [SerializeField] private TextMeshProUGUI perkPriceText;

    [SerializeField] private CharacterSkills _characteSkills;  

    private XPCounter _xpCounter;  

    public Sprite AvailableIkon => _availableIkon;
    public Sprite NotAvailableIkon => _notAvailableIkon;
    public Sprite PurchasedIkon => _purchasedIkon;


    void Awake()
    {   
        _perkStates = new Dictionary<PerkType, PerkStatus>();
        _xpCounter = FindObjectOfType<XPCounter>();
        LoadPerkData();
    }
    public Dictionary<PerkManager.PerkType, PerkManager.PerkStatus> GetPerkStates()       //  получение словоря с перками
    {
        return _perkStates;
    }
   
    public void SavePerkData()
    {
        SaveSystem.SaveSystem.SavePerkData(_perkStates);
    }

    public void LoadPerkData()
    {
       _perkStates = SaveSystem.SaveSystem.LoadPerkData();
    }

    public void InitializedPerk()
    {
        foreach (PerkType perk in Enum.GetValues(typeof(PerkType)))
        {
            _perkStates[perk] = PerkStatus.NotAvailable;
        }

        _perkStates[PerkType.Perk1] = PerkStatus.Available;
        _perkStates[PerkType.Perk8] = PerkStatus.Available;
    }

    public PerkStatus GetPerkStatus(PerkType perkType)
    {   
        try
        {   
            return _perkStates[perkType];
        }
        catch (Exception)
        {
            InitializedPerk();
            return GetPerkStatus(perkType);
        }
    }

    public void PurchasePerk(PerkType perk, int perkPrice)
    {      
        _characteSkills.PurchasePerk(perkPrice);
           
        _perkStates[perk] = PerkStatus.Purchased;  

        _xpCounter.UpdateXPCounter();
    }


    public void AvailablePerk(PerkType perk)
    {
        _perkStates[perk] = PerkStatus.Available;
    }
}
    

