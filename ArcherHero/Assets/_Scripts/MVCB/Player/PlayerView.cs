using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerView : UnitView
{
    public void ChangeMoveDirection(Vector2 dir, float speed)
    {
        Vector3 velocity = _rigidbody.velocity;
        dir *= speed;
        velocity.x = dir.x;
        velocity.z = dir.y;
        _rigidbody.velocity = velocity;
    }
}
