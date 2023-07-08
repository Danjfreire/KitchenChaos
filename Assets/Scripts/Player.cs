using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private GameInput gameInput;

    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float turnSpeed = 10.0f;
    private float playerRadius = 0.7f;
    private float playerHeight = 2.0f;

    private bool isWalking;
    private void Update() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        float moveDistance = moveSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        if (!canMove) {
            // cannot move towards moveDirection, try to move in the other direction

            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

            if (canMove) {
                // can move only on the x axis
                moveDirection = moveDirectionX;
            } else {
                // cannot move on the x axis, try to move on the z axis
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

                if (canMove) {
                    // can move only on the z axis
                    moveDirection = moveDirectionZ;
                } else {
                    // cannot move in any direction
                }
            }
        }


        if (canMove) {
            transform.position += moveDistance * moveDirection;
        }

        isWalking = moveDirection != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, turnSpeed * Time.deltaTime);
    }

    public bool IsWalking() {
        return isWalking;
    }
}
