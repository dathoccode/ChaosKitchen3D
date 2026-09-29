using UnityEngine;
using UnityEngine.UI;

public class IconPrefab : BaseUI
{
    [SerializeField] private Image background;
    [SerializeField] private Image icon;

    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        icon.sprite = kitchenObjectSO.sprite;
    }
}
