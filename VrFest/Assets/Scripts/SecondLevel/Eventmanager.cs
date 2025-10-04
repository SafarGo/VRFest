using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Eventmanager : MonoBehaviour
{
    public static int buttons = 4;
    public GameObject up_cloth;
    public GameObject down_cloth;
    public int put_cloth = 0;

    private void Update()
    {
        Debug.Log(buttons);
        if (buttons == 0)
        {
            up_cloth.GetComponent<XRGrabInteractable>().enabled = true;
            down_cloth.GetComponent<XRGrabInteractable>().enabled = true;
        }
    }

    public void UnderCloth()
    {

    }

}
