using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : KitchenCounter
{
    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;

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

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject()) {
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }
    }
}
