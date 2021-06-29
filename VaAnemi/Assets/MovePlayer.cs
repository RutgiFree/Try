using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public CharacterController controlador;
    public Camera cam;

    public float speed = 6f;
    public float girSmoothTemps = 0.1f;
    float girSmoothVelocitat;

    // Update is called once per frame
    void Update()
    {
        float horitzonalInput = Input.GetAxisRaw("Horizontal");
        float vertivalInput = Input.GetAxisRaw("Vertical");
        Vector3 direccio = new Vector3(horitzonalInput, 0f, vertivalInput).normalized; //aixi quan vagi en diagonal no s'accelera

        if (direccio.magnitude >= 0.1f)
        {
            //es multiplicar per canviar el tipus, i li sumem el angle de la camera aixi es mou en relacio a on mire la cemra
            float angleDesti = Mathf.Atan2(direccio.x, direccio.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angleDesti, ref girSmoothVelocitat, girSmoothTemps);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //es moltiplica aixi podem tenri el formar que necestiem. 
            Vector3 movDir = Quaternion.Euler(0f, angleDesti, 0f) * Vector3.forward;
            controlador.Move(movDir.normalized * speed * Time.deltaTime);
        }
    }
}
