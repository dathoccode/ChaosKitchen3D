using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : BaseUI
{
    [SerializeField] private Image timerImage;

    private void Update()
    {
        timerImage.fillAmount = GameManager.Instance.GetPlayingTimerNormalized();
    }
}
