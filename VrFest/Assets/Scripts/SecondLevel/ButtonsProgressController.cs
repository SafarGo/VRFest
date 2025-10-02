using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;

public class ButtonsProgressController : MonoBehaviour
{
    public int buttons = 3;
    public bool is_rotate = false;
    private XRKnob xrKnob; 

    void Start()
    {
        xrKnob = GetComponent<XRKnob>(); 
    }

    private void Update()
    {
        if (xrKnob.value <= 0)
        {
            is_rotate = true;

        }
    }
}