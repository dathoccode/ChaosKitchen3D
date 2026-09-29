using JetBrains.Annotations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    public event EventHandler OnDeliverySuccessed;
    public event EventHandler OnDeliveryFailed;
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private MenuSO menuSO;
    private List<RecipeSO> waitingRecipeList;

    private float recipeSpawnTimer;
    private float recipeSpawnTimerMax = 10f;
    private int recipeWaitingAmountMax = 4;
    private int reicipeDeliveredAmount = 0;

    private void Awake()
    {
        Instance = this;
        waitingRecipeList = new List<RecipeSO>();
    }

    private void Update()
    {
        SpawnRecipe();
    }

    private void SpawnRecipe()
    {
        recipeSpawnTimer -= Time.deltaTime;
        if (recipeSpawnTimer <= 0)
        {
            // Stop spawning new waiting recipe when it's fulled
            if (waitingRecipeList.Count >= recipeWaitingAmountMax) return;
            recipeSpawnTimer = recipeSpawnTimerMax;
            RecipeSO waitingRecipeSO = menuSO.recipeSOList[UnityEngine.Random.Range(0, menuSO.recipeSOList.Count)];
            waitingRecipeList.Add(waitingRecipeSO);

            OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool DeliveryRecipe(Plate plate)
    {
        foreach(RecipeSO recipe in waitingRecipeList)
        {
            if(recipe.kitchenObjectSOList.Count == plate.GetKitchenObjectSOInPlate().Count)
            {
                // Has the same number of ingredients
                bool isPlateMatchAnyOrder = true;
                foreach(KitchenObjectSO kitchenObjectSO in recipe.kitchenObjectSOList)
                {
                    bool isIngredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plate.GetKitchenObjectSOInPlate())
                    {
                        if(kitchenObjectSO == plateKitchenObjectSO)
                        {
                            // found the similar ingredient, look for others
                            isIngredientFound = true;
                            break;
                        }
                    }
                    if (isIngredientFound == false)
                    {
                        // Cant find the ingredient, stop comparing
                        isPlateMatchAnyOrder = false;
                    }
                }
                if (isPlateMatchAnyOrder)
                {
                    // Player delivered the correct recipe
                    waitingRecipeList.Remove(recipe);
                    OnDeliverySuccessed?.Invoke(this, EventArgs.Empty);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    reicipeDeliveredAmount++;
                    return true;
                }
            }
        }

        OnDeliveryFailed?.Invoke(this, EventArgs.Empty);
        return false;
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeList;
    }

    public int GetRecipeDeliveredAmount()
    {
        return reicipeDeliveredAmount;
    }
}
