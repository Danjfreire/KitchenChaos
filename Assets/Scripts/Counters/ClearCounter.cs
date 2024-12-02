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
            // give kitchen object to player if possible
            if (!player.HasKitchenObject()) {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
