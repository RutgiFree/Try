using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class IA1 : MonoBehaviour
{
    public NavMeshAgent NavMeshAgent;
    public GameObject joPeroNOjo;
    public int multiplicadorRandom;
    public int gana, set, son, vida;
    
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
            Instantiate<GameObject>(joPeroNOjo, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation)
            );
    }

    void Update()
    {


        if (KeysObj.Count > 0 && finishedObj)//sempre que tinguem objectius i no estiuem a un desti, anem al seguent destí
        {
            finishedObj = AnemAlObj(KeysObj.Dequeue()); //si retorna "false" vol dir que tot ha anat bé i que tenim objectiu al que ens dirigim
        }
        else if (!finishedObj) //aixo es per evitar que entri cada cop aixi no agafem tota la condicio llarga i costos de abaix tot el rato
        {
            if (new Vector2(NavMeshAgent.destination.x, NavMeshAgent.destination.z).Equals(new Vector2(this.transform.position.x, this.transform.position.z)))
            {
                finishedObj = true;
                Debug.Log("HA ARRIBAT al destí -> " + this.name);
            }
        }

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
    public void setDormit(int dormit)
    {
        son += dormit;
        if (son > Tags.MAXSON)
        {
            son = Tags.MAXSON;
        }
    }

    private void setAcualObj(string _Key)
    { 
        KeysObj.Enqueue(_Key); //Afegim objectiu a seguir usan la KEY
    }
    private int getRand(int multRand, int min, int max)
    {
        Random.InitState(Time.frameCount * multRand);
        return Random.Range(min, max);
    }

    private bool AnemAlObj(string _key)
    {
        GameObject _ActualObj; Boolean retorn = true;

        if (MapaObj.TryGetValue(_key, out _ActualObj))
        {
            NavMeshAgent.destination = _ActualObj.transform.position; //fiquem posicio del objectiu
            retorn =  false;//hem inicat l'objectiu, clarament no l'hem acavat
        }
        else Debug.Log("ERROR-KEY NO TROBADA:" + _key + " en l'objecte " + this.name + " en Update \n");

        return retorn;
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
        //setAcualObj(Tags.ORIGEN); // anem al origen
    }   
}
