using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : KitchenCounter
{

    public override void Interact(Player player)
    {
        if(player.HasKitchenObject()) {
            // only accepts plates
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate)) {
                plate.DestroySelf();
            }
        }
    }

}
