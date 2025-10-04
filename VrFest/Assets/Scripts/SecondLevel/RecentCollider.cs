using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecentCollider : MonoBehaviour
{
    public string Tag = "";
    private void OnTriggerEnter(Collider other)
    {
        Tag = other.gameObject.tag;
    }
}
