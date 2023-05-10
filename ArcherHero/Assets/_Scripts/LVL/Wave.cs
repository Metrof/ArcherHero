using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private SpawnPoint[] _points;

    public SpawnPoint[] Points { get { return _points; } }

    private void Awake()
    {
        _points = GetComponentsInChildren<SpawnPoint>();
    }
}
