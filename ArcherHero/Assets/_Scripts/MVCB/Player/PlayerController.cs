using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : UnitController<PlayerView, PlayerModel>
{
    private Controller _inputController;
    public PlayerController(PlayerView view, PlayerModel model, UnitBody body, Vector3 startPos) : base(view, model, body, startPos)
    {
        _inputController = new Controller();
    }
    protected override void Enable()
    {
        _inputController.Enable();
        _inputController.Player.Move.performed += Move;
        _inputController.Player.Move.canceled += StopMove;
    }

    public void StartNewLvl(List<UnitBody> enemies)
    {
        SetEnemyPull(enemies);
        TransformToDefoltPos();
    }
    public void SetEnemyPull(List<UnitBody> enemies)
    {
        _model.SetPull(enemies);
    }
    public void TransformToDefoltPos()
    {

    }


    public void Move(InputAction.CallbackContext obj)
    {
        _view.ChangeMoveDirection(obj.ReadValue<Vector2>(), _body.MovementSpeed);
        _model.ChangeTarget(_view.Position);
    }
    public void StopMove(InputAction.CallbackContext obj)
    {
        _view.ChangeMoveDirection(Vector2.zero, 0);
        _model.ChangeTarget(_view.Position);
    }


    protected override void Death()
    {
        base.Death();
        _model.ChangeTarget(_view.Position);
    }
    protected override void Disable() 
    {
        _inputController.Player.Move.performed -= Move;
        _inputController.Player.Move.canceled -= StopMove;
        _inputController.Disable();
    }
}
