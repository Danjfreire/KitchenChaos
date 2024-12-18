using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    private enum LookMode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted,
    }

    [SerializeField] private LookMode mode;

    private void LateUpdate()
    {
        switch(mode) {
            case LookMode.LookAt:
                {
                    transform.LookAt(Camera.main.transform);
                     break;
                }
            case LookMode.LookAtInverted: 
                {
                    Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                    transform.LookAt(transform.position + dirFromCamera);
                    break;
                }
            case LookMode.CameraForward: 
                {
                    transform.forward = Camera.main.transform.forward;
                    break;
                }
            case LookMode.CameraForwardInverted: {
                    transform.forward = -Camera.main.transform.forward;
                    break;
                }

        }

    }
}
