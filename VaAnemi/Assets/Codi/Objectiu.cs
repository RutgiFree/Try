using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectiu : MonoBehaviour
{
    private Queue<GameObject> MyGO = new Queue<GameObject>();

    private void OnTriggerStay(Collider other)
    {
        if (!MyGO.Contains(other.gameObject) && other.gameObject.name.Contains("Usable-"))
        {
            MyGO.Enqueue(other.gameObject);
        }
    }

    public Queue<GameObject> getMyGoL()
    {
        return MyGO;
    }
}