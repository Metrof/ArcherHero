using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    [SerializeField] protected float _cooldown = 1f;

    public abstract void Activate(Player player);
}
