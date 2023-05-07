using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(MeshRenderer))]
public class DynamicResolutionTexture : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Camera viewerCamera;
    public float maxViewDistance = 1f;

    private MeshRenderer meshRenderer;
    private RenderTexture renderTexture;
    private float currentDistance;
    private float currentResolutionScale = 0.1f;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        // Create a new Render Texture and assign it to the Mesh Renderer's material
        renderTexture = new RenderTexture(1920,1080, 16, RenderTextureFormat.ARGB32);
        meshRenderer.material.mainTexture = renderTexture;
    }

    void Update()
    {
        if (videoPlayer == null || viewerCamera == null)
        {
            return;
        }

        // Calculate the distance between the viewer camera and the video object
        currentDistance = Vector3.Distance(viewerCamera.transform.position, transform.position);

        // Calculate the current resolution scale based on the viewer's distance from the video object
        currentResolutionScale = Mathf.Clamp(1f - (currentDistance / maxViewDistance), 0.25f, 1f);

        // Set the resolution scale on the Video Player's Render Mode
        videoPlayer.targetCameraAlpha = currentResolutionScale;

        // Update the Render Texture's size based on the resolution scale
        renderTexture.width = (int)(1920 * currentResolutionScale);
        renderTexture.height = (int)(1080 * currentResolutionScale);

        // Check if the video player is ready to play and if it's not already playing
        if (videoPlayer.isPrepared && !videoPlayer.isPlaying)
        {
            // Play the video
            videoPlayer.Play();
        }
    }
}
