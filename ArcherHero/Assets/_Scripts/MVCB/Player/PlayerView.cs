using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : UnitView
{
    public void ChangeMoveDirection(Vector2 dir, float speed)
    {
        _rigidbody.velocity = dir * speed;
    }
}
