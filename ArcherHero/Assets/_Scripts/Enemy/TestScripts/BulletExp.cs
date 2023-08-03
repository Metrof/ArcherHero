using System;
using Unity.VisualScripting;
using UnityEngine;

public class BulletExp : MonoBehaviour
{
    [SerializeField] private int _damage = 20;
    [SerializeField] private TypeDamage _damageType;

    [SerializeField] private int _bulletSpeed;
    private Vector3 moveDirection;
    

    private void OnTriggerEnter(Collider other)
    {
        
        var damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage( _damageType, _damage);
        }
        //Destroy(gameObject);
    }
    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction.normalized;
    }
    private void Update()
    {
        
        transform.position += moveDirection * _bulletSpeed * Time.deltaTime;
    }
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
    
}
