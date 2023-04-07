using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : UnitController<PlayerView, PlayerModel>
{
    private Controller _inputController;
    public PlayerController(PlayerView view, PlayerModel model, UnitBody body) : base(view, model, body)
    {
        _inputController = new Controller();
    }
    public void Enable()
    {
        _inputController.Enable();
    }
    public override void Death()
    {
        Disable();
    }
    public void Disable() 
    {
        _inputController.Disable();
    }
}
