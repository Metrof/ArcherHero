using System;
using Unity.VisualScripting;
using UnityEngine;

public class BulletExp : MonoBehaviour
{
    [SerializeField] private int _damage = 20;
    [SerializeField] private DamageTypeExp _damageTypeExp;
    

    private void OnTriggerEnter(Collider other)
    {
        
        var damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(_damage, _damageTypeExp);
        }

       
        //Destroy(gameObject);
    }
}