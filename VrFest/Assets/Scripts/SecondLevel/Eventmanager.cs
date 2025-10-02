using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Eventmanager : MonoBehaviour
{
    public static int buttons = 3;

    private void Update()
    {
        Debug.Log(buttons);
        if (buttons == 0)
        {
            GameObject cloth = GameObject.Find("cloth");
            cloth.GetComponent<XRGrabInteractable>().enabled = true;
            cloth.GetComponent<Rigidbody>().isKinematic = false;


        }
    }
}
