using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlSwitchManager 
{
    public event Action<int> OnLevelChanged;
    private int _currentLvl;
    public int CurrentLevel
    {
        get { return _currentLvl; }
        private set
        {
            OnLevelChanged?.Invoke(_currentLvl);
            _currentLvl = _currentLvl >= ObstaclesSwitchManager.LvlCount - 1 ? 0 : value;
        }
    }
    public void SwitchLvl()
    {
        CurrentLevel++;
    }
}
