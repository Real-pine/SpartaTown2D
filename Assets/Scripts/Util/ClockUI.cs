using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClockUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI clockToText;

    private void Start()
    {
        if(clockToText == null)
        {
            clockToText = GetComponent<TextMeshProUGUI>();
        }
        if(clockToText == null )
        {
            Debug.LogError("ClockText component is not assigned");
        }
    }

    private void Update()
    {
        if(clockToText != null)
        {
            UpdateClockToText();
        }
    }

    private void UpdateClockToText()
    {
        DateTime currentTime = DateTime.Now;
        string timeString = currentTime.ToString("HH:mm");
        clockToText.text = timeString;
    }
}
