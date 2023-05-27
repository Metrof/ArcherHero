using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private EnemyTypes _type = EnemyTypes.DefoltRobot;

    public Transform Transform { get { return transform; } }

    public EnemyTypes Type { get { return _type; } }
}
