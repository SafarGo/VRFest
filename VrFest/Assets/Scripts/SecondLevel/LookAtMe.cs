using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMe : MonoBehaviour
{
    public Transform Player;
    void Update()
    {
        transform.LookAt(Player);
    }
}
