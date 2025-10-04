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
    public int newput_cloth = 0;
    public bool cloth_up = false;
    public bool cloth_down = false;

    public SocketController upSocketController;
    public SocketController downSocketController;

    private void Start()
    {

    }

    private void Update()
    {
        string upSocketTag = upSocketController != null ? upSocketController.placedObjectTag : "";
        string downSocketTag = downSocketController != null ? downSocketController.placedObjectTag : "";
        Debug.Log($"Верхний сокет: {upSocketTag}, Нижний сокет: {downSocketTag}");
        if (put_cloth == 3)
        {
            if (upSocketTag == "TShirt" && downSocketTag == "Dick")
            {
                ObjectsActivator.instance.Activate(1);
                put_cloth++;
                upSocketController.obj.GetComponent<XRGrabInteractable>().enabled = false;
                downSocketController.obj.GetComponent<XRGrabInteractable>().enabled = false;
            }
        }

        if (put_cloth == 2)
        {
            ObjectsActivator.instance.Activate(0);
            put_cloth++;
        }

        if (buttons == 0)
        {
            up_cloth.GetComponent<XRGrabInteractable>().enabled = true;
            down_cloth.GetComponent<XRGrabInteractable>().enabled = true;
        }
    }

    public void UnderCloth()
    {
        put_cloth++;
    }

}