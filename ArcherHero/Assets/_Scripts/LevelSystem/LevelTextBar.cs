using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelTextBar : MonoBehaviour
{
    [SerializeField] private TextMeshPro _lvlText;

    private LvlSwitchManager _lvlSwitchManager;

    [Inject]
    private void Construct(LvlSwitchManager lvlSwitchManager)
    {
        _lvlSwitchManager = lvlSwitchManager;
    }
    private void Start()
    {   
        _lvlText.text = $"Level  {_lvlSwitchManager.CurrentLevel}";
    }

    private void OnEnable()
    {
        _lvlSwitchManager.OnLevelChanged += OnLevelChangedHandler;
    }

    private void OnDisable()
    {
        _lvlSwitchManager.OnLevelChanged -= OnLevelChangedHandler;
    }

    private void OnLevelChangedHandler(int currentLvl)
    {
        _lvlText.text = $"Level  {currentLvl + 1}";
    }
}    