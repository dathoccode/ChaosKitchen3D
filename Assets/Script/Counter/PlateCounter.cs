using System;
using System.Threading;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    private readonly int plateMaxAmount = 4;

    [SerializeField] private float spawnPlateTimerMax = 4f;
    [SerializeField] private KitchenObjectSO plateSO;
    private float spawnPlateTimer;
    private int plateSpawnedAmount = 0;
    

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer >= spawnPlateTimerMax)
        {
            spawnPlateTimer = 0;
            if (plateSpawnedAmount < plateMaxAmount)
            {
                plateSpawnedAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }

    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject() && plateSpawnedAmount > 0)
        {
            OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            KitchenObject.SpawnKitchenObject(plateSO, player);
            plateSpawnedAmount--;
        }
        
    }
}
