using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : KitchenCounter
{
    public event EventHandler<OnCuttingProgressChangedEventArgs> OnCuttingProgressChanged;
    public class OnCuttingProgressChangedEventArgs : EventArgs
    {
        public float normalizedProgress;
    }
    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress = 0;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject()) {
            // place player kitchen object on clear counter if possible
            if (player.HasKitchenObject()) {
                KitchenObject kitchenObject = player.GetKitchenObject();

                if (HasCuttingRecipe(kitchenObject.GetKitchenObjectSO())) {
                    kitchenObject.SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    CuttingRecipeSO recipe = GetCuttingRecipeForInput(kitchenObject.GetKitchenObjectSO());

                    OnCuttingProgressChanged?.Invoke(this, new OnCuttingProgressChangedEventArgs {
                        normalizedProgress = (float)cuttingProgress / recipe.requiredCuts
                    });
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
            if (slicedKitchenObjectSO == null) {
                return;
            }

            CuttingRecipeSO recipe = GetCuttingRecipeForInput(GetKitchenObject().GetKitchenObjectSO());

            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);

            if (cuttingProgress == recipe.requiredCuts) {
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(slicedKitchenObjectSO, this);
                cuttingProgress = 0;
            }

            OnCuttingProgressChanged?.Invoke(this, new OnCuttingProgressChangedEventArgs {
                normalizedProgress = (float)cuttingProgress / recipe.requiredCuts
            });
        }
    }

    private bool HasCuttingRecipe(KitchenObjectSO kitchenObjectSO)
    {
        CuttingRecipeSO recipe = GetCuttingRecipeForInput(kitchenObjectSO);

        return recipe != null;
    }

    private KitchenObjectSO GetCuttingOutput(KitchenObjectSO inputObject)
    {
        CuttingRecipeSO recipe = GetCuttingRecipeForInput(inputObject);
        
        if (recipe != null && recipe.output != null) {
            return recipe.output;
        }

        return null;

    }

    private CuttingRecipeSO GetCuttingRecipeForInput(KitchenObjectSO inputObject)
    {
        foreach (CuttingRecipeSO recipe in cuttingRecipeSOArray) {
            if (recipe.input == inputObject) {
                return recipe;
            }
        }

        return null;
    }
}