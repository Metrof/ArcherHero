using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class DamageEffect : Effect
{
    public DamageEffect(CharacterStats baseDamage, CharacterStats addDamage = null)
    {
        _changes = baseDamage;
        if (addDamage != null) _changes.AddStats(addDamage);
    }

    public override void GetEffect(Action<CharacterStats> changer)
    {
        changer.Invoke(_changes);
    }
}
