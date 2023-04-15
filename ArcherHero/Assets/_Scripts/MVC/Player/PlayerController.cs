using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : UnitController<PlayerView, PlayerModel>
{
    public void Move(InputAction.CallbackContext context)
    {
        _view.ChangeMoveDirection(context.ReadValue<Vector2>(), _model.MovementSpeed);
        _model.ChangeTarget(transform.position);
    }
}
