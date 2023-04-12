using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : Effect
{
    private float _damage;
    public DamageEffect(float damage)
    {
        _damage = damage;
    }

    protected float Damage { get { return _damage; } private set { _damage = value; } }
    public override void GetEffect(UnitBody body)
    {
        body.TakeDamage(Damage);
    }
    public void ChangeDamage(float newDamage)
    {
        Damage = newDamage;
    }
}
