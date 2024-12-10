using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlaced;

    [SerializeField] protected KitchenObjectSO kitchenObjectSO;
    [SerializeField] protected Transform counterTopPoint;

    private KitchenObject kitchenObject;
    public virtual void Interact(Player player) { }
    public virtual void InteractAlternate(Player player) { }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public Transform GetAttachTransform()
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

        if(kitchenObject != null)
        {
            OnAnyObjectPlaced?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
}
