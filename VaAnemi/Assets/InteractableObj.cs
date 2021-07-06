using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObj : MonoBehaviour
{
    public bool isInRange, isActive;
    public KeyCode keyInteract;
    public UnityEvent interationAction, NoLookingAction, SiLookingAction;

    // Start is called before the first frame update
    void Start()
    {
        isInRange = false;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && isActive)// si estem en rang entrentrem
        {
            if (Input.GetKeyDown(keyInteract)) //si la tecla que estem mantenint es la tecla interraci definida, entrem
            {
                interationAction.Invoke();  //invoquem la interacio definida
                isActive = false;
                NoLookingAction.Invoke();
            }
        }
    }

    public void setActive()
    {
        isActive = true;
        SiLookingAction.Invoke();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            this.isInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            this.isInRange = false;
            this.isActive = false;
            NoLookingAction.Invoke();
        }
    }
}
