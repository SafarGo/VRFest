using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrudController : MonoBehaviour
{
    public int Count_of_hits = 0;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("!!!");
        if(other.CompareTag("Hand") && HandProximityCheck.instance.AreHandsTogether)
        {
            SlidersController.instance.CheckHit(GameObject.Find("SliderHit").GetComponent<Slider>().value);
            Count_of_hits++;
            Debug.Log(Count_of_hits);
        }
    }
}
