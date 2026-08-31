using System;
using System.Collections.Generic;
using UnityEngine;

public class Plate : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectList;

    private List<KitchenObjectSO> kitchenObjectSOList;
    
    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!validKitchenObjectList.Contains(kitchenObjectSO))
        {
            // Not a valid ingredient to put on a plate
            return false;
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            // Already has this type
            return false;
        }
        kitchenObjectSOList.Add(kitchenObjectSO);
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
        {
            kitchenObjectSO = kitchenObjectSO,
        });
        return true;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOInPlate()
    {
        return kitchenObjectSOList;
    }
}
