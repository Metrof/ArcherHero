using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlSwitchManager 
{
    public event Action<int> OnLevelChanged;
    public event Action OnLevelOver;
    private int _currentLvl;
    public int CurrentLevel
    {
        get { return _currentLvl; }
        private set
        {
            OnLevelChanged?.Invoke(_currentLvl);
            if (value >= ObstaclesSwitchManager.LvlCount)
            {
                OnLevelOver?.Invoke();
                _currentLvl = 0;
                return;
            }
            else
            {
                _currentLvl = value;
            }
        }
    }
    public void SwitchLvl()
    {
        CurrentLevel++;
    }
    public void StayLvlZero()
    {
        _currentLvl = 0;
    }
}
