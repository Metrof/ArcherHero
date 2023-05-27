using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHolder 
{
    private static int _lvlStart;
    private static CharacterStatsE _playerStats;
    private static PlayerBountyStruct _playerBounty;

    public static int LvlStart 
    { 
        get { return _lvlStart; } 
        set 
        { 
            if (value > 4 - 1)
            {
                _lvlStart = 0;
            }
            else
            {
                _lvlStart = value;
                if (_lvlStart < 0)
                {
                    _lvlStart = 4 - 1;
                }
            }
        }
    }
    public static CharacterStatsE PlayerStats { get { return _playerStats; } }
    public static PlayerBountyStruct PlayerBounty { get { return _playerBounty; } }

    public static void SetStats(CharacterStatsE playerStats)
    {
        _playerStats = playerStats;
    }
    public static void AddLvlMinedBounty(PlayerBountyStruct enemyBounty)
    {
        _playerBounty.AddBounty(enemyBounty);
    }
}
