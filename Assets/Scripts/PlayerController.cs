using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float turnSpeed = 10.0f;

    private bool isWalking;
    private void Update() {
        Vector3 inputVector = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) {
            inputVector.z += 1;
        }

        if (Input.GetKey(KeyCode.S)) {
            inputVector.z -= 1;
        }

        if (Input.GetKey(KeyCode.A)) {
            inputVector.x -= 1;
        }

        if (Input.GetKey(KeyCode.D)) {
            inputVector.x += 1;
        }

        inputVector = inputVector.normalized;
        
        isWalking = inputVector != Vector3.zero;

        transform.position += moveSpeed * Time.deltaTime * inputVector;
        transform.forward = Vector3.Slerp(transform.forward, inputVector, turnSpeed * Time.deltaTime);
    }

    public bool IsWalking() {
        return isWalking;
    }
}
