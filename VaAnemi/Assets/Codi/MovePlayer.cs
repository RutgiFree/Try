using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public CharacterController controlador;
    public Camera cam;
    public Transform groundCheck;
    public LayerMask groundMask;

    public float speed = 6f;
    public float girSmoothTemps = 0.1f;
    float girSmoothVelocitat;
    Vector3 velocityY;

    // Update is called once per frame
    void Update()
    {
        girSmoothVelocitat = movimentPayer(controlador, cam, speed, girSmoothTemps, girSmoothVelocitat);
        velocityY = gravetatPlayer(velocityY, groundCheck, groundMask, Tags.GRAVTERRA, controlador.height, 0.5f);
    }

    float movimentPayer(CharacterController c, Camera _camRef, float _speed, float _girSmoothT, float _girSmoothV)
    {
        float horitzonalInput = Input.GetAxisRaw("Horizontal");
        float vertivalInput = Input.GetAxisRaw("Vertical");
        Vector3 direccio = new Vector3(horitzonalInput, 0f, vertivalInput).normalized; //aixi quan vagi en diagonal no s'accelera

        if (direccio.magnitude >= 0.1f)
        {
            //es multiplicar per canviar el tipus, i li sumem el angle de la camera aixi es mou en relacio a on mire la cemra
            float angleDesti = Mathf.Atan2(direccio.x, direccio.z) * Mathf.Rad2Deg + _camRef.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angleDesti, ref _girSmoothV, _girSmoothT);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //es moltiplica aixi podem tenri el formar que necestiem. 
            Vector3 movDir = Quaternion.Euler(0f, angleDesti, 0f) * Vector3.forward;
            c.Move(movDir.normalized * _speed * Time.deltaTime);
            
        }
        return _girSmoothV;
    }
    Vector3 gravetatPlayer(Vector3 _velocityY, Transform _groundCheck, LayerMask _groundMask, float _gravity, float _jumpHeight, float _radiGroundDist)
    {
        //amb aixo savem si esta tocan amb els elements de la "groundMask" espesificats
        bool _isGrounded = Physics.CheckSphere(_groundCheck.position, _radiGroundDist, _groundMask); 

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
