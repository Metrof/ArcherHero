using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    [SerializeField] private int _ownerlayer = 6;
    [SerializeField] private float _zOffset = 0.8f;

    private Transform _ownerTrans;
    public int Ownerlayer { get { return _ownerlayer; } }

    public void SetOwner(Transform owner)
    {
        _ownerTrans = owner;
    }

    private void Update()
    {
        Vector3 targetPos = _ownerTrans.position;
        targetPos.z += _zOffset;
        transform.position = targetPos;
    }
}
