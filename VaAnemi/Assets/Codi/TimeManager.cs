using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    private float temps;
    private bool nit, dia;

    // Start is called before the first frame update
    void Start()
    {
        temps = 0;
        nit = false;
        dia = false;
    }

    // Update is called once per frame
    void Update()
    {
        temps = temps + Time.deltaTime;
        if(temps > 6) temps = 0;
        //Debug.Log(temps);


        if (temps <= 3 && !dia) //es de dia
        {
            dia = true; nit = false;
            esDia();
        }
        else if (temps > 3 && !nit) //es de nit
        {
            dia = false; nit = true;
            esNit();
        }
    }

    private void esDia()
    {
        EventsJoc.acutal.isDia(); //iniciem l'event  Dia
    }
    private void esNit()
    {
        EventsJoc.acutal.isNit(); //iniciem l'event Nit
    }
}
