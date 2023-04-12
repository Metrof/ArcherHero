using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class EnemyController : UnitController<EnemyView, EnemyModel>
{
    private Vector3 _pullPos;
    public EnemyController(EnemyView view, EnemyModel model, UnitBody body, Vector3 startPos, Vector3 pullPos) : base(view, model, body, startPos)
    {
        _pullPos = pullPos;
    }
    protected override void Death()
    {
        base.Death();
        _body.Teleportation(_pullPos);
    }
}
