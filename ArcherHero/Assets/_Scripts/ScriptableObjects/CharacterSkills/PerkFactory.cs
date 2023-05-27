using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkFactory
{

    public IPerk CreatePerk(PerkManager.PerkType perkType)
    {
        switch (perkType)
        {
            case PerkManager.PerkType.Perk1:
                return new Perk1();
            case PerkManager.PerkType.Perk2:
                return new Perk2();
            case PerkManager.PerkType.Perk3:
                return new Perk3();
            case PerkManager.PerkType.Perk4:
                return new Perk4();
      
            default:
                throw new ArgumentException("Unknown perk type: " + perkType);
        }
    }
}




public class Perk1 : IPerk
{
    public void Apply(PlayerController playerController)
    {
        Debug.Log("Perk1 Dash");
       
    }
}
public class Perk2 : IPerk
{
    public void Apply(PlayerController playerController)
    {
        Debug.Log("Perk2");
        //playerController.
    }
}
public class Perk3 : IPerk
{
    public void Apply(PlayerController playerController)
    {
        Debug.Log("Perk3");
    }
}
public class Perk4 : IPerk
{
    public void Apply(PlayerController playerController)
    {
        Debug.Log("Perk4");
    }
}




// Dictionary<PerkManager.PerkType, PerkManager.PerkStatus> _perkStates;
// List<PerkManager.PerkType> purchasedPerks;

// public void Awake()
// {
//     LoadPerkData();
// }

// public void LoadPerkData()
// {
//     Debug.Log("LoadData");
//     _perkStates = SaveSystem.SaveSystem.LoadPerkData();
// }

// public void GetPurchasedPerk()
// {
//     purchasedPerks = new List<PerkManager.PerkType>();
//     _perkStates = new Dictionary<PerkManager.PerkType, PerkManager.PerkStatus>();

//     LoadPerkData();

//     foreach (var perkEntry in _perkStates)
//     {
//         if (perkEntry.Value == PerkManager.PerkStatus.Purchased)
//         {
//             purchasedPerks.Add(perkEntry.Key);
//         }
//     }

//     Debug.Log("GetPurchasedPerk()");
// }

// public void ApplyPerks()
// {
//     PerkFactory perkFactory = new PerkFactory();

//     foreach (PerkManager.PerkType perkType in purchasedPerks)
//     {
//         IPerk perk = perkFactory.CreatePerk(perkType);
//         perk.Apply(this);
//     }
// }