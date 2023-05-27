using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerBountyStruct 
{
    private int _minedExp;
    private int _minedGold;

    public PlayerBountyStruct(int exp, int gold)
    {
        _minedExp = exp;
        _minedGold = gold;
    }

    public int MinedGold { get { return _minedGold;} }
    public int MinedExp { get { return _minedExp; } }

    public void AddExp(int exp)
    {
        _minedExp += exp;   
    }
    public void AddGold(int gold) {  _minedGold += gold; }

    public void AddBounty(PlayerBountyStruct bounty)
    {
        _minedExp += bounty.MinedExp;
        _minedGold += bounty.MinedGold;
    }
    public void ClearStruct()
    {
        _minedExp = 0;
        _minedGold = 0;
    }
}
