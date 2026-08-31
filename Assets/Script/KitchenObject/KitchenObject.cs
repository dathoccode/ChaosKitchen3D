using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("ClearCounter already has a KitchenObject!");
            return;
        }

        this.kitchenObjectParent = kitchenObjectParent;
        this.kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent parent)
    {
        KitchenObject kitchenObject = Instantiate(kitchenObjectSO.prefab).GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(parent);

        return kitchenObject;
    }

    public bool TryGetPlate(out Plate plate)
    {
        if (this is Plate)
        {
            plate = this as Plate;
            return true;
        }
        else
        {
            plate = null;
            return false;   
        }
    }
}
