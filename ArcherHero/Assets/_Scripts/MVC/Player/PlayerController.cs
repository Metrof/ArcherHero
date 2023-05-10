using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : UnitController<PlayerView, PlayerModel>
{
    public void Move(InputAction.CallbackContext context)
    {
        _view.ChangeMoveDirection(context.ReadValue<Vector2>(), _model.MovementSpeed);
        _model.ChangeTarget(transform.position);
    }
    protected override void Death()
    {
        base.Death();
        SceneManager.LoadScene(1);
    }
}
