using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlSwitchTriggerZone : MonoBehaviour
{
    public event Action OnPlayerEnter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnter?.Invoke();
        }
    }
}
