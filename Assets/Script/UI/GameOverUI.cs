using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI recipeDeliveredText;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button menuButton;

    private void Awake()
    {
        replayButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameScene);
        });
        menuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        replayButton.Select();  
    }

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
