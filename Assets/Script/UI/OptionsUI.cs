using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : BaseUI
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private Transform rebindUI;

    [Header("Key Bind") ]
    [SerializeField] private TextMeshProUGUI moveUpButtonText;
    [SerializeField] private TextMeshProUGUI moveDownButtonText;
    [SerializeField] private TextMeshProUGUI moveLeftButtonText;
    [SerializeField] private TextMeshProUGUI moveRightButtonText;
    [SerializeField] private TextMeshProUGUI interactButtonText;
    [SerializeField] private TextMeshProUGUI alternateInteractButtonText;
    [SerializeField] private TextMeshProUGUI pauseButtonText;
    [SerializeField] private TextMeshProUGUI gamepadInteractButtonText;
    [SerializeField] private TextMeshProUGUI gamepadAlternateInteractButtonText;
    [SerializeField] private TextMeshProUGUI gamepadPauseButtonText;

    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button alternateInteractButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadAlternateInteractButton;
    [SerializeField] private Button gamepadPauseButton;


    private void Awake()
    {
        Instance = this;

        soundEffectButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        exitButton.onClick.AddListener(() =>
        {
            Hide();
            GamePauseUI.Instance.Show();
        });

        moveUpButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Up);
        });

        moveDownButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Down);
        });

        moveLeftButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Left);
        });

        moveRightButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Right);
        });

        interactButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Interact);
        });

        alternateInteractButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.InteractAlternate);
        });

        pauseButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Pause);
        });

        gamepadInteractButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Gamepad_Interact);
        });

        gamepadAlternateInteractButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Gamepad_InteractAlternate);
        });

        gamepadPauseButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Gamepad_Pause);
        });
    }

    private void Start()
    {
        UpdateVisual();
        Hide();
        HideRebindUI();
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectText.text = "Sound Effects: " + Mathf.RoundToInt(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.RoundToInt(MusicManager.Instance.GetVolume() * 10f);

        moveUpButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        alternateInteractButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        gamepadInteractButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        gamepadAlternateInteractButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        gamepadPauseButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);

        // prevent buttons from displaying too long text
        moveUpButtonText.text = moveUpButtonText.text.Substring(0, Mathf.Min(moveUpButtonText.text.Length, 3));
        moveDownButtonText.text = moveDownButtonText.text.Substring(0, Mathf.Min(moveDownButtonText.text.Length, 3));
        moveLeftButtonText.text = moveLeftButtonText.text.Substring(0, Mathf.Min(moveLeftButtonText.text.Length, 3));
        moveRightButtonText.text = moveRightButtonText.text.Substring(0, Mathf.Min(moveRightButtonText.text.Length, 3));
        interactButtonText.text = interactButtonText.text.Substring(0, Mathf.Min(interactButtonText.text.Length, 3));
        alternateInteractButtonText.text = alternateInteractButtonText.text.Substring(0, Mathf.Min(alternateInteractButtonText.text.Length, 3));
        pauseButtonText.text = pauseButtonText.text.Substring(0, Mathf.Min(pauseButtonText.text.Length, 3));
        gamepadInteractButtonText.text = gamepadInteractButtonText.text.Substring(0, Mathf.Min(gamepadInteractButtonText.text.Length, 3));
        gamepadAlternateInteractButtonText.text = gamepadAlternateInteractButtonText.text.Substring(0, Mathf.Min(gamepadAlternateInteractButtonText.text.Length, 3));
        gamepadPauseButtonText.text = gamepadPauseButtonText.text.Substring(0, Mathf.Min(gamepadPauseButtonText.text.Length, 3));
    }

    private void ShowRebindUI()
    {
        rebindUI.gameObject.SetActive(true);
    }

    private void HideRebindUI()
    {
        rebindUI.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowRebindUI();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HideRebindUI();
            UpdateVisual();
        });
    }

    public override void Show()
    {
        base.Show();
        soundEffectButton.Select();
    }
}

