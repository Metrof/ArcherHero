using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LvlDoor : MonoBehaviour
{
    [SerializeField] private float _openTime = 1;

    private const int _openDoorState = 110;
    private const int _closeDoorState = 0;

    private Tween _rotateTween;
    private CancellationTokenSource _cancellationToken;

    public void OpenDoor()
    {
        _rotateTween.Kill();
        _rotateTween = transform.DORotate(new Vector3(0, _openDoorState, 0), _openTime);
    }
    public void CloseDoor()
    {
        _rotateTween.Kill();
        _rotateTween = transform.DORotate(new Vector3(0, _closeDoorState, 0), _openTime);
    }
}
