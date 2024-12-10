using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : KitchenCounter
{
    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public override void Interact(Player player)
    {
        if(player.HasKitchenObject()) {
            // only accepts plates
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate)) {

                DeliveryManager.Instance.DeliverRecipe(plate);
                plate.DestroySelf();
            }
        }
    }

}
