using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class IA1 : MonoBehaviour
{
    public NavMeshAgent NavMeshAgent;
    public GameObject origin;
    
    private Queue<string> KeysObj = new Queue<string>();

    private bool finishedObj = true;
    private Dictionary<string, GameObject> MapaObj = new Dictionary<string, GameObject>();
    //llença "ArgumentException" si ja existeix la KEY, evitem repeticions; 
    //llença "KeyNotFoundException" si no existeix la KEY buscada; 
    // https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?redirectedfrom=MSDN&view=net-5.0

    void Start(){
        EventsJoc.acutal.onNit += isNit;//ens subcrivim al event, quan s'executi s'executara el nostre metode provat
        EventsJoc.acutal.onDia += isDia;//ens subcrivim al event, quan s'executi s'executara el nostre metode provat
        
        setMemoryObj(
            Tags.ORIGEN, 
            Instantiate<GameObject>(origin, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation)
            );
    }

    void Update()
    {
        AnemAlObj();
    }

    public void setMemoryObj(string _str, GameObject _GO)       
    { 
        try
        {
            MapaObj.Add(_str, _GO);//Guardem el nom del objecte i el objecte en si (el no es per facilitar la serca del objecte) 
        }
        catch (ArgumentException){
            Debug.Log("ERROR-KEY REPETIDA: " + this.name + " en setMemoryObj \n");
        }
    }

    private void setAcualObj(string _Key)
    { 
        KeysObj.Enqueue(_Key); //Afegim objectiu a seguir usan la KEY
    }

    private void AnemAlObj()
    {
        if (KeysObj.Count > 0 && finishedObj)//sempre que tinguem objectius i no estiuem a un desti, anem al seguent destí
        { 
            GameObject _ActualObj;

            if (MapaObj.TryGetValue(KeysObj.Peek(), out _ActualObj))
            {
                NavMeshAgent.destination = _ActualObj.transform.position; //fiquem posicio del objectiu
                //finishedObj = false; //hem inicat l'objectiu, clarament no l'hem acavat
                KeysObj.Dequeue();//aixi no perseguira l'objectiu eternament si el movem
            }
            else Debug.Log("ERROR-KEY NO TROBADA:"+ KeysObj.Peek() + " en l'objecte " + this.name + " en Update \n");
        }
    }

    private void isNit() // es de nit
    {
        if (MapaObj.ContainsKey(Tags.LLIT1)) //tenim el llit tipus 1 en la llista dememoria? okey doncs es de nit aixi que...
        {
            setAcualObj(Tags.LLIT1); // a mimir
        }
       
    }

    private void isDia() // es de nit
    {
        setAcualObj(Tags.ORIGEN); // anem al origen
    }
}
