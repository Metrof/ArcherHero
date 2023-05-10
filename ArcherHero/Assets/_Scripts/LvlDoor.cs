using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LvlDoor : Singleton<LvlDoor>
{
    [SerializeField] private static float _openTime = 1;

    private const int _openDoorState = -110;
    private const int _closeDoorState = 0;

    public static void OpenDoor()
    {
        Instance.StartCoroutine(DoorCorotine(_openDoorState));
    }
    public static void CloseDoor()
    {
        Instance.StartCoroutine(DoorCorotine(_closeDoorState));
    }
    private static IEnumerator DoorCorotine(float targetAngle)
    {
        float endCorotineTime = Time.time + _openTime;
        float startCorotineTime = Time.time;
        float delta;
        Quaternion targetQuaternion = Quaternion.Euler(0, targetAngle, 0);
        while (endCorotineTime > Time.time)
        {
            delta = (Time.time - startCorotineTime) / _openTime;
            Instance.transform.rotation = Quaternion.Lerp(Instance.transform.rotation, targetQuaternion, delta);
            yield return null;
        }
    }
}
