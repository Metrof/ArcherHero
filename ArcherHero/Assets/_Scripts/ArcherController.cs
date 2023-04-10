using UnityEngine;
using UnityEngine.InputSystem;

public class ArcherController : MonoBehaviour
{   
    private CharacterController controller;
    private CharacterController Controller{get { return controller = controller ?? GetComponent<CharacterController>();} }
  
    private InputControls input;

    private Vector2 moveDirection;

    public float moveSpeed = 10f;

    public void SetMoveInput(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }
    
    private void Move(Vector2 direction)
    {
        float scaleMoveSpeed = moveSpeed * Time.deltaTime;

        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
        Controller.Move((moveDirection * scaleMoveSpeed) * Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            transform.LookAt(transform.position + moveDirection);
        }    
    }

    private void Update()
    {
        Move(moveDirection);  
    }
}
