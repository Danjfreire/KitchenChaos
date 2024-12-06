using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct IngredientMap
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plate;
    [SerializeField] private List<IngredientMap> ingredientMaps;

    private void Start()
    {
        plate.OnIngredientAdded += Plate_OnIngredientAdded;

        foreach (IngredientMap map in ingredientMaps){
           map.gameObject.SetActive(false);
        }
    }

    private void Plate_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (IngredientMap map in ingredientMaps) {
            if (e.ingredientSO == map.kitchenObjectSO) {
                map.gameObject.SetActive(true);
            }
        }
    }
}
