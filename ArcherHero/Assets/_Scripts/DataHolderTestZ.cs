using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolderTestZ : MonoBehaviour
{
    [SerializeField] private int _lvlStart = 1;
    [SerializeField] private CharacterStatsE _playerStats;
    [SerializeField] private PlayerBountyStruct _playerBounty;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public int LvlStart
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

    public CharacterStatsE PlayerStats { get { return _playerStats; } }

    public PlayerBountyStruct PlayerBounty { get { return _playerBounty; } }

    public void SetStats(CharacterStatsE playerStats)
    {
        _playerStats = playerStats;
    }
    public void AddLvlMinedBounty(PlayerBountyStruct enemyBounty)
    {
        _playerBounty.AddBounty(enemyBounty);
    }
}
