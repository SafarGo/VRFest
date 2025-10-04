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
    public bool isLevelEndedSuccesfull = false;

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
        if(giventablets ==2)
        {
            IsTabletsGave = true;
            ObjectsActivator.instance.Activate(2);
            
        }
        if(IsWaterDropped && GameObject.Find("cup") != null)
        {
            GameObject.Find("cup").SetActive(false);
        }
        if(IsTabletsGave && IsWaterDropped && IsBandageApplied)
        {
            CheckProgress();
        }
    }

    public void CheckProgress()
    {
        isLevelEndedSuccesfull = (IsTabletsGave && IsWaterDropped && IsBandageApplied) ? true : false;
        Timer.instance.Success(isLevelEndedSuccesfull);
    }
}
