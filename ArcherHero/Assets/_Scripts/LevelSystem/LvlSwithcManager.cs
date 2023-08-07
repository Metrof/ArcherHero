using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LvlSwithcManager : MonoBehaviour
{
    public event Action<int> OnLevelChanged; 
    [SerializeField] private List<GameObject> lvlObstaclesList = new List<GameObject>();
    public int _currentLevel;

    private void Start()
    {
        _currentLevel -= 1;
        SetLevelObstacles(_currentLevel);
    }

    public void SetLevelObstacles(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= lvlObstaclesList.Count)
        {
            Debug.LogError("Invalid level index!");
            return;
        }

        foreach (var lvlObstacles in lvlObstaclesList)
        {
            lvlObstacles.SetActive(false);
        }
        
        lvlObstaclesList[levelIndex].SetActive(true);
    }

    
    public void SwitchToNextLevel()
    {
        _currentLevel++;

        if (_currentLevel >= lvlObstaclesList.Count)
        {
            _currentLevel = 0; 
        }
        SetLevelObstacles(_currentLevel);
        
        OnLevelChanged?.Invoke(_currentLevel);
    }

    private void OnTriggerEnter(Collider other)
    {
        throw new NotImplementedException();
    }
}