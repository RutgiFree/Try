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
    
    public float speed = Tags.VELCAMINARHUMA;
    public float girSmoothTemps = 0.1f;
    
    float radiGroundDist = 0.5f;
    float girSmoothVelocitat;
    Vector3 velocityY;

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = Physics.CheckSphere(groundCheck.position, radiGroundDist, groundMask);

        girSmoothVelocitat = movimentPayer(controlador, cam, speed, girSmoothTemps, girSmoothVelocitat, isGrounded);
        velocityY = gravetatPlayer(velocityY, Tags.GRAVTERRA, controlador.height, isGrounded);
    
    }

    float movimentPayer(CharacterController c, Camera _camRef, float _speed, float _girSmoothT, float _girSmoothV, bool _isGrounded)
    {
        float horitzonalInput = Input.GetAxisRaw("Horizontal"); // si presiona "a" i "d" a la hora es pare, lo seu seria que agafes el ultim "input"
        float verticalInput = Input.GetAxisRaw("Vertical"); // si presiona "w" i "s" a la hora es pare, lo seu seria que agafes el ultim "input"
        Vector3 direccio = new Vector3(horitzonalInput, 0f, verticalInput).normalized; //aixi quan vagi en diagonal no s'accelera

        if (Input.GetKey("left shift"))
        {
           _speed = Tags.VELCORREHUMA;
        }
        else
        {
           _speed = Tags.VELCAMINARHUMA;
        }

        if (direccio.magnitude >= 0.1f)
        {
            //es multiplicar per canviar el tipus, i li sumem el angle de la camera aixi es mou en relacio a on mire la cemra
            float angleDesti = Mathf.Atan2(direccio.x, direccio.z) * Mathf.Rad2Deg + _camRef.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angleDesti, ref _girSmoothV, _girSmoothT);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //es moltiplica aixi podem tenri el formar que necestiem. 
            Vector3 movDir = Quaternion.Euler(0f, angleDesti, 0f) * Vector3.forward;
            c.Move(movDir.normalized * _speed * Time.deltaTime);

            bool caminant = animator.GetBool(Tags.ISCAMINANT_ANIMATOR); //fent aixo guanyem eficiencia, aixi no el canviem si ja esta com volem
            bool corrent = animator.GetBool(Tags.ISCORRENT_ANIMATOR); //fent aixo guanyem eficiencia, aixi no el canviem si ja esta com volem


            if (_speed == Tags.VELCAMINARHUMA && !caminant) //caminem
            {
                animator.SetBool(Tags.ISCAMINANT_ANIMATOR, true);
            }

            if (_speed == Tags.VELCORREHUMA && !corrent) //correm
            {
                animator.SetBool(Tags.ISCORRENT_ANIMATOR, true);

            }
            else if(_speed != Tags.VELCORREHUMA) //no correm
            {
                animator.SetBool(Tags.ISCORRENT_ANIMATOR, false);
            }
            
          
        }
        else //no ens movem (no es conte el "saltar") 
        {
            bool caminant = animator.GetBool(Tags.ISCAMINANT_ANIMATOR); //fent aixo guanyem eficiencia, aixi no el canviem si ja esta com volem
            bool corrent = animator.GetBool(Tags.ISCORRENT_ANIMATOR); //fent aixo guanyem eficiencia, aixi no el canviem si ja esta com volem

            if (caminant)
                animator.SetBool(Tags.ISCAMINANT_ANIMATOR, false);
            if (corrent)
                animator.SetBool(Tags.ISCORRENT_ANIMATOR, false);
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
