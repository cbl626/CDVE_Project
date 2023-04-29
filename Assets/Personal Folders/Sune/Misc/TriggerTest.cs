using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    [SerializeField] string stringToLog = "Trigger is working!";

    // Update is called once per frame
    public void TestTrigger()
    {
        Debug.Log(stringToLog);
    }
}
