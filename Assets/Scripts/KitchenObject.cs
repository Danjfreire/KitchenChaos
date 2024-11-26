using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter clearCounter;
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        // Remove kitchen object from current clear counter if it exists
        if (this.clearCounter != null) {
            this.clearCounter.ClearKitchenObject();
        }

        // Add kitchen object to new clear counter
        if (clearCounter.HasKitchenObject()) {
            // this should never happen!
            Debug.LogError("Clear counter already has a kitchen object");
        }

        this.clearCounter = clearCounter;
        this.clearCounter.SetKitchenObject(this);

        transform.parent = clearCounter.GetTopPoint();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter() 
    {
        return this.clearCounter;
    }
}
