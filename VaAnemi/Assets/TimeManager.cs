using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    private float temps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(temps);
        temps = temps + Time.deltaTime;
        if(temps > 24) temps = 0;

        if (temps > 1) EventsJoc.acutal.isNit(); //iniciem l'event Nit
        else EventsJoc.acutal.isDia(); //iniciem l'event  Dia
    }
}
