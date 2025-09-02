using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Camera : MonoBehaviour
{
    [SerializeField] private float panSpeed = 20f;
    [SerializeField] private float panBorderThickness = 10f;
    [SerializeField] private Vector2 panLimit;

    InputAction moveUp;
    InputAction moveRight;
    InputAction moveDown;
    InputAction moveLeft;
    
    private void Start()
    {
        moveUp = InputSystem.actions.FindAction("MoveUp");
        moveRight = InputSystem.actions.FindAction("MoveRight");
        moveDown = InputSystem.actions.FindAction("MoveDown");
        moveLeft = InputSystem.actions.FindAction("moveLeft");
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        
        if (moveUp.IsPressed() || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (moveDown.IsPressed() || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (moveRight.IsPressed() || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (moveLeft.IsPressed() || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        transform.position = pos;
    }
}
