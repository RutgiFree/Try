using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlitPetit1 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.NPC))
        {
            Debug.Log(other.name + "\t PLAY ANIMATION zZzZ");
        }
    }
}
