using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 2)]
public class Level : ScriptableObject
{
    [SerializeField] private GameObject _lvlPref;
    [SerializeField] private LvlTriggerZone _lvlStartTriggerZone;
    [SerializeField] private LvlTriggerZone _lvlEndTriggerZone;
    [SerializeField] private List<EnemyController> _enemies;
    [SerializeField] private float _scaleEnemyPower;
    [SerializeField] private int _lvlNum = 0;
    [SerializeField] private int _enemysCount = 7;

    private Wave[] _waves;
    private int _currentWave;

    public int EnemyCount { get { return _enemysCount; } }
    public int LvlNum { get { return _lvlNum; } }   
    public int CurrentWave 
    {
        get 
        {
            return _currentWave; 
        }
    }
    public float EnemyPower { get { return _scaleEnemyPower; } }
    public List<EnemyController> Enemies { get { return _enemies; } }
    public LvlTriggerZone LvlStartTriggerZone { get { return _lvlStartTriggerZone; } }
    public LvlTriggerZone LvlEndTriggerZone { get { return _lvlEndTriggerZone; } }
    public GameObject LvlPref { get { return _lvlPref; } }

    public Wave[] Waves { get { return _waves ??= _lvlPref.GetComponentsInChildren<Wave>(); } }
}
