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
    public static SlidersController instance;


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        //if (ThirdLevelController.instance.isStartedProcess)
        //{
            MoveSlider();
            Slider_for_hits.value += speed * direction;
            Human_Condition_Slider.value -= 0.001f;
            if (Human_Condition_Slider.value >0.95)
            {
                StopGame();
            }
        //}
    }


    public void PlusCondition()
    {
        Human_Condition_Slider.value += 0.2f;
    }

    public void CheckHit(float value)
    {
        if (minValue_for_success_hit < value && value < maxValue_for_success_hit)
        {
            PlusCondition();
        }
    }

    void StopGame()
    {
        speed = 0;
        Human_Condition_Slider.value = 1;
        ThirdLevelController.instance.EndSet();
        Debug.Log("!!");
    }

    void MoveSlider()
    {
        if (Slider_for_hits.value == 0)
        {
            direction = 1;
        }
        else if (Slider_for_hits.value == 1)
        {
            direction = -1;
        }
    }

    public void StartGame()
    {
        ThirdLevelController.instance.StartSet();
    }
}
