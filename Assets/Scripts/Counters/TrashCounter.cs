using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : KitchenCounter
{
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject()) {
            player.GetKitchenObject().DestroySelf();
        }
    }
}
