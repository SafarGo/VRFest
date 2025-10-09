using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float Timer_value;
    private TMP_Text timer_text;
    public static Timer instance;
    private ILevelsController levelController;
    bool _end;

    private void Awake()
    {
        instance = this;
        // Find any component in scene implementing ILevelsController (interface cannot be used directly with FindObjectOfType)
        foreach (var mb in FindObjectsOfType<MonoBehaviour>())
        {
            if (mb is ILevelsController)
            {
                levelController = (ILevelsController)mb;
                break;
            }
        }
    }

    private void Start()
    {
        timer_text = gameObject.GetComponent<TMP_Text>();
        UpdateTimerDisplay();
    }

    private void Update()
    {
        if (_end)
        {
            return;
        }
        if (Timer_value > 0)
        {
            Timer_value -= Time.deltaTime;
            UpdateTimerDisplay();
            if(levelController != null && levelController.CheckProgress())
            {
                Success(true);
            }
        }
        else
        {
            Timer_value = 0;
            bool state = levelController != null && levelController.CheckProgress();
            Success(state);
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
        if(!_end)
        {
            timer_text.text = (state) ? "Success" : "Loose";
            _end = true;
            enabled = false; // stop Update from running further to avoid overwriting the result
        }
    }
}
