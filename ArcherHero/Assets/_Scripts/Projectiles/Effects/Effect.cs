using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect
{
    protected CharacterStatsE _changes;
    public abstract void GetEffect(Action<CharacterStatsE> changer);
}
