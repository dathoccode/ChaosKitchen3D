using TMPro;
using UnityEngine;

public class WaitingRecipeUIPrefab : BaseUI
{
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private Transform iconPrefab;
    [SerializeField] private Transform iconContainer;

    public void SetRecipeSO(RecipeSO recipe)
    {
        recipeName.text = recipe.name;
        foreach (KitchenObjectSO kitchenObjectSO in recipe.kitchenObjectSOList)
        {
            IconPrefab icon = Instantiate(iconPrefab, iconContainer).GetComponent<IconPrefab>();
            icon.SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
