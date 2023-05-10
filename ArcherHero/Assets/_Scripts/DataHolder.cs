using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHolder 
{
    private static int _lvlStart;
    private static CharacterStatsE _playerStats;

    public static int LvlStart { get { return _lvlStart; } }
    public static CharacterStatsE PlayerStats { get { return _playerStats; } }

    public static void SetLvlData(int lvlStart, CharacterStatsE playerStats)
    {
        _lvlStart = lvlStart;
        _playerStats = playerStats;
    }
}
