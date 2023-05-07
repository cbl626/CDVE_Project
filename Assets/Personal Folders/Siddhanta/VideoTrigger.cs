using UnityEngine;
using UnityEngine.Video;

public class VideoTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Collider triggerCollider;

    private bool isPlaying = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isPlaying)
            {
                videoPlayer.targetCameraAlpha = 1f;
                videoPlayer.Play();
                isPlaying = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isPlaying)
            {
                videoPlayer.targetCameraAlpha = 0f;
                videoPlayer.Pause();
                isPlaying = false;
            }
        }
    }
}
