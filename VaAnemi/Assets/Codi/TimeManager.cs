using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private bool nit, dia;

    private int horaJ;
    private float minutJ;

    // Start is called before the first frame update
    void Start()
    {
        nit = false;
        dia = false;

        //1 hora de joc == 1 minut mon real -> 24horesJ == 24minutsR, 60minutsJ = 60segonsR;
        horaJ = 7;
        minutJ = 0;
    }

    // Update is called once per frame
    void Update()
    {
        minutJ = minutJ + Time.deltaTime;

        if (minutJ > 60)
        {
            minutJ = 0;
            horaJ++;
        }

        if (horaJ > 24)
        {
            horaJ = 0;
            //Debug.Log("HORA: " + horaJ + ":" + Mathf.FloorToInt(minutJ) + "\tBY: TimeManager");
        }

        if ( (horaJ >= Tags.SOLETHSURT && horaJ < Tags.SOLETHMARXE) && !dia) //es de dia [si ja ha sortit el sol i no ho sabiem que ja ho era]
        {
            dia = true; nit = false;
            esDia();
            //Debug.Log("ES DE DIA: HORA: " + horaJ + ":" + Mathf.FloorToInt(minutJ) + "\tBY: TimeManager");
        }
        else if ( (horaJ < Tags.SOLETHSURT || horaJ >= Tags.SOLETHMARXE) && !nit) //es de nit [si ja ha marxat el sol i no ho sabiem que ja ho era]
        {
            dia = false; nit = true;
            esNit();
            //Debug.Log("ES DE NIT: HORA: " + horaJ + ":" + Mathf.FloorToInt(minutJ) + "\tBY: TimeManager");
        }
    }

    private void esDia()
    { //diu es es NULLPointExepction???????????????????????????????????????????
        EventsJoc.acutal.isDia(); //iniciem l'event  Dia
    }
    private void esNit()
    { //diu es es NULLPointExepction???????????????????????????????????????????
        EventsJoc.acutal.isNit(); //iniciem l'event Nit
    }
}
