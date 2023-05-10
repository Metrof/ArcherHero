using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlTriggerZone : MonoBehaviour
{
    public delegate void TriggerZoneDelegate();
    public event TriggerZoneDelegate OnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnTrigger?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
