using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    [SerializeField] private ClearCounter secondCounter;
    [SerializeField] private bool testing;

    private KitchenObject kitchenObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && testing) {
            if (kitchenObject != null) {
                kitchenObject.SetClearCounter(secondCounter);
            }
        }
    }

    public void Interact()
    {
        if (kitchenObject == null) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        } else {
            Debug.Log(kitchenObject.GetClearCounter());
        }
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public Transform GetTopPoint()
    {
        return counterTopPoint;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
}
