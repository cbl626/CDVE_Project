using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechToTextDeprecated : MonoBehaviour
{	
	// Tag associated with the target object in VR
	public string interactableTablet = "Interactable_Tablet";
	
	// Tag associated with the target object in regular First-Person 3D
	public string tabletAreaCollider = "Tablet_Area_Collider";

	// Reference to the UI Text message that explains how to interact with the tablet/canvas
	public GameObject interactionInstruction;
	
	// Reference to the UI Text message that explains how to interact with the speech recorder
	public GameObject recorderInstruction;
	
	// Reference to the object that holds the speech transcription text
	public GameObject speechTranscription;
	
	// Reference to the speech recorder button, which can only be invoked via this script
	public Button recordSpeechButton;
	
	// Time in seconds before delayed text removal will occur.
	public float delayedTextPromptParser = 20f;
	
	// Time in seconds before delayed text removal will occur.
	public float delayedTextRemovalTime = 30f;

	// Detect when the VR controller pointer clicks on the target object
	private void OnMouseDown()
	{
		if (gameObject.CompareTag(interactableTablet))
		{
			Debug.Log("Placeholder: OnMouseDown detected on VR target object.");
			recorderInstruction.SetActive(true);
			recordSpeechButton.onClick.Invoke();
			Invoke("DelayedTextRemover", delayedTextRemovalTime);
		}
	}
	
	// Detect when the player enters the trigger area of the target object
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(tabletAreaCollider))
		{
			// Set the UI text message object to active
			interactionInstruction.SetActive(true);
		}
	}

	// Detect when the player stays inside the trigger area of the target object
	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag(tabletAreaCollider) && Input.GetKeyDown(KeyCode.E))
		{			
			interactionInstruction.SetActive(false);
			recorderInstruction.SetActive(true);
			recordSpeechButton.onClick.Invoke();
			Invoke("DelayedTextRemover", delayedTextRemovalTime);
		}
	}
	
	// Detect when the player leaves the trigger area
	private void OnTriggerExit(Collider other)
	{   
		if (other.CompareTag(tabletAreaCollider))
		{
			// Set the UI text message object to inactive
			interactionInstruction.SetActive(false);
			Invoke("DelayedTextRemover", delayedTextRemovalTime);
		}
	}
	
	private void DelayedTextPromptParser()
	{
		//speechTranscription.GetComponent<Text>();
	}
	
	private void DelayedTextRemover()
	{
		recorderInstruction.SetActive(false);
		speechTranscription.GetComponent<Text>().text = "";
	}
}