using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Injury : MonoBehaviour
{
    public float Lives = 1;
    public InjuryLevelController LevelController;
    public Slider Slider;
    public GameObject Socket;
    public static Injury instance;
    public bool is_cold = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Socket.GetComponent<XRSocketInteractor>().enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Water"))
        {
            Lives -= 0.1f;
        }
    }

    private void Update()
    {
        if(Lives<=0 && !is_cold)
        {
            LevelController.IsWaterDropped = true;
            Socket.GetComponent<XRSocketInteractor>().enabled = true;
            ObjectsActivator.instance.Activate(0);
            is_cold = true;
        }
        Slider.value = Lives;
    }

    public void Bandage()
    {
        GameObject obj = GameObject.Find("Bandage");
            LevelController.IsBandageApplied = true;
            obj.GetComponent<XRGrabInteractable>().enabled = false;
            obj.GetComponent<Rigidbody>().isKinematic = true;
            ObjectsActivator.instance.Activate(1);
            
    }
}
