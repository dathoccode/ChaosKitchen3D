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
    }

    private void Start()
    {
        UpdateVisual();
        Hide();
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
    }
}

