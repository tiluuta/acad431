using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTime : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    void Update(){
        TimeDisplay();
    }

    public void TimeDisplay(){
        int minutes = (int) RecordTime.elapsedTime;
        int hours = minutes / 60;
        timeText.text = string.Format("{0:00}:{1:00}", hours, minutes);
    }
}
