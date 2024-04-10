using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput controles;
    private Vector2 Direction2;
    public Transform cam;
    public CharacterController characterController;
    [Tooltip("Gère la force de la gravité sur le joueur")]
    public float gravityScale;
    private float verticalVelocity;
    [Tooltip("Gère la force de la vitesse du joueur")]

    public float speed;
    [Tooltip("Gère le temps que met le joueuer à tourner")]
    public float turnSmoothTime;
    float turnSmoothVelocity;
    [Tooltip("Gère la force du saut du joueur")]
    public float jumpForce;
    public bool ground;
    public CinemachineVirtualCamera vcam;
    public CinemachineFreeLook flcam;

    public bool fps = false;
    private void Awake()
    {
        controles = new PlayerInput();
        controles.Movement.Deplacement.performed += ctx => Direction2 = controles.Movement.Deplacement.ReadValue<Vector2>();
        controles.Movement.Deplacement.canceled += ctx => Direction2 = Vector2.zero;
        controles.Movement.Jump.performed += ctx => Jump();
        controles.Movement.SwapCamera.performed += ctx => Swap();

    }
    private void OnEnable()
    {
        controles.Enable();
    }
    private void OnDisable()
    {
        controles.Disable();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {/*
        Cursor.visible = false;*/
        
        Vector3 Direction = new Vector3(Direction2.x, 0, Direction2.y).normalized;
        if (Direction.magnitude >= 0.1f)
        {
            Vector3 moveDir = Vector3.zero;
            if(!fps)
            {
                float targetAngle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            }
            else
            {
                Vector3 moveDirFor = new Vector3(transform.forward.x * Direction.x, 0, transform.forward.z * Direction.x).normalized;
                Vector3 moveDirRight = new Vector3(transform.right.x * Direction.y, 0, transform.right.z * Direction.y).normalized;
                moveDir = (moveDirFor + moveDirRight); 
            }
            
            characterController.Move(moveDir.normalized * speed * Time.deltaTime);

        }
        CheckGrounded();
        if (!ground)
        {
            JumpgGravity();

        }
        characterController.Move(verticalVelocity * Vector3.up * Time.deltaTime);


    }

    void Jump()
    {
        if(ground)
        {
            verticalVelocity = jumpForce;

        }
    }

    void JumpgGravity()
    {

        if (verticalVelocity < 0)
        {
            verticalVelocity -= gravityScale * Time.deltaTime;

        }
        else
        {
            verticalVelocity -= gravityScale * Time.deltaTime;
        }
    }


    void CheckGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 2f))
        {
            Debug.Log("catouche");
            ground = true;
        }
        else
        {
            ground = false;
            Debug.Log("catouchepas");
        }
    }

    void Swap()
    {
        if(fps)
        {
           
            vcam.Priority = 4;
            flcam.Priority = 10;
        }
        else
        {
            vcam.Priority = 10;
            flcam.Priority = 4;
        }
        fps = !fps;
    }

  
}
