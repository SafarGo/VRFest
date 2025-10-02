using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPrefab_controller : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Injury"))
        {
            Destroy(this.gameObject);
        }
        if(collision.gameObject.CompareTag("Default"))
        {
            Destroy(this.gameObject);
        }
    }
}
