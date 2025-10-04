using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketController : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor socketInteractor;
    public string Tag;
    public bool isObjectPlaced;
    public string placedObjectTag;
    public GameObject obj;
    private void Start()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
        socketInteractor.selectEntered.AddListener(OnObjectPlaced);
        socketInteractor.selectExited.AddListener(OnObjectRemoved);
    }

    private void OnObjectPlaced(SelectEnterEventArgs args)
    {
        GameObject placedObject = args.interactableObject.transform.gameObject;
        placedObjectTag = placedObject.tag;
        obj = placedObject;
        if (placedObject.tag == Tag)
        {
            isObjectPlaced = true;
        }
    }

    private void OnObjectRemoved(SelectExitEventArgs args)
    {
        isObjectPlaced = false;
        placedObjectTag = "";
    }
}
