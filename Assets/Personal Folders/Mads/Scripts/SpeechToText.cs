using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechToText : MonoBehaviour
{	
	// Tag associated with the target object in VR
	public string interactableTablet = "Interactable_Tablet";
	
	// Tag associated with the target object in regular First-Person 3D
	public string tabletAreaCollider = "Tablet_Area_Collider";

	// Reference to the UI Text message
	public GameObject interactionInstruction;

	// Define a function that will execute when the object is clicked or the E key is pressed
	public void RecordSpeech()
	{
		// Debug.Log("Placeholder: RecordSpeech called from VR controller click.");
	}

	// Detect when the VR controller pointer clicks on the target object
	private void OnMouseDown()
	{
		if (gameObject.CompareTag(interactableTablet))
		{
			Debug.Log("Placeholder: OnMouseDown detected on VR target object.");
			// RecordSpeech();
		}
	}

	// Detect when the player stays inside the trigger area of the target object
	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag(tabletAreaCollider))
		{
			// Set the UI text message object to active
			interactionInstruction.SetActive(true);
			
			if (Input.GetKeyDown(KeyCode.E))
			{
				Debug.Log("Placeholder: E key pressed while player is close to interactable object.");
				// RecordSpeech();
			}
		}
	}
	
	// Detect when the player leaves the trigger area
	private void OnTriggerExit(Collider other)
	{   
		if (other.CompareTag(tabletAreaCollider))
		{
			// Set the UI text message object to inactive
			interactionInstruction.SetActive(false);
		}
	}
}