using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private GameInput gameInput;

    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float turnSpeed = 10.0f;

    private bool isWalking;
    private void Update() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveSpeed * Time.deltaTime * moveDirection;

        isWalking = moveDirection != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, turnSpeed * Time.deltaTime);
    }

    public bool IsWalking() {
        return isWalking;
    }
}
