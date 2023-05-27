using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : UnitController<PlayerView, PlayerModel>
{
    private Skill _firstSkill;
    private Skill _secondSkill;

    private Controller _controller;


    private void Awake()
    {
        _controller = new Controller();
    }

    private void OnEnable()
    {
        _controller.Enable();
        _controller.Player.FirstSkill.performed += ActiveFirstSkill;
        _controller.Player.SecondSkill.performed += ActiveSecondSkill;
    }
    private void OnDisable()
    {
        _controller.Player.FirstSkill.performed -= ActiveFirstSkill;
        _controller.Player.SecondSkill.performed -= ActiveSecondSkill;
        _controller.Disable();
    }
    public void Move(InputAction.CallbackContext context)
    {
        if (!_isMoving) return;
        _view.ChangeMoveDirection(context.ReadValue<Vector2>(), _model.MovementSpeed, Rigidbody);
        _model.ChangeTarget(transform.position);
    }
    public void SetFirstSkill(Skill skill)
    {
        _firstSkill = skill;
    }
    public void SetSecondSkill(Skill skill)
    {
        _secondSkill = skill;
    }
    private void ActiveFirstSkill(InputAction.CallbackContext context)
    {
        if (_firstSkill != null)
        {
            _firstSkill.Action(this);
        }
    }
    private void ActiveSecondSkill(InputAction.CallbackContext context)
    {
        if (_secondSkill != null)
        {
            _secondSkill.Action(this);
        }
    }
    protected override void Death()
    {
        base.Death();
        SceneManager.LoadScene(0);
    }
}
