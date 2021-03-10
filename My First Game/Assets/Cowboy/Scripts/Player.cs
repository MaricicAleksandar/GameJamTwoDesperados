using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    //VARIABLES
    [SerializeField]private bool isGrounded;
    private float groundCheckDistance = 0.2f;
    private LayerMask groundMask;
    private TerrainLayer terrainLayer;
    private float walkSpeed = 30;
    private float runSpeed = 80;
    private float gravity = -9.8f;
    private float jumpHeight = 5f;
    private Vector3 velocity;
    private Vector3 movement;
    private float moveSpeed;


    //REFERENCES
    private CharacterController characterController;
    private Animator animator;
    
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        terrainLayer = GetComponent<TerrainLayer>();
    }

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        groundMask = LayerMask.GetMask(LayerMask.LayerToName(9));
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement = new Vector3(horizontal, 0, vertical);
        movement = transform.TransformDirection(movement);
        characterController.SimpleMove(movement * Time.deltaTime * moveSpeed);
        movement *= moveSpeed;

        Quaternion newDirection = Quaternion.LookRotation(movement);

        if (isGrounded)
        {
            if (movement != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            else if (movement != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            else if (movement != Vector3.zero && Input.GetKey(KeyCode.E))
            {
                WalkAndAim();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            else
            {
                Idle();
            }
        }  
    }


    private void Walk()
    {
        moveSpeed = walkSpeed;
        animator.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

    private void WalkAndAim()
    {
        moveSpeed = walkSpeed;
        animator.SetFloat("Aim", 2.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        animator.SetFloat("Speed", 2.2f, 0.1f, Time.deltaTime);
    }

    private void Idle()
    {
        moveSpeed = walkSpeed;
        animator.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        animator.SetFloat("Speed", -0.5f, -0.1f, Time.deltaTime);
        characterController.Move(movement * Time.deltaTime);
        characterController.Move(velocity * Time.deltaTime);
    }
}

//{Greske u kodiranju iskoriscene u kodu iznad

//    //VARIABLES
//    [SerializeField] private float moveSpeed;
//    [SerializeField] private float walkSpeed;
//    [SerializeField] private float runSpeed;
//    [SerializeField] private bool isGrounded;
//    [SerializeField] private float groundCheckDistance;
//    [SerializeField] private LayerMask groundMask;
//    [SerializeField] private float gravity;
//    [SerializeField] private float jumpHeight;


//    private Vector3 moveDirection;
//    private Vector3 velocity;
//    private Vector3 cameraMovement;

//    //REFFERENCES
//    private CharacterController controller;
//    private Animator anim;

//    // Update is called once per frame
//    void Update()
//    {
//        Move();
//    }

//    private void Move()
//    {
//        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

//        if(isGrounded && velocity.y < 0)
//        {
//            velocity.y = -2f;
//        }

//        float moveZ = Input.GetAxis("Vertical");
//        moveDirection = new Vector3(0, 0, moveZ);
//        moveDirection = transform.TransformDirection(moveDirection);


//        if (isGrounded)
//        {
//            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
//            {
//                WalkForward();
//            }
//            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
//            {
//                WalkAndAim();
//            }
//            else if (- moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
//            {
//                WalkBackward();
//            }

//            else if (moveDirection != Vector3.zero && moveDirection != -Vector3.forward && Input.GetKey(KeyCode.LeftShift))
//            {
//                Run();
//            }
//            else
//            {
//                Idle();
//            }


//            moveDirection *= moveSpeed;

//            if (Input.GetKeyDown(KeyCode.Space))
//            {
//                Jump();
//            }
//        }

//        controller.Move(moveDirection * Time.deltaTime);
//        velocity.y += gravity * Time.deltaTime;
//        controller.Move(velocity * Time.deltaTime);
//    }

//       private void Idle()
//    {
//        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
//    }

//    private void WalkForward()
//    {
//        moveSpeed = walkSpeed;
//        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
//    }

//    private void WalkAndAim()
//    {
//        moveSpeed = walkSpeed;
//        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
//        anim.SetFloat("Combat", 1, 0.1f, Time.deltaTime);
//    }

//    private void WalkBackward()
//    {
//        moveSpeed = walkSpeed;
//        anim.SetFloat("Speed", -1, 0.1f, Time.deltaTime);
//    }

//    private void Run()
//    {
//        moveSpeed = runSpeed;
//        anim.SetFloat("Speed", 2, 0.1f, Time.deltaTime);
//    }

//    private void Jump()
//    {
//        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
//        anim.SetFloat("Speed", 3, 0.1f, Time.deltaTime);
//    }
//}
