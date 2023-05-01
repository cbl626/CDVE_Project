using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    // Reference to the game object with the StableDiffusionText2Material script
    public GameObject diffusionObject;

    private StableDiffusionText2Material diffusionScript;

    private void Start()
    {
        // Get the StableDiffusionText2Material script component from the game object
        diffusionScript = diffusionObject.GetComponent<StableDiffusionText2Material>();

        // Get the Button component on this game object and add a listener to the OnClick event
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    // This method will be called when the button is clicked
    private void OnClick()
    {
        if (diffusionScript != null)
        {
            diffusionScript.Generate();
        }
    }
}
