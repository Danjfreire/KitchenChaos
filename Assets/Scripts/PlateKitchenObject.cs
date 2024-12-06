using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO ingredientSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectsSO;

    private List<KitchenObjectSO> kitchenObjectList;

    private void Awake()
    {
        kitchenObjectList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        // Only accept valid objects
        if (!validKitchenObjectsSO.Contains(kitchenObjectSO)) {
            return false;
        }

        // Check if object has already been added
        if (kitchenObjectList.Contains(kitchenObjectSO)) {
            return false;
        }

        kitchenObjectList.Add(kitchenObjectSO);
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs { ingredientSO = kitchenObjectSO });
        return true;
    }
}
