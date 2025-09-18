using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Injury : MonoBehaviour
{
    public float Lives = 100;
    public InjuryLevelController LevelController;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Water"))
        {
            Lives -= 10;
        }
    }

    private void Update()
    {
        if(Lives<=0)
        {
            LevelController.IsWaterDropped = true;
        }
    }

    public void Bandage()
    {
        LevelController.IsBandageApplied = true;
        GameObject bandage = GameObject.Find("Bandage");
        bandage.GetComponent<XRGrabInteractable>().enabled = false;
    }
}
