using System.Collections.Generic;

public class TripleCreatedProjectile : TwinProjectiles
{
    private readonly List<float> _shiftX = new()
    {
        0,
        1f,
        -1f,
    };

    protected override List<float> ShiftX => _shiftX;
}
