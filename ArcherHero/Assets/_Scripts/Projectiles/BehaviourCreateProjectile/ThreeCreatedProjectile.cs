using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ThreeCreatedProjectile : CreateProjectilesAround, IBehaviorCreateProjectile
{
    private readonly List<float> _anglesRotation = new() 
    {
        0,
        15,
        -15,
    };

    protected override List<float> AnglesRotation => _anglesRotation;
}