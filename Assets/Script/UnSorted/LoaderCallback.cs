using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool isFistUpdate = true;

    private void Update()
    {
        if (isFistUpdate)
        {
            isFistUpdate = false;
            Loader.LoaderCallback();
        }
    }
}
