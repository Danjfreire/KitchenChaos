using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{

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
        return true;
    }
}
