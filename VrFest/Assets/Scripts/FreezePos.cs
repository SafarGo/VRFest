using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePos : MonoBehaviour
{
    public GameObject obj;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    public static bool isFrozen = true;

    void Start()
    {
            initialPosition = obj.transform.position;
            initialRotation = obj.transform.rotation;

    }

    void Update()
    {
        if (isFrozen)
        {
            obj.transform.position = initialPosition;
            obj.transform.rotation = initialRotation;
        }
    }

    public void Unfreeze()
    {
        isFrozen = false;
    }

}