using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeDelivered;
    public event EventHandler OnDeliverySuccess;
    public event EventHandler OnDeliveryFailure;

    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;
    
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeCooldown = 4f;
    private int maxWaitingRecipes = 4;
    private int successfullDeliveries = 0;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer += Time.deltaTime;
        if (spawnRecipeTimer >= spawnRecipeCooldown) {
            spawnRecipeTimer = 0;

            if (GameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < maxWaitingRecipes) {
                // pick a random recipe and add it to the list of pending recipes
                RecipeSO recipe = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(recipe);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public bool DeliverRecipe(PlateKitchenObject plate) 
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++) {
            RecipeSO recipe = waitingRecipeSOList[i];
            
            // ignore if the amount of ingredients is different
            if (recipe.ingredients.Count != plate.GetIngredientList().Count) {
                continue;
            }

            bool plateMatchesRecipe = true;

            foreach (KitchenObjectSO ingredient in recipe.ingredients) {
                bool hasIngredient = false;

                foreach (KitchenObjectSO requiredIngredient in plate.GetIngredientList()) {
                    if (ingredient == requiredIngredient) {
                        hasIngredient = true;
                        break;
                    }
                }

                if (!hasIngredient) {
                    plateMatchesRecipe = false;
                    break;
                }
            }

            if (plateMatchesRecipe) {
                // Player delivered the correct recipe
                waitingRecipeSOList.RemoveAt(i);
                OnRecipeDelivered?.Invoke(this, EventArgs.Empty);
                OnDeliverySuccess?.Invoke(this, EventArgs.Empty);
                successfullDeliveries++;
                return true;
            }
        }

        // Played delived incorrect recipe
        OnRecipeDelivered?.Invoke(this, EventArgs.Empty);
        OnDeliveryFailure?.Invoke(this, EventArgs.Empty);
        return false;
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccessfullDeliveries()
    {
        return successfullDeliveries;
    }
}
