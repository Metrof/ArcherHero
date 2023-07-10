using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill 
{
    protected float _colldown;

    public Skill(float colldown)    {
        _colldown = colldown;
    }


    public abstract void Action(PlayerController player);
}
