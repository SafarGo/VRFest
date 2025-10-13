using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdLevelController : MonoBehaviour, ILevelsController
{
    public static ThirdLevelController instance;
    public bool isHandsTogether = false;
    public bool isStartedProcess = false;
    public bool isEndedPocess = false;

    private void Awake()
    {
        instance = this;
    }

    public void HandsSet()
    {
        isHandsTogether = true;
        ObjectsActivator.instance.Activate(0);
    }

    public void StartSet()
    {
        isStartedProcess = true;
        ObjectsActivator.instance.Activate(1);
    }

    public void EndSet()
    {
        isEndedPocess = true;
        ObjectsActivator.instance.Activate(2);
        AnimController.instance.PlayAnimation();
    }

    public bool CheckProgress()
    {
        return  (isHandsTogether && isStartedProcess && isEndedPocess) ? true : false;
    }
}
