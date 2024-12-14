using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : KitchenCounter, IHasProgress
{

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs: EventArgs
    {
        public State state;
    }
    public enum State
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

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        normalizedProgress = fryingTimer / currentFryingRecipe.maxFryingDuration
                    });

                    if (fryingTimer >= currentFryingRecipe.maxFryingDuration) {
                        //Fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(currentFryingRecipe.output, this);
                        state = State.Fried;
                        currentBurningRecipe = GetBurningRecipeForInput(GetKitchenObject().GetKitchenObjectSO());
                        burningTimer = 0;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                    }

                    break;
                }
            case State.Fried: 
                {
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        normalizedProgress = burningTimer / currentBurningRecipe.maxBurningDuration
                    });

                    if (burningTimer >= currentBurningRecipe.maxBurningDuration) {
                        //Fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(currentBurningRecipe.output, this);
                        state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            normalizedProgress = 0
                        });
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

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        normalizedProgress = fryingTimer / currentFryingRecipe.maxFryingDuration
                    });
                }
            }
        } else {
            // give kitchen object to player if possible
            if (!player.HasKitchenObject()) {
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    normalizedProgress = 0
                });
            } else {
                // If player is holding a plate, add ingredient to plate
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate)) {
                    if (plate.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }

                    state = State.Idle;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        normalizedProgress = 0
                    });
                }
            }
        }
    }

    public bool IsFried()
    {
        return state == State.Fried;
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
