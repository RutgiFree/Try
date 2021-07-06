using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObjAction : MonoBehaviour
{
    public Material NO_active, YES_active, actualM, LOOKING_active;
    bool isActive, isAccion;

    Renderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        myRenderer.material = NO_active;
        actualM = NO_active;
        isActive = false;
        isAccion = false;
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
            myRenderer.material = YES_active;
        }
        else
        {
            myRenderer.material = NO_active;
        }
        actualM = myRenderer.material;
    }

    public void SiSocActiu()
    {
        isActive = true;
        myRenderer.material = LOOKING_active;
    }

    public void NoSocActiu()
    {
        isActive = false;
        myRenderer.material = actualM;        
    }
}