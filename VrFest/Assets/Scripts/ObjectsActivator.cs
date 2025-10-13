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
    public Animator animator;
    public string anim;
    private int activatedCount = 0;

    private void Awake()
    {
        instance = this;
        sound.Pause();

        if (animator != null)
        {
            animator.enabled = false;
        }
    }

    public void Activate(int index)
    {
        if (galochki[index].GetComponent<Image>().sprite != Image)
        {
            galochki[index].GetComponent<Image>().sprite = Image;
            sound.PlayOneShot(clip);

            activatedCount++;

            if (activatedCount >= 3)
            {
                PlayAnimation();
            }
        }
    }

    public void PlayPods(AudioSource audio)
    {
        audio.Play();
    }

    private void PlayAnimation()
    {
        if (animator != null)
        {

            animator.enabled = true;

            animator.Rebind();
            animator.Update(0f);

            animator.Play(anim);
        }
    }
}