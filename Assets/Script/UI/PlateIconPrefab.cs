using UnityEngine;
using UnityEngine.UI;

public class PlateIconPrefab : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image icon;

    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        icon.sprite = kitchenObjectSO.sprite;
    }
}
