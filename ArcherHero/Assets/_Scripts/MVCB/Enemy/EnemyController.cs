using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class EnemyController : UnitController<EnemyView, EnemyModel>
{
    public EnemyController(EnemyView view, EnemyModel model, UnitBody body, Vector3 startPos) : base(view, model, body, startPos)
    {

    }
}
