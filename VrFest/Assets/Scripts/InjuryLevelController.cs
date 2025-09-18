using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuryLevelController : MonoBehaviour
{
    public static InjuryLevelController instance;
    public bool IsWaterDropped = false;
    public bool IsBandageApplied = false;
    public bool IsTabletsGave = false;
    public bool IsCrane = false;
    public int giventablets = 0;

    private void Awake()
    {
        instance = this;
    }

    public void CraneButton()
    {
        IsCrane = true;
    }
    public void UnCraneButton()
    {
        IsCrane = false;
    }

    private void Update()
    {
        if(IsTabletsGave && IsWaterDropped && IsBandageApplied)
        {
            Debug.Log("Конец");
        }
    }
}
