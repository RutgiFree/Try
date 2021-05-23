using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPredefinits : MonoBehaviour
{
    private Queue<string> KeysObj = new Queue<string>();
    private IA1 ia;
    void Awake()
    {
        ia = GetComponent<IA1>();
    }

    private void Start()
    {
       for (int i = 0; i < transform.childCount; i++){
            var child = transform.GetChild(i).GetComponent<Objectiu>();
            
            if (child != null && child.getMyGo() != null)
            {
                ia.setMemoryObj(child.getMyGo().name, child.getMyGo());
            }
            else
            {
                Debug.LogError("ERROR - NULLS PER EL MITX");
            }

            Destroy(transform.GetChild(i).gameObject);//destriu el objecte no desitjat
        }
    }
}
