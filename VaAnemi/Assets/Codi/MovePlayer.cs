using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public CharacterController controlador;
    public Animator animator;
    public Camera cam;
    public Transform groundCheck;
    public LayerMask groundMask;

    public float speed, acceleracio, desacceleracio;
    public float girSmoothTemps;
    
    float radiGroundDist = 0.5f;
    float girSmoothVelocitat;
    Vector3 velocityY;

    private void Start()
    {
        girSmoothTemps = 0.1f;

        speed = 0.0f;
        acceleracio = 10f;
        desacceleracio = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = Physics.CheckSphere(groundCheck.position, radiGroundDist, groundMask);
        bool playerSayRUN = Input.GetKey("left shift"); // key que indica que vol corre
       
        float horitzonalInput = Input.GetAxisRaw("Horizontal"); // si presiona "a" i "d" a la hora es pare, lo seu seria que agafes el ultim "input"
        float verticalInput = Input.GetAxisRaw("Vertical"); // si presiona "w" i "s" a la hora es pare, lo seu seria que agafes el ultim "input"
        Vector3 direccio = new Vector3(horitzonalInput, 0f, verticalInput).normalized; //aixi quan vagi en diagonal no s'accelera


        if (direccio.magnitude >= 0.1f) // ens indica que vol moures
        {
            if (playerSayRUN && speed != Tags.VELCORRE_HUMA) //ens indica que vol corre
            {
                if (speed < Tags.VELCORRE_HUMA)//accelerem per corre
                {
                    speed += (Time.deltaTime * acceleracio);
                    if (speed > Tags.VELCORRE_HUMA) speed = Tags.VELCORRE_HUMA; //no ens pasem de la velocitat objectiu (per si de cas)
                }
            }
            else if(speed != Tags.VELCAMINAR_HUMA)//ens indica que vol caminar
            {
                if (speed < Tags.VELCAMINAR_HUMA)//accelerem per caminar
                {
                    speed += (Time.deltaTime * acceleracio);
                    if (speed > Tags.VELCAMINAR_HUMA) speed = Tags.VELCAMINAR_HUMA; //no ens pasem de la velocitat objectiu (per si de cas)
                }
                else if (speed > Tags.VELCAMINAR_HUMA) //frenem per caminar
                {
                    speed -= (Time.deltaTime * desacceleracio);
                    if (speed < Tags.VELCAMINAR_HUMA) speed = Tags.VELCAMINAR_HUMA; //no ens pasem de la velocitat objectiu (per si de cas)
                }
            }
        }
        else //ens indica que no vol moures
        {
            if (speed > 0.0f) //frenem per pararnos
            {
                speed -= (Time.deltaTime * desacceleracio);
            }

            if (speed < 0.0f) speed = 0.0f; //no ens pasem de la velocitat objectiu (per si de cas)
        }

        girSmoothVelocitat = movimentPayer(controlador, cam, speed, girSmoothTemps, girSmoothVelocitat, direccio);
        velocityY = gravetatPlayer(velocityY, Tags.GRAVTERRA, controlador.height, isGrounded);

        animator.SetFloat(Tags.VELOCITAT_ANIM, speed);//animen un cop fet tot moviment
    }

    float movimentPayer(CharacterController c, Camera _camRef, float _speed, float _girSmoothT, float _girSmoothV, Vector3 _dir)
    {
        if (_dir.magnitude >= 0.1f) //ens movem
        {
            //es multiplicar per canviar el tipus, i li sumem el angle de la camera aixi es mou en relacio a on mire la cemra
            float angleDesti = Mathf.Atan2(_dir.x, _dir.z) * Mathf.Rad2Deg + _camRef.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angleDesti, ref _girSmoothV, _girSmoothT);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //es moltiplica aixi podem tenri el formar que necestiem. 
            Vector3 movDir = Quaternion.Euler(0f, angleDesti, 0f) * Vector3.forward;
            c.Move(movDir.normalized * _speed * Time.deltaTime);

        }
       
        return _girSmoothV;
    }

    Vector3 gravetatPlayer(Vector3 _velocityY, float _gravity, float _jumpHeight, bool _isGrounded)
    {
        //amb aixo savem si esta tocan amb els elements de la "groundMask" espesificats

        if (_isGrounded && _velocityY.y < 0)
            _velocityY.y = -2f; // no fiquem 0 ja que estem afegin un "marge d'error" i aixi l'evitem

        if (Input.GetButtonDown("Jump") && _isGrounded)
            _velocityY.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity); // es una formula matematica/fisica per aplicar el salt

        Debug.Log("condicio de reset? " + (_isGrounded && _velocityY.y < 0));

        _velocityY.y += _gravity * Time.deltaTime; //es aixi per formula matematica/fisica
        controlador.Move(_velocityY * Time.deltaTime); //es aixi per formula matematica/fisica
        return _velocityY;
    }
}
