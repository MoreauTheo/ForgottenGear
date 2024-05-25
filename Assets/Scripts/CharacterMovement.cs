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
    private Vector2 lookDelta;
    public float lookSpeed;
    public bool fps = false;
    public GameObject character;
    public GameObject reticule;
    public Animator animator;
    private void Awake()
    {
        controles = new PlayerInput();
        controles.Movement.Deplacement.performed += ctx => Direction2 = controles.Movement.Deplacement.ReadValue<Vector2>();
        controles.Movement.Deplacement.canceled += ctx => Direction2 = Vector2.zero;
        controles.Movement.Jump.performed += ctx => Jump();
        controles.Movement.SwapCamera.performed += ctx => Swap();
        controles.Movement.HorizontalLook.performed += ctx => lookDelta = controles.Movement.HorizontalLook.ReadValue<Vector2>();
        controles.Movement.HorizontalLook.canceled += ctx => lookDelta = Vector2.zero;
    }
    public void OnEnable()
    {
        controles.Enable();
    }
    public void OnDisable()
    {
        controles.Disable();
    }
    void Start()
    {
    }

    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (fps)
            transform.Rotate(0, lookDelta.x * Time.deltaTime * lookSpeed, 0);

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
               

                moveDir = new Vector3(cam.transform.forward.x,0,cam.transform.forward.z).normalized * Direction2.y;
                moveDir += new Vector3(cam.transform.right.x, 0, cam.transform.right.z).normalized * Direction2.x;
            }
            
            characterController.Move(moveDir.normalized * speed * Time.deltaTime);
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);

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
            ground = true;
        }
        else
        {
            ground = false;
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
        StartCoroutine(CharacterDisapear());
    }


    IEnumerator CharacterDisapear()
    {
        yield return new WaitForSeconds(0.5f);
        if (character.activeSelf == true)
        {
            character.SetActive(false);
            reticule.SetActive(true);
        }
        else
        {
            character.SetActive(true);
            reticule.SetActive(false);


        }
    }


}
