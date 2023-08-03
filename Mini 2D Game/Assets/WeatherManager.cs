using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeatherState
{
    Clear,
    Rain,
    HeavyRain,
    RainAndThunder
}

public class WeatherManager : TimeAgent
{
    [Range(0f, 1f)] [SerializeField] float chanceToChangeWeather = 0.02f;
    [SerializeField] ParticleSystem rainPar;
    [SerializeField] ParticleSystem heavyrainPar;
    [SerializeField] ParticleSystem rainAndThunderPar;

    WeatherState currentWeatherState = WeatherState.Clear;

    private void Start()
    {
        Init();
        onTimeTick += RandomWeatherChangeCheck;
        UpdateWeather();
    }

    public void RandomWeatherChangeCheck()
    {
        if (UnityEngine.Random.value < chanceToChangeWeather)
        {
            RandomWeatherChange();
        }
    }

    private void RandomWeatherChange()
    {
        WeatherState newWS = (WeatherState) UnityEngine.Random.Range(0, Enum.GetNames(typeof(WeatherState)).Length);
        ChangeWeather(newWS);
    }

    private void ChangeWeather(WeatherState newWS)
    {
        currentWeatherState = newWS;
        Debug.Log(currentWeatherState);

        UpdateWeather();
    }

    private void UpdateWeather()
    {
        switch (currentWeatherState)
        {
            case WeatherState.Clear:
                rainPar.gameObject.SetActive(false);
                heavyrainPar.gameObject.SetActive(false);
                rainAndThunderPar.gameObject.SetActive(false);
                break;
            case WeatherState.Rain:
                rainPar.gameObject.SetActive(true);
                heavyrainPar.gameObject.SetActive(false);
                rainAndThunderPar.gameObject.SetActive(false);
                break;
            case WeatherState.HeavyRain:
                rainPar.gameObject.SetActive(false);
                heavyrainPar.gameObject.SetActive(true);
                rainAndThunderPar.gameObject.SetActive(false);
                break;
            case WeatherState.RainAndThunder:
                rainPar.gameObject.SetActive(false);
                heavyrainPar.gameObject.SetActive(false);
                rainAndThunderPar.gameObject.SetActive(true);
                break;
        }
    }
}
