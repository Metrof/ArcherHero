using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class ObstaclesSwitchManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> lvlObstaclesList = new List<GameObject>();

    private LvlSwitchManager _lvlSwitchManager;

    private static int _lvlCount;

    public static int LvlCount { get { return _lvlCount; } }
    [Inject]
    private void Construct(LvlSwitchManager lvlSwitchManager)
    {
        _lvlCount = lvlObstaclesList.Count;
        _lvlSwitchManager = lvlSwitchManager;
    }
    private void OnEnable()
    {
        _lvlSwitchManager.OnLevelChanged += SetLevelObstacles;
    }

    private void OnDisable()
    {
        _lvlSwitchManager.OnLevelChanged -= SetLevelObstacles;
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
}