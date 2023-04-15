using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class EnemyController : UnitController<EnemyView, EnemyModel>
{
    private Vector3 _pullPos;

    public void SetPullPos(Vector3 pullPos)
    {
        _pullPos = pullPos;
    }
    protected override void Death()
    {
        base.Death();
        Teleportation(_pullPos);
    }
}
