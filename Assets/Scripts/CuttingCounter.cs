using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : KitchenCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject()) {
            // place player kitchen object on clear counter if possible
            if (player.HasKitchenObject()) {
                KitchenObject kitchenObject = player.GetKitchenObject();

                if (HasCuttingRecipe(kitchenObject.GetKitchenObjectSO())) {
                    kitchenObject.SetKitchenObjectParent(this);
                }
            }
        } else {
            // give kitchen object to player if possible
            if (!player.HasKitchenObject()) {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject()) {
            KitchenObjectSO slicedKitchenObjectSO = GetCuttingOutput(GetKitchenObject().GetKitchenObjectSO());

            // Only cut if input object has cutting recipe
            if (slicedKitchenObjectSO != null) {
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(slicedKitchenObjectSO, this);
            }
        }
    }

    private bool HasCuttingRecipe(KitchenObjectSO kitchenObjectSO)
    {
        foreach (CuttingRecipeSO recipe in cuttingRecipeSOArray) {
            if (recipe.input == kitchenObjectSO) {
                return true;
            }
        }

        return false;
    }

    private KitchenObjectSO GetCuttingOutput(KitchenObjectSO inputObject)
    {
        foreach(CuttingRecipeSO recipe in cuttingRecipeSOArray) {
            if (recipe.input == inputObject) {
                return recipe.output;
            }
        }

        return null;
    }
}
