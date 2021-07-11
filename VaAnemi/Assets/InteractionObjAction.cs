using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObjAction : MonoBehaviour
{
    public Shader lookingShader;
    bool isActive, isAccion;

    Outline outlineShader;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        isAccion = false;

        //configurem la "bora" de l'objecte per quan el seleccionem
        outlineShader = gameObject.AddComponent<Outline>();
        outlineShader.OutlineColor = Color.white;
        outlineShader.OutlineMode = Outline.Mode.OutlineVisible;
        outlineShader.OutlineWidth = 0f; //de mentres l'amaguem
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void interactua()
    {
        isAccion = !isAccion;
        if (isAccion)
        {
            //ACTIVAT
        }
        else
        {
            //DESACTIVAT
        }
    }

    public void SiSocActiu()
    {
        isActive = true;
        outlineShader.OutlineWidth = 5f; //el fem visible
    }

    public void NoSocActiu()
    {
        isActive = false;
        outlineShader.OutlineWidth = 0f;//de mentres l'amaguem
    }
}