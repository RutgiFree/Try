using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravetatPlayer : MonoBehaviour
{
    public CharacterController controlador;
    public Transform groundCheck;
    public LayerMask groundMask;

    Vector3 velocityY;

    bool isGrounded;
    float gravity = Tags.GRAVTERRA, jumpHeight, groundDist = 0.5f; //el groundDist es el radi de la esfera imaginaria que es crearà

    private void Start()
    {
        jumpHeight = controlador.height; // amb aixo definim que pot saltar la alçada del seu "controlador" / personatge
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

        if (isGrounded && velocityY.y < 0)
            velocityY.y = -2f; // no fiquem 0 ja que estem afegin un "marge d'error" i aixi l'evitem

        if( Input.GetButtonDown("Jump") && isGrounded)
            velocityY.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // es una formula matematica/fisica per aplicar el salt
        
        velocityY.y += gravity * Time.deltaTime; //es aixi per formula matematica/fisica
        controlador.Move(velocityY * Time.deltaTime); //es aixi per formula matematica/fisica
    }
}
