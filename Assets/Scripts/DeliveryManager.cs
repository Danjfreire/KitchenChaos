using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;
    
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeCooldown = 4f;
    private int maxWaitingRecipes = 4;

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

            if (waitingRecipeSOList.Count < maxWaitingRecipes) {
                // pick a random recipe and add it to the list of pending recipes
                RecipeSO recipe = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(recipe);
                Debug.Log("Requesting recipe:" + recipe.recipeName);
            }
        }
    }

    public bool DeliverRecipe(PlateKitchenObject plate) 
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++) {
            RecipeSO recipe = waitingRecipeSOList[i];
            Debug.Log("Start cheking for " + recipe.recipeName);

            // ignore if the amount of ingredients is different
            if (recipe.ingredients.Count != plate.GetIngredientList().Count) {
                Debug.Log("Skip because of ingredient count");
                continue;
            }

            bool plateMatchesRecipe = true;

            foreach (KitchenObjectSO ingredient in recipe.ingredients) {
                bool hasIngredient = false;

                foreach (KitchenObjectSO requiredIngredient in plate.GetIngredientList()) {
                    if (ingredient == requiredIngredient) {
                        hasIngredient = true;
                        Debug.Log("Found ingredient:" + ingredient.objectName);
                        break;
                    }

                    Debug.Log("Did not Found ingredient:" + requiredIngredient.objectName);
                }

                if (!hasIngredient) {
                    plateMatchesRecipe = false;
                    break;
                }
            }

            if (plateMatchesRecipe) {
                // Player delivered the correct recipe
                Debug.Log("Delivered correct recipe at index:" + i);
                waitingRecipeSOList.RemoveAt(i);
                return true;
            }

            Debug.Log("-------------------------------------------------------------------");
        }

        Debug.Log("No recipe match");
        return false;
    }
}
