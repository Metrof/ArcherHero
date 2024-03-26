using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private int _defoltLayer;
    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private Vector3 _moveDirection;
    private Vector3 _pullPos;
    private List<Effect> _effects = new List<Effect>();

    private Transform _shoterTrans;

    public List<Effect> Effects { get { return _effects; } }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _defoltLayer = gameObject.layer;
    }
    public void MoveToDirection(Vector3 moveDirection, Transform shotSource, float speed = 5)
    {
        _shoterTrans = shotSource;
        transform.position = _shoterTrans.position;
        _moveDirection = moveDirection.normalized;
        _rigidbody.velocity = _moveDirection * speed;
    }
    public void ChangeOwner(int layer, Material ownerMat)
    {
        ChangeLayer(layer);
        _renderer.material = ownerMat;
    }
    private void ChangeLayer(int layer)
    {
        gameObject.layer = layer * 2;
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
        if (other.TryGetComponent(out Dagger dagger))
        {
            //делать копию пули
            MoveToDirection(_shoterTrans.position - transform.position, dagger.transform, 10);
            ChangeLayer(dagger.Ownerlayer);
        }
        else
        {
            OnHit();
        }
    }
}
