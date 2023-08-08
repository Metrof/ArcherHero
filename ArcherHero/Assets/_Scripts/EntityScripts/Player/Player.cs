using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    Controller _controller;
    private Tween _moveTween;

    private Weapon _weapon;

    Vector2 _contextDir;

    Quaternion _targetRotation;

    private void Awake()
    {
        _controller = new Controller();
    }
    private void OnEnable()
    {
        _controller.Enable();
        _controller.Player.Move.performed += Move;
    }
    private void OnDisable()
    {
        _controller.Player.Move.performed -= Move;
        _controller.Disable();
    }
    public void Move(InputAction.CallbackContext context)
    {
        _contextDir = context.ReadValue<Vector2>();

        //.OnComplete() => transform.gameObject.SetActive(false); метод отрабатывает при завершении цикла Tweena
    }
    private void Update()
    {
        _moveTween.Kill();
        if (_controller.Player.Move.IsPressed())
        {
            Vector3 moveDir = new Vector3(_contextDir.x, 0, _contextDir.y);
            _moveTween = transform.DOMove(moveDir, Time.deltaTime * Speed).SetSpeedBased().SetEase(Ease.Linear).SetRelative();
            //_characterController.Move(moveDir * Time.deltaTime * Speed);

            transform.LookAt(Vector3.LerpUnclamped(transform.forward + transform.position, moveDir + transform.position, RotationSpeed));
            //Quaternion.LookRotation(new Vector3(moveDir.x, 0, moveDir.y));

            //Vector3 _rotatedMovement = Quaternion.Euler(0.0f, 0, 0.0f) * moveDir;
            //_rotationAngle = Mathf.Atan2(_rotatedMovement.x, _rotatedMovement.z) * Mathf.Rad2Deg;
            //_targetRotation = Quaternion.Euler(0.0f, _rotationAngle, 0.0f);
            //_rotateTween = transform.DORotate(_targetRotation.eulerAngles, 100).SetSpeedBased().SetEase(Ease.Linear).SetRelative();
        }
    }
}
