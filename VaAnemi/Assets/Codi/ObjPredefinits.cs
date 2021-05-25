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

    private void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            var cComponent = child.GetComponent<Objectiu>();

            if (cComponent != null)
            {
                var cMyGoL = cComponent.getMyGoL();

                if (cMyGoL.Count > 0)
                {
                    foreach (var item in cMyGoL)
                    {
                        ia.setMemoryObj(item.name, item);
                    }

                    Destroy(child.gameObject);//destriu el objecte no desitjat
                }
            }

        }
    }
}

