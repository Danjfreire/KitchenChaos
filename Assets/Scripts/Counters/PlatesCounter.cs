using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : KitchenCounter
{

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float plateSpawnTimer;
    private float plateSpawnInterval = 4f;
    private int plateAmount;
    private int plateAmountMax = 4;

    private void Update()
    {
        plateSpawnTimer += Time.deltaTime;

        if (plateSpawnTimer >= plateSpawnInterval) {
            plateSpawnTimer = 0f;
            if (plateAmount < plateAmountMax) {
                plateAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
       if(!player.HasKitchenObject()) {
            if (plateAmount > 0) {
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                plateAmount--;
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        } 
    }

}
