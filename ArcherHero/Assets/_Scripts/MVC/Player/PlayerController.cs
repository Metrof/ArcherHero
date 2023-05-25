using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : UnitController<PlayerView, PlayerModel>
{
    [SerializeField] private CharacterSkills _characterSkills;
    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] Dagger _dagger;
    private Skill _firstSkill;
    private Skill _secondSkill;

    private Controller _controller;


    private void Awake()
    {
        Dagger dagger = Instantiate(_dagger);
        dagger.SetOwner(transform);

        _controller = new Controller();
        _firstSkill = new Dash(2);
        _secondSkill = new Parry(dagger.gameObject, 2);
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
        SceneManager.LoadScene(1);
    }
}
