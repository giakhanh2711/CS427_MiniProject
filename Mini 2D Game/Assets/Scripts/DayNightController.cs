using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public enum DayOfWeek
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday
}

public enum Season
{
    Spring,
    Summer,
    Fall,
    Winter,
}

public class DayNightController : MonoBehaviour
{
    const float SECONDS_IN_DAY = 86400f;
    const float PHASE_LENGTH = 900f; // 15 minutes chunk of time
    const int seasonLengthDays = 30;

    [SerializeField] Color nightColor;
    [SerializeField] Color dayColor = Color.white;
    [SerializeField] AnimationCurve DayNightCurve;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float timeScale = 96f;
    [SerializeField] Light2D lightSetter;
    [SerializeField] float startAtTime = 25200f; // 7am
    [SerializeField] TextMeshProUGUI dayOfWeekText;
    [SerializeField] TextMeshProUGUI seasonText;

    float curTime;
    int oldPhase = 0;
    private int days = 0;
    List<TimeAgent> timeAgents;
    DayOfWeek dayOfWeek;
    Season currentSeason;


    private void Awake()
    {
        timeAgents = new List<TimeAgent>();
    }

    private void Start()
    {
        curTime = startAtTime;
        UpdateDayText();
        UpdateSeason();
    }

    public void Subscribe(TimeAgent timeAgent)
    {
        timeAgents.Add(timeAgent);
    }

    public void Unsubscribe(TimeAgent timeAgent)
    {
        timeAgents.Remove(timeAgent);
    }

    float Hours
    {
        get { return curTime / 3600; }
    }

    float Minutes
    {
        get { return curTime % 3600f / 60f; }
    }

    private void Update()
    {
        curTime += Time.deltaTime * timeScale;

        TimeCalculation();

        DayLight();

        if (curTime > SECONDS_IN_DAY)
        {
            NextDay();
        }

        TimeAgents();
    }

    private void NextDay()
    {
        curTime = 0;
        days += 1;

        int dayNum = (int)dayOfWeek;
        dayNum += 1;

        if (dayNum >= 7)
        {
            dayNum = 0;
        }

        dayOfWeek = (DayOfWeek)dayNum;
        UpdateDayText();

        if (days >= seasonLengthDays)
        {
            NextSeason();
        }
    }

    private void NextSeason()
    {
        days = 0;
        int seasonNum = (int)currentSeason;
        seasonNum += 1;

        if (seasonNum >= 4)
        {
            seasonNum = 0;
        }

        currentSeason = (Season)seasonNum;

        UpdateSeason();
    }

    private void UpdateSeason()
    {
        seasonText.text = currentSeason.ToString();
    }

    private void UpdateDayText()
    {
        dayOfWeekText.text = dayOfWeek.ToString();
    }

    private void TimeAgents()
    {
        int curPhase = (int)(curTime / PHASE_LENGTH);
        Debug.Log(curPhase);

        if (oldPhase != curPhase)
        {
            oldPhase = curPhase;
            for (int i = 0; i < timeAgents.Count; ++i)
            {
                timeAgents[i].Invoke();
            }
        }
    }

    private void DayLight()
    {
        float valueFromCurve = DayNightCurve.Evaluate(Hours); // Get value from curve with hours of curTime

        Color interpolatedColor = Color.Lerp(dayColor, nightColor, valueFromCurve); // interpolate color at time "valueFromCurve"
        lightSetter.color = interpolatedColor;
    }

    private void TimeCalculation()
    {
        text.text = ((int)Hours).ToString("00") + ":" + ((int)Minutes).ToString("00");
    }
}
