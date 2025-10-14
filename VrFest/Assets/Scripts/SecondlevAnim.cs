using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLevAnim : MonoBehaviour
{
    public Animator animator;
    public string anim;

    private void Awake()
    {
        if (animator != null)
        {
            animator.enabled = false;
        }
        else
        {
            Debug.LogError("Animator not assigned in AnimController!");
        }
    }

    public void PlayAnimation()
    {
        if (animator != null && !string.IsNullOrEmpty(anim) && Eventmanager.put_cloth == 4)
        {
            Eventmanager.instance.Blanket();
            FreezePos.isFrozen = false;
            animator.enabled = true;
            animator.Rebind();
            animator.Update(0f);

            // Проверяем существование состояния
            if (HasAnimationState(anim))
            {
                animator.Play(anim);
                Debug.Log("Playing animation: " + anim);
            }
            else
            {
                Debug.LogError("Animation state not found: " + anim);
            }
        }
        else
        {
            Debug.LogError("Animator or animation name is missing!");
        }
    }

    private bool HasAnimationState(string stateName)
    {
        if (animator == null) return false;

        // Проверяем существование состояния в Animator
        return animator.HasState(0, Animator.StringToHash(stateName));
    }
}