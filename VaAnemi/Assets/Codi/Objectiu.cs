using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectiu : MonoBehaviour
{
    private GameObject MyGO = null;

    private void OnTriggerStay(Collider other)
    {
        if (MyGO == null) MyGO = other.gameObject;
    }

    public GameObject getMyGo(){
        return MyGO;
    }
}
