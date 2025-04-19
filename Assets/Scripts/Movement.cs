using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private float targetXPos;
    private float laneDistance = 2f;

    private GameInput input = null;
    private Vector2 moveInput;

    private void Awake()
    {
        input = new GameInput();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCanceled;

    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCanceled;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 currentPos = transform.position;

        float newX = Mathf.Lerp(currentPos.x, targetXPos, speed * Time.deltaTime);

        transform.position = new Vector3(newX, currentPos.y, currentPos.z);
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveInput = value.ReadValue<Vector2>();

        if (moveInput.x > 0)
            targetXPos += laneDistance;
        else if (moveInput.x < 0)
            targetXPos -= laneDistance;

        targetXPos = Mathf.Clamp(targetXPos, -laneDistance, laneDistance);

        Debug.Log(moveInput);
    }

    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        moveInput = Vector2.zero;
    }
}
