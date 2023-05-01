using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;



public class ActivateTeleportationArray : MonoBehaviour
{
    [SerializeField] GameObject leftTeleportation;
    [SerializeField] GameObject rightTeleportation;


    [SerializeField] InputActionProperty leftActivate;
    [SerializeField] InputActionProperty rightActivate;
    [SerializeField] InputActionProperty leftCancel;
    [SerializeField] InputActionProperty rightCancel;


    void Update()
    {
        //leftTeleportation.SetActive(false);
        //rightTeleportation.SetActive(false);

        leftTeleportation.SetActive(leftCancel.action.ReadValue<float>() == 0 && leftActivate.action.ReadValue<float>() > 0.1f);
        rightTeleportation.SetActive(rightCancel.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1f);
    }
}
