using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Mouth") && InjuryLevelController.instance.IsBandageApplied)
        {
            InjuryLevelController.instance.giventablets += 1;
            Destroy(this.gameObject);
        }
    }
}
