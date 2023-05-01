using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class SpeechToText : MonoBehaviour
{	
	// Tag associated with the target object in VR
	public string interactableTablet = "Interactable_Tablet";
	
	// Tag associated with the player (used in case of regular First-Person 3D)
	public string player = "Player";

	// Reference to the UI Text message that explains how to interact with the tablet/canvas
	public GameObject interactionInstruction;
	
	// Reference to the UI Text message that explains how to interact with the speech recorder
	public GameObject recorderInstruction;
	
	// Reference to the object that holds the speech transcription text
	public GameObject speechTranscription;
	
	// Reference to the object that holds the paintable canvas
	public GameObject paintableCanvas;
	
	// Reference to the object that holds the interaction handler
	public GameObject interactionHandler;
	
	// Reference to the speech recorder button, which can only be invoked via this script
	public Button recordSpeechButton;
	
	// This variable represents an additional string that can be appended to the user's input prompt to modify its output.
	// The default value is an empty string, but it can be set to a custom value in the inspector.
	public string promptModifier = "";
	
	// Time in seconds before spoken text prompt is parsed.
	//public float delayedTextPromptParser = 20f; // No longer needed, but can potentially be added back.
	
	// Time in seconds before delayed text removal will occur.
	public float delayedTextRemovalTime = 5f;

	// Detect when the VR controller pointer clicks on the target object
	private IEnumerator OnMouseDown()
	{
		if (gameObject.CompareTag(interactableTablet))
		{
			Debug.Log("Placeholder: OnMouseDown detected on VR target object.");
			
			recorderInstruction.SetActive(true);
			
			recordSpeechButton.onClick.Invoke();
			
			Oculus.Voice.Demo.InteractionHandler recorderStatus = interactionHandler.GetComponent<Oculus.Voice.Demo.InteractionHandler>();
			
			bool isRunning = true;
			while (isRunning)
			{
				yield return new WaitForSeconds(1f);
		
				if (recorderStatus.IsActive == false)
				{
					TextPromptParser();
					isRunning = false;
				}
			}
		}
	}
	
	// Detect when the player enters the trigger area of the target object
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(player))
		{
			// Set the UI text message object to active
			interactionInstruction.SetActive(true);
		}
	}

	// Detect when the player stays inside the trigger area of the target object
	private IEnumerator OnTriggerStay(Collider other)
	{
		// Activates the voice recorder and passes the resulting text string to the TextPromptParser
		if (other.CompareTag(player) && Input.GetKeyDown(KeyCode.E))
		{			
			interactionInstruction.SetActive(false);

			recorderInstruction.SetActive(true);
			
			recordSpeechButton.onClick.Invoke();
			
			Oculus.Voice.Demo.InteractionHandler recorderStatus = interactionHandler.GetComponent<Oculus.Voice.Demo.InteractionHandler>();
			
			bool isRunning = true;
			while (isRunning)
			{
				yield return new WaitForSeconds(1f);
		
				if (recorderStatus.IsActive == false)
				{
					TextPromptParser();
					isRunning = false;
				}
			}
		}
	}
	
	// Detect when the player leaves the trigger area
	private void OnTriggerExit(Collider other)
	{   
		if (other.CompareTag(player))
		{
			// Set the UI text message object to inactive
			interactionInstruction.SetActive(false);
		}
	}
	
	private void TextPromptParser()
	{
		// Get the original text from the speechTranscription GameObject
		string originalSpeechInputText = speechTranscription.GetComponent<Text>().text;

		// Remove the "I heard: " prefix using a regular expression
		string processedSpeechInputText = Regex.Replace(originalSpeechInputText, "^I heard: ", "");
		
		StableDiffusionText2Material text2materialScriptComponent = paintableCanvas.GetComponent<StableDiffusionText2Material>();
			
		text2materialScriptComponent.prompt = processedSpeechInputText + " " + promptModifier;

		//Debug.Log("Prompt: " + text2materialScriptComponent.prompt);
		text2materialScriptComponent.Generate();
		
		Invoke("DelayedTextRemover", delayedTextRemovalTime);
	}
	
	private void DelayedTextRemover()
	{
		recorderInstruction.SetActive(false);
		speechTranscription.GetComponent<Text>().text = "";
	}
}