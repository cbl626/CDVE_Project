using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTargetCubes : MonoBehaviour
{
    Transform[] children;
    void Start()
    {
        children = gameObject.GetComponentsInChildren<Transform>();
    }

    public void ResetObjects()
    {
        Debug.Log("EVENT FIRED!");
        foreach (Transform child in children)
        {
            //Rigidbody rb = child.GetComponent<Rigidbody>();
            //rb.velocity = Vector3.zero;
            child.localPosition = Vector3.zero;
            child.localRotation = Quaternion.identity;
            child.localScale = Vector3.one;
        }
    }
}
