using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsProgressController : MonoBehaviour
{
    public XRKnob xrKnob;
    public GameObject des;
    public Slider slider_ring;
    

    void Start()
    {
        xrKnob = this.GetComponent<XRKnob>();
    }

    private void Update()
    {
        slider_ring.value = xrKnob.value;

        if (xrKnob.value < 0)
        {
            Eventmanager.buttons--;
            Destroy(des);

        }
    }
}