using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : UnitController<PlayerView, PlayerModel>
{
    //private Controller _inputController = new();
    public PlayerController(PlayerView view, PlayerModel model, UnitBody body, Vector3 startPos) : base(view, model, body, startPos)
    {
    }
    protected override void Enable()
    {
        base.Enable();
        //_inputController.Enable();
        //_inputController.Player.Move.performed += Move;
        //_inputController.Player.Move.canceled += StopMove;
    }

    public void StartNewLvl(List<UnitBody> enemies)
    {
        SetEnemyPull(enemies);
        TransformToDefoltPos();
        _model.ChangeTarget(_body.Position);
    }
    public void SetEnemyPull(List<UnitBody> enemies)
    {
        _model.SetPull(enemies);
    }
    public void TransformToDefoltPos()
    {
        _body.Teleportation(_defoltPosition);
    }


    public void Move(InputAction.CallbackContext obj)
    {
        _view.ChangeMoveDirection(obj.ReadValue<Vector2>(), _body.MovementSpeed);
        _model.ChangeTarget(_body.Position);
    }
    public void StopMove(InputAction.CallbackContext obj)
    {
        _view.ChangeMoveDirection(Vector2.zero, 0);
        _model.ChangeTarget(_body.Position);
    }


    protected override void Death()
    {
        base.Death();
    }
    protected override void Disable() 
    {
        base.Disable();
        //_inputController.Player.Move.performed -= Move;
        //_inputController.Player.Move.canceled -= StopMove;
        //_inputController.Disable();
    }
}
