using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameInput : MonoBehaviour
{
    private IA_Player playerInputActions;

    private void Awake()
    {
        playerInputActions = new IA_Player();
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;
        return inputVector;
    }
}