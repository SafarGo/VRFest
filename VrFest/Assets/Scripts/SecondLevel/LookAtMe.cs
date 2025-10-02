using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMe : MonoBehaviour
{

    void Update()
    {
        GameObject obj = GameObject.Find("Player");
        transform.LookAt(obj.transform);
    }
}
