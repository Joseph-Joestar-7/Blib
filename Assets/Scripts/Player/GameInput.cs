using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameInput : MonoBehaviour
{
    private IA_Player playerInputActions;

    public event EventHandler OnJumpAction;

    private void Awake()
    {
        playerInputActions = new IA_Player();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Jump.performed += Jump_performed;
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;
        return inputVector;
    }
}