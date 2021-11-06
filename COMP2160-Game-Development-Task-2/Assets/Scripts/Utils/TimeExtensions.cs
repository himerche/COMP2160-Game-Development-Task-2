using System.Collections;
using UnityEngine;

public static class TimeExtensions
{
    /* 
     * Return time in the format “minutes:seconds:hundredths"
     */
    public static string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time - minutes;
        float hundredths = (time - minutes - seconds) * 100;

        return string.Format("{0:0}:{1:00}:{2:00}", minutes, seconds, hundredths);
    }
}
