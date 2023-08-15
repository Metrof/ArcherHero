using System.Collections.Generic;

public class EightCreatedProjectilesAround : CreateProjectilesAround
{
    private readonly List<float> _anglesRotation = new()
    {
        0,
        45,
        90,
        135,
        180,
        -45,
        -90,
        -135,
    };
    protected override List<float> AnglesRotation => _anglesRotation;
}