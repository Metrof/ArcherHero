using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerView : UnitView
{
    public void ChangeMoveDirection(Vector2 dir, float speed, Rigidbody rigidbody)
    {
        Vector3 velocity = rigidbody.velocity;
        dir *= speed;
        velocity.x = dir.x;
        velocity.z = dir.y;
        rigidbody.velocity = velocity;
    }
}
