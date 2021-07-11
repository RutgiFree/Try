using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionAccion : MonoBehaviour
{

    List<GameObject> interact_llista;
    GameObject interact_Actiu;
    int indexActual;

    // Start is called before the first frame update
    void Start()
    {
        interact_llista = new List<GameObject>();
        indexActual = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(interact_Actiu != null)
        {
            if (!interact_Actiu.GetComponent<InteractableObj>().isActive)
            {
                indexActual = getNextActiveIndex(indexActual); //actualitzem l'index de la llista
                interact_Actiu = interact_llista[indexActual]; //agafem un element de la llista  
                interact_Actiu.GetComponent<InteractableObj>().setActive();
            }
        }
        else if(interact_llista.Count > 0)
        {
            indexActual = getNextActiveIndex(indexActual); //actualitzem l'index de la llista
            interact_Actiu = interact_llista[ indexActual ]; //agafem un element de la llista 
        }
        Debug.Log(interact_llista.Count);
    }

    private int getNextActiveIndex(int _actualIndex)
    {
        if ( _actualIndex >= (interact_llista.Count - 1) )
            return 0;
        
        return _actualIndex + 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.INTERACT))
        {
            if (interact_Actiu == null)
            {
                interact_Actiu = other.gameObject;
                interact_Actiu.GetComponent<InteractableObj>().setActive();
            }
            
            if(!interact_llista.Contains(other.gameObject)) //penso que avans es colaven duplicas aixi que per sid e cas
            {
               interact_llista.Add(other.gameObject); //safegeix al final de la llista
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.INTERACT))
        {
            if (interact_Actiu == other.gameObject)
                interact_Actiu = null;

            interact_llista.Remove(other.gameObject);
        }
    }
}
