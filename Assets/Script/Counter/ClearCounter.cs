using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // There is no kitchen object on the counter
            if (player.HasKitchenObject())
            {
                // Player is carrying a kitchen object, place it on the counter
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
                // Player is carrying a kitchen object
                if (GetKitchenObject().TryGetPlate(out Plate plate))
                {
                    // counter is hold a plate
                    if (plate.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        player.GetKitchenObject().DestroySelf();
                        return;
                    }
                }
                if (player.GetKitchenObject().TryGetPlate(out plate))
                {
                    // player is carrying a plate
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
            }
        }
    }
}
