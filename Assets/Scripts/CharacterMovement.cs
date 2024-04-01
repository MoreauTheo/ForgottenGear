using System.Collections;
using System.Collections.Generic;
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
    public bool linkedToGear;
    public GameObject linkedGear;
    public bool ground;
    private void Awake()
    {
        controles = new PlayerInput();
        controles.Movement.Deplacement.performed += ctx => Direction2 = controles.Movement.Deplacement.ReadValue<Vector2>();
        controles.Movement.Deplacement.canceled += ctx => Direction2 = Vector2.zero;
        controles.Movement.Jump.performed += ctx => Jump();

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
            float targetAngle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
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

  
}
