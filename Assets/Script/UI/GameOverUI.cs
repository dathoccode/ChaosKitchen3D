using System;
using TMPro;
using UnityEngine;

public class GameOverUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI recipeDeliveredText;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void Update()
    {
        recipeDeliveredText.text = DeliveryManager.Instance.GetRecipeDeliveredAmount().ToString();
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
