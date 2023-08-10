using System.Collections.Generic;

public class DoubleCreatedProjectile : TwinProjectiles
{
    private readonly List<float> _shiftX = new ()
    {
        0.5f,
        -0.5f,
    };

    protected override List<float> ShiftX => _shiftX;
}
