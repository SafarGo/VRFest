using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Eventmanager : MonoBehaviour, ILevelsController
{
    public static Eventmanager instance;

    [SerializeField] private bool _isClothesOut;
    [SerializeField] private bool _isClothesIn;
    [SerializeField] private bool _isBlanked;

    public static int buttons = 4;

    public GameObject up_cloth;
    public GameObject down_cloth;
    public GameObject blanket;
    public GameObject first_socket;
    public GameObject second_socket;

    public static int put_cloth = 0;
    public int newput_cloth = 0;

    public bool cloth_up = false;
    public bool cloth_down = false;
    
    public SocketController upSocketController;
    public SocketController downSocketController;

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        string upSocketTag = upSocketController != null ? upSocketController.placedObjectTag : "";
        string downSocketTag = downSocketController != null ? downSocketController.placedObjectTag : "";

        if (put_cloth == 3)
        {
            if (upSocketTag == "TShirt" && downSocketTag == "Dick")
            {
                ObjectsActivator.instance.Activate(1);
                put_cloth++;
                _isClothesIn = true;
                blanket.SetActive(true);
            }
        }

        if (put_cloth == 2)
        {
            ObjectsActivator.instance.Activate(0);
            put_cloth++;
            _isClothesOut = true;

        }

        if (buttons == 0)
        {
            up_cloth.GetComponent<XRGrabInteractable>().enabled = true;
            down_cloth.GetComponent<XRGrabInteractable>().enabled = true;
            first_socket.SetActive(true);
            second_socket.SetActive(true);
            buttons--;

        }
    }

    public void UnderCloth(GameObject obj)
    {
        Destroy(obj);
        put_cloth++;
    }
    
    public void Blanket()
    {
        if (put_cloth == 4)
        {
            blanket.GetComponent<XRGrabInteractable>().enabled = false;
            ObjectsActivator.instance.Activate(2);
            _isBlanked = true;
        }
    }

    public bool CheckProgress()
    {
        return (_isBlanked && _isClothesOut && _isClothesIn) ? true : false;
    }
}