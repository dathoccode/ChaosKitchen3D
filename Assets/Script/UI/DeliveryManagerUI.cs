using UnityEngine;

public class DeliveryManagerUI : BaseUI
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeUIPrefab;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in container)
        {
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            WaitingRecipeUIPrefab recipeUI = Instantiate(recipeUIPrefab, container).GetComponent<WaitingRecipeUIPrefab>();
            recipeUI.SetRecipeSO(recipeSO);
        }
    }
}
