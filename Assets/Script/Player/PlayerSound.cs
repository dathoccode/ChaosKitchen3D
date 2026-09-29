using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private float footStepTimer;
    private float footStepTimerMax = .1f;

    private void Update()
    {
        footStepTimer -= Time.deltaTime;
        if (footStepTimer <= 0f)
        {
            footStepTimer = footStepTimerMax;
            if (Player.Instance.IsWalking)
            {
                SoundManager.Instance.PlayFootStepSound(transform.position);
            }
        }
    }
}
