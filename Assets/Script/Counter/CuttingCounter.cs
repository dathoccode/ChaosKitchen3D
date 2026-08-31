using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{   
    public event EventHandler OnCut;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    [SerializeField] private CuttingRecipeSO[] cutKitchenObjectSOArray;

    private int cuttingProgress = 0;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // There is no kitchen object on the counter
            if (player.HasKitchenObject())
            {
                // Player is carrying a kitchen object, place it on the counter
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // Player is carrying a kitchen object that can be cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    ResetCuttingProgress();
                }
            }
            else
            {
                // Player is not carrying a kitchen object
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                // Player is carrying a plate
                if (player.GetKitchenObject().TryGetPlate(out Plate plate))
                {
                    if (plate.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
            else
            {
                // Player is not carrying a kitchen object, pick up the kitchen object from the counter
                GetKitchenObject().SetKitchenObjectParent(player);
                ResetCuttingProgress();
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (!HasKitchenObject())
        {
            // There is no kitchen object on the counter
        }
        else
        {
            if (player.HasKitchenObject())
            {
                // Player is carrying a kitchen object, do nothing
            }
            else
            {
                //Temp
                if (!HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) return;

                // Player is not carrying a kitchen object, pick up the kitchen object from the counter
                OnCut?.Invoke(this, EventArgs.Empty);
                UpdateCuttingProgress(cuttingProgress + 1);
                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromIput(GetKitchenObject().GetKitchenObjectSO());
                if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
                {
                    KitchenObjectSO outputKitchenObjectSO = GetOutputFromInput(GetKitchenObject().GetKitchenObjectSO());
                    GetKitchenObject().DestroySelf();
                    KitchenObject kitchenObject = KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
                }
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO input)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromIput(input);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO input)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromIput(input);
        return cuttingRecipeSO != null ? cuttingRecipeSO.output : null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOFromIput(KitchenObjectSO input)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cutKitchenObjectSOArray)
        {
            if (cuttingRecipeSO.input == input)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }

    private void UpdateCuttingProgress(int newCuttingProgress)
    {
        cuttingProgress = newCuttingProgress;
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromIput(GetKitchenObject().GetKitchenObjectSO());
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
        });
    }

    private void ResetCuttingProgress()
    {
        cuttingProgress = 0;
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            progressNormalized = (float)cuttingProgress
        });
    }
}
