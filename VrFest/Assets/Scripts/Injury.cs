using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Injury : MonoBehaviour
{
    public float Lives = 100;
    public InjuryLevelController LevelController;


    private void OnParticleCollision(GameObject other)
    {
        Lives -= 5;
        if(Lives <=0)
        {
            LevelController.IsWaterDropped = true;
        }
    }

    public void Bandage()
    {
        LevelController.IsBandageApplied = true;
        Debug.Log("!");
    }

}
