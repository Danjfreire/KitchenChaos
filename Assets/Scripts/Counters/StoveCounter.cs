using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : KitchenCounter
{
    private enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State state;
    private FryingRecipeSO currentFryingRecipe;
    private BurningRecipeSO currentBurningRecipe;
    private float fryingTimer;
    private float burningTimer;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (!HasKitchenObject()) {
            return;
        }

        switch (state) {
            case State.Idle:
                {
                    break;
                }
            case State.Frying: 
                {
                    fryingTimer += Time.deltaTime;

                    if (fryingTimer >= currentFryingRecipe.maxFryingDuration) {
                        //Fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(currentFryingRecipe.output, this);
                        state = State.Fried;
                        currentBurningRecipe = GetBurningRecipeForInput(GetKitchenObject().GetKitchenObjectSO());
                        burningTimer = 0;
                    }

                    break;
                }
            case State.Fried: 
                {
                    burningTimer += Time.deltaTime;

                    if (burningTimer >= currentBurningRecipe.maxBurningDuration) {
                        //Fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(currentBurningRecipe.output, this);
                        state = State.Burned;
                    }
                    break;
                }
            case State.Burned: 
                {
                    break;
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
                    kitchenObject.SetKitchenObjectParent(this);

                    currentFryingRecipe = GetFryingRecipeForInput(kitchenObject.GetKitchenObjectSO());
                    state = State.Frying;
                    fryingTimer = 0;
                }
            }
        } else {
            // give kitchen object to player if possible
            if (!player.HasKitchenObject()) {
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
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

    private bool HasBurningRecipe(KitchenObjectSO kitchenObjectSO)
    {
        BurningRecipeSO recipe = GetBurningRecipeForInput(kitchenObjectSO);

        return recipe != null;
    }

    private KitchenObjectSO GetBurningOutput(KitchenObjectSO inputObject)
    {
        BurningRecipeSO recipe = GetBurningRecipeForInput(inputObject);

        if (recipe != null && recipe.output != null) {
            return recipe.output;
        }

        return null;

    }

    private BurningRecipeSO GetBurningRecipeForInput(KitchenObjectSO inputObject)
    {
        foreach (BurningRecipeSO recipe in burningRecipeSOArray) {
            if (recipe.input == inputObject) {
                return recipe;
            }
        }

        return null;
    }
}
