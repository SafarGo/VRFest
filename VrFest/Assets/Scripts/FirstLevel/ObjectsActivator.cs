using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsActivator : MonoBehaviour
{
    public List<GameObject> galochki = new List<GameObject>();
    public static ObjectsActivator instance;
    public AudioSource sound;

    private void Awake()
    {
        instance = this;
        sound.Pause();
    }

    public void Activate(int index)
    {
        galochki[index].SetActive(true);
        sound.Play();
    }
}
