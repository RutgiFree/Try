using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class IA1 : MonoBehaviour
{
    public NavMeshAgent NavMeshAgent;
    
    private Queue<string> KeysObj = new Queue<string>();
    private bool finishObj = true;
    private Dictionary<string, GameObject> MapaObj = new Dictionary<string, GameObject>();
    //llença "ArgumentException" si ja existeix la KEY, evitem repeticions; 
    //llença "KeyNotFoundException" si no existeix la KEY buscada; 
    // https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?redirectedfrom=MSDN&view=net-5.0

    void Start()
    {

    }

    void Update()
    {

       

        if (KeysObj.Count > 0 && finishObj) { //sempre que tinguem objectius i no estiuem a un desti, anem al seguent destí
            GameObject _ActualObj;

            if (MapaObj.TryGetValue(KeysObj.Peek(), out _ActualObj)){
                NavMeshAgent.destination = _ActualObj.transform.position; //fiquem posicio del objectiu
                finishObj = false; //hem inicat l'objectiu, clarament no l'hem acavat
            }           
            else Debug.Log("ERROR-KEY NO TROBADA: " + this.name + " en Update \n");
        }
        else{
            //mirarem msi podem anar al seguent objectiu depent del criteri que volguem observar (velocitat, accio en realització)
        }
    }

    public void setMemoryObj(string _str, GameObject _GO)       
    { 
        Debug.Log("HEY");
        try{
            MapaObj.Add(_str, _GO);
            KeysObj.Enqueue(_str);
        }
        catch (ArgumentException){
            Debug.Log("ERROR-KEY REPETIDA: " + this.name + " en setMemoryObj \n");
        }
    }
}
