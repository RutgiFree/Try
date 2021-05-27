using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsJoc : MonoBehaviour
{
    //link -> https://www.youtube.com/watch?v=gx0Lt4tCDE0
    public static EventsJoc acutal;
    void Awake()
    {
        acutal = this;
    }

    public event Action onNit; //on es guarden les subscripcions al eveny
    public void isNit()
    {
        Debug.Log("isNIT");
        if (onNit != null)
            onNit();//execució del event
    }

    public event Action onDia; //on es guarden les subscripcions al eveny
    public void isDia()
    {
        Debug.Log("isDIA");
        if (onDia != null)
            onDia();//execució del event
    }
}
