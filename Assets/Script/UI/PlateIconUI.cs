using UnityEngine;

public class PlateIconUI : BaseUI
{
    [SerializeField] private Plate plate;
    [SerializeField] Transform plateIconPrefab;

    private void Start()
    {
        plate.OnIngredientAdded += Plate_OnIngredientAdded;
    }

    private void Plate_OnIngredientAdded(object sender, Plate.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();

    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        foreach (KitchenObjectSO kitchenObjectSO in plate.GetKitchenObjectSOInPlate())
        {
            Transform plateIcon = Instantiate(plateIconPrefab, transform);
            plateIcon.GetComponent<IconPrefab>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
