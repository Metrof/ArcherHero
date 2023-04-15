using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : UnitController<PlayerView, PlayerModel>
{
    public void StartNewLvl(List<Transform> enemies)
    {
        SetEnemyPull(enemies);
        TransformToDefoltPos();
        _model.ChangeTarget(transform.position);
    }
    public void SetEnemyPull(List<Transform> enemies)
    {
        _model.SetPull(enemies);
    }
    public void TransformToDefoltPos()
    {
        Teleportation(_defoltPosition);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _view.ChangeMoveDirection(context.ReadValue<Vector2>(), _model.MovementSpeed);
        _model.ChangeTarget(transform.position);
    }
}
