using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class SlidersController : MonoBehaviour
{
    public Slider Slider_for_hits;
    public Slider Human_Condition_Slider;
    public float minValue_for_success_hit;
    public float maxValue_for_success_hit;
    private int direction = 1;
    public float speed;

    private void Update()
    {
        if(Slider_for_hits.value == 0)
        {
            direction = 1;
        }
        else if (Slider_for_hits.value == 1)
        {
            direction = -1;
        }
        Slider_for_hits.value += speed * direction;
        Human_Condition_Slider.value -= 0.01f;
    }


    public void PlusCondition()
    {
        Human_Condition_Slider.value += 0.1f;
    }

    public void CheckHit(float value)
    {
        if (minValue_for_success_hit < value && value < maxValue_for_success_hit)
        {
            PlusCondition();
        }
    }
}
