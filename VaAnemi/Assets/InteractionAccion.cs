using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionAccion : MonoBehaviour
{

    List<GameObject> interact_llista;
    GameObject interact_Actiu;

    // Start is called before the first frame update
    void Start()
    {
        interact_llista = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(interact_Actiu != null)
        {
            
            if (!interact_Actiu.GetComponent<InteractableObj>().isActive)
            {
                interact_llista.Add(interact_Actiu); //tornem a guardar ja que potser volem tornar a interactuar amb ell
                interact_Actiu = interact_llista[0]; //agafem un element de la llista  (el primer, ja que volem actuam com una FIFO)
                interact_llista.Remove(interact_Actiu); //i l'eliminem de la llista
                interact_Actiu.GetComponent<InteractableObj>().setActive();
            }
        }
        else if(interact_llista.Count > 0)
        {
            interact_Actiu = interact_llista[0]; //agafem un element de la llista  (el primer, ja que volem actuam com una FIFO)
            interact_llista.Remove(interact_Actiu); //i l'eliminem de la llista
        }
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
            else if(!interact_llista.Contains(other.gameObject)) //penso que avans es colaven duplicas aixi que per sid e cas
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
            else
                interact_llista.Remove(other.gameObject);
        }
    }
}
