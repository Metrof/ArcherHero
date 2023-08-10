using System.Collections.Generic;

public class DoubleCreatedProjectile : TwinProjectiles
{
    private readonly List<float> _shiftX = new ()
    {
        0.4f,
        -0.4f,
    };

    protected override List<float> ShiftX => _shiftX;
}
