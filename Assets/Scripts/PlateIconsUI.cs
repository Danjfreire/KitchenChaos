using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{

    [SerializeField] private PlateKitchenObject plate;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plate.OnIngredientAdded += Plate_OnIngredientAdded;
    }

    private void Plate_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        // remove previous icons
        foreach(Transform child in transform) {
            if (child == iconTemplate) continue; // don't destroy the icon template
            Destroy(child.gameObject);
        }

       foreach(KitchenObjectSO ingredient in plate.GetIngredientList()) {
            Transform icon = Instantiate(iconTemplate, transform);
            icon.gameObject.SetActive(true);
            icon.GetComponent<PlateIconsSingleUI>().SetKitchenObjectSO(ingredient);
        }
    }
}
