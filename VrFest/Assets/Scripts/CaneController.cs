using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneController : MonoBehaviour
{
    public GameObject WaterPrefab;
    public float timer = 0.1f;

    public void SpawnWater()
    {
        Instantiate(WaterPrefab, transform.position, transform.rotation);
    }

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if(InjuryLevelController.instance.IsCrane && timer<=0)
        {
            SpawnWater();
            timer = 0.1f;
        }
    }
}
