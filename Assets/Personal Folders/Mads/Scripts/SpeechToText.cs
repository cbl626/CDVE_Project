﻿using System.Collections;
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
	
	// Reference to the speech recorder button, which can only be invoked via this script
	public Button recordSpeechButton;
	
	// Time in seconds before spoken text prompt is parsed.
	public float delayedTextPromptParser = 20f;
	
	// Time in seconds before delayed text removal will occur.
	public float delayedTextRemovalTime = 5f;

	// Detect when the VR controller pointer clicks on the target object
	private void OnMouseDown()
	{
		if (gameObject.CompareTag(interactableTablet))
		{
			Debug.Log("Placeholder: OnMouseDown detected on VR target object.");
			recorderInstruction.SetActive(true);
			recordSpeechButton.onClick.Invoke();
			Invoke("DelayedTextPromptParser", delayedTextPromptParser);
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
	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag(player) && Input.GetKeyDown(KeyCode.E))
		{			
			interactionInstruction.SetActive(false);
			recorderInstruction.SetActive(true);
			recordSpeechButton.onClick.Invoke();
			Invoke("DelayedTextPromptParser", delayedTextPromptParser);
		}
	}
	
	// Detect when the player leaves the trigger area
	private void OnTriggerExit(Collider other)
	{   
		if (other.CompareTag(player))
		{
			// Set the UI text message object to inactive
			interactionInstruction.SetActive(false);
			//Invoke("DelayedTextRemover", delayedTextRemovalTime);
		}
	}
	
	private void DelayedTextPromptParser()
	{
		// Get the original text from the speechTranscription GameObject
		string originalText = speechTranscription.GetComponent<Text>().text;

		// Remove the "I heard: " prefix using a regular expression
		string processedText = Regex.Replace(originalText, "^I heard: ", "");

		// Do something with the new text variable
		Debug.Log("Processed text: " + processedText);	
		
		Invoke("DelayedTextRemover", delayedTextRemovalTime);
	}
	
	private void DelayedTextRemover()
	{
		recorderInstruction.SetActive(false);
		speechTranscription.GetComponent<Text>().text = "";
	}
}