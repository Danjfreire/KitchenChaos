using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }


    public Vector2 GetNormalizedMovementVector()
    {
        Vector2 inputVector = this.playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        Debug.Log(inputVector);

        return inputVector;
    }
}
