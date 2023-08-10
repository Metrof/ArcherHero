using System.Collections.Generic;

public class TripleCreatedProjectile : TwinProjectiles
{
    private readonly List<float> _shiftX = new()
    {
        0,
        0.7f,
        -0.7f,
    };

    protected override List<float> ShiftX => _shiftX;
}
