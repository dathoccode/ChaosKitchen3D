using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    //testing
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

    private KitchenObject kitchenObject;

    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenObject != null)
            {
                kitchenObject.SetClearCounter(secondClearCounter);
                Debug.Log(transform.name);
            }
        }
    }
    public void Interact()
    {
        if (kitchenObject == null) 
        {
            GameObject kitchenObjectInstance = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectInstance.transform.localPosition = Vector3.zero;
            KitchenObject newKitchenObject = kitchenObjectInstance.GetComponent<KitchenObject>();
            newKitchenObject.SetClearCounter(this);
            Debug.Log("Spawned " + kitchenObject.name);
        }
        else
        {
            Debug.Log("Interact with: " + transform.name);
        }
    }
    
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
