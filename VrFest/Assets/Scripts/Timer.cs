using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float Timer_value;
    private TMP_Text timer_text;
    public static Timer instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timer_text = gameObject.GetComponent<TMP_Text>();
        UpdateTimerDisplay();
    }

    private void Update()
    {
        if (Timer_value > 0)
        {
            Timer_value -= Time.deltaTime;
            UpdateTimerDisplay();
            InjuryLevelController.instance.CheckProgress();
        }
        else
        {
            Timer_value = 0;
            UpdateTimerDisplay();
           
        }
    }

    private void UpdateTimerDisplay()
    {
        int minutes = (int)Timer_value / 60;
        int seconds = (int)Timer_value % 60;
        timer_text.text = $"{minutes}:{seconds:D2}";
    }

    public void Success(bool state)
    {
        timer_text.text = (state) ? "Success" : "Loose";
    }
}
