using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextBar : MonoBehaviour
{
    public LvlSwithcManager lvlSwithcManager;
    public TextMeshPro _lvlText;

    private void Start()
    {   
        _lvlText.text = $"Level  {lvlSwithcManager.CurrentLevel}";
    }

    private void OnEnable()
    {
        lvlSwithcManager.OnLevelChanged += OnLevelChangedHandler;
    }

    private void OnDisable()
    {
        lvlSwithcManager.OnLevelChanged -= OnLevelChangedHandler;
    }

    private void OnLevelChangedHandler(int currentLvl)
    {
        _lvlText.text = $"Level  {currentLvl + 1}";
    }
}    