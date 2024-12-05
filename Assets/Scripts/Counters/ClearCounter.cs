using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : KitchenCounter
{

    public override void Interact(Player player)
    {
        if (!HasKitchenObject()) {
            // place player kitchen object on clear counter if possible
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        } else {
            if (!player.HasKitchenObject()) {
                // If player is not holding anything, pick the object
                GetKitchenObject().SetKitchenObjectParent(player);

            } else {
                // If player is holding a plate, add ingredient to plate
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate)) {
                   if(plate.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                   }
                } else {
                    // If the object on the counter is plate, try to put the player object on the plate
                    if (GetKitchenObject().TryGetPlate(out plate)) {
                        // Counter has a plate
                       if(plate.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
        }
    }
}
