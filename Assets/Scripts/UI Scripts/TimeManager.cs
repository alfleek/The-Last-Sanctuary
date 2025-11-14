using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Time ration 15 real time minutes to 12 hours in game 
    // 15 real minutes == 900 seconds 
    // 12 in game hours == 720 minutes

    public float realMinutesPerDay = 15f;

    private float gameMinutes;
    private int dayCount = 1;
    private float timeScale; 

    // Start is called before the first frame update
    void Start()
    {
        timeScale = 720f/ (realMinutesPerDay*60f);
        gameMinutes = 8 * 60f; 
    }

    // Update is called once per frame
    void Update()
    {
        updateGameTime();

    }

    void updateGameTime() {
        gameMinutes += Time.deltaTime* timeScale;

        if(gameMinutes >= 1440f)
        {
            gameMinutes -= 1440f;
            dayCount++; 
        }
    }

    public int GetHour()
    {
        return Mathf.FloorToInt(gameMinutes / 60f); 
    }

    public int GetMinutes()
    {
        return Mathf.FloorToInt(gameMinutes % 60f); 
    }

    public string GetAMPM(int hour)
    {
        return hour >= 12 ? "PM" : "AM";
    }

    public int GetDays()
    {
        return dayCount;
    }
}
