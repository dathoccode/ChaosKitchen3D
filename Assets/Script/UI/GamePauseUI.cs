using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : BaseUI
{
    public static GamePauseUI Instance { get; private set; }    

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        Instance = this;
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePauseGame();
        });

        optionsButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show();
            Hide();
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
       GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
       GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnPaused;

       Hide();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void GameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }
}
