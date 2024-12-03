using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : KitchenCounter
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

    private FryingRecipeSO currentRecipe; 
    private float fryingTimer;

    private void Update()
    {
        if (HasKitchenObject()) {
            fryingTimer += Time.deltaTime;

            if (fryingTimer >= currentRecipe.maxFryingDuration) {
                //Fried
                fryingTimer = 0;
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(currentRecipe.output, this);
            }
        }
        
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject()) {
            // place player kitchen object on clear counter if possible
            if (player.HasKitchenObject()) {
                KitchenObject kitchenObject = player.GetKitchenObject();

                if (HasFryingRecipe(kitchenObject.GetKitchenObjectSO())) {
                    Debug.Log("Valid Object");
                    kitchenObject.SetKitchenObjectParent(this);

                    currentRecipe = GetFryingRecipeForInput(kitchenObject.GetKitchenObjectSO());

                    ////cuttingProgress = 0;

                    //FryingRecipeSO recipe = GetFryingRecipeForInput(kitchenObject.GetKitchenObjectSO());

                    //OnCuttingProgressChanged?.Invoke(this, new OnCuttingProgressChangedEventArgs {
                    //    normalizedProgress = (float)cuttingProgress / recipe.requiredCuts
                    //});
                }
            }
        } else {
            // give kitchen object to player if possible
            if (!player.HasKitchenObject()) {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    private bool HasFryingRecipe(KitchenObjectSO kitchenObjectSO)
    {
        FryingRecipeSO recipe = GetFryingRecipeForInput(kitchenObjectSO);

        return recipe != null;
    }

    private KitchenObjectSO GetFryingOutput(KitchenObjectSO inputObject)
    {
        FryingRecipeSO recipe = GetFryingRecipeForInput(inputObject);

        if (recipe != null && recipe.output != null) {
            return recipe.output;
        }

        return null;

    }

    private FryingRecipeSO GetFryingRecipeForInput(KitchenObjectSO inputObject)
    {
        foreach (FryingRecipeSO recipe in fryingRecipeSOArray) {
            if (recipe.input == inputObject) {
                return recipe;
            }
        }

        return null;
    }

}
