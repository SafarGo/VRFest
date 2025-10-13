using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectsActivator : MonoBehaviour
{
    public List<GameObject> galochki = new List<GameObject>();
    public Sprite Image;
    public static ObjectsActivator instance;
    public AudioSource sound;
    public AudioClip clip;
    

    private void Awake()
    {
        instance = this;
        sound.Pause();
    }

    public void Activate(int index)
    {
        if (galochki[index].GetComponent<Image>().sprite != Image)
        {
            galochki[index].GetComponent<Image>().sprite = Image;
            sound.PlayOneShot(clip);
        }
    }

    public void PlayPods(AudioSource audio)
    {
        audio.Play();
    }

    
}