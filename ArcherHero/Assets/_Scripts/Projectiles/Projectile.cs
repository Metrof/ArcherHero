using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 1;


    private int _defoltLayer;
    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private Vector3 _moveDirection;
    private Vector3 _pullPos;
    private List<Effect> _effects = new List<Effect>();

    public List<Effect> Effects { get { return _effects; } }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _defoltLayer = gameObject.layer;
    }
    public void MoveToDirection(Vector3 moveDirection, Vector3 shotPos)
    {
        transform.position = shotPos;
        _moveDirection = moveDirection.normalized;
        _rigidbody.velocity = _moveDirection * _movementSpeed;
    }
    public void ChangeOwner(int layer, Material ownerMat)
    {
        gameObject.layer = layer * 2;
        _renderer.material = ownerMat;
    }
    public void SetPullPos(Vector3 pullPos)
    {
        _pullPos = pullPos;
        transform.position = _pullPos;
    }
    public void OnHit()
    {
        Return();
    }
    protected void Return()
    {
        transform.position = _pullPos;
        _rigidbody.velocity = Vector3.zero;
        gameObject.layer = _defoltLayer;
    }
    private void OnTriggerEnter(Collider other)
    {
        OnHit();
    }
}
