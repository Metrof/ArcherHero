using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    protected Rigidbody _rigidbody;
    public Vector3 Position { get { return transform.position; } }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
}
