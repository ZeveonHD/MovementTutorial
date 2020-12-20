using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float movementSpeed = 1f;
    public float jumpPower = 10f;

    public float gravityFactor = -9.81f;
    public float currentVelY = 0;

    public bool isSprinting = false;
    public float sprintingMultiplier;

    public bool isCrouching = false;
    public float crouchingMulitplier;

    public CharacterController controller;
    public float standingHeight = 1.8f;
    public float crouchingHeight = 1.25f;

    public LayerMask groundMask;
    public Transform groundDetectionTransform;


    public bool isGrounded;

    public void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void CheckIsGrounded()
    {
        Collider[] cols = Physics.OverlapSphere(groundDetectionTransform.position, 0.05f, groundMask);

        if (cols.Length > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    public void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        CheckIsGrounded();

        if (isGrounded == false)
        {
            currentVelY += gravityFactor * Time.deltaTime;
        }
        else if (isGrounded == true)
        {
            currentVelY = -2f;
        }

        if (Input.GetKeyDown("space") && isGrounded == true)
        {
            currentVelY = jumpPower;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }

        if (Input.GetKey(KeyCode.LeftShift) && isCrouching == false)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        Vector3 movement = new Vector3();

        movement = inputX * transform.right + inputY * transform.forward;

        if (isCrouching == true)
        {
            controller.height = crouchingHeight;
            movement *= crouchingMulitplier;
        }
        else
        {
            controller.height = standingHeight;
        }

        if (isSprinting == true)
        {
            movement *= sprintingMultiplier;
        }

        controller.Move(movement * movementSpeed * Time.deltaTime);
        controller.Move(new Vector3(0,currentVelY * Time.deltaTime,0));
    }
}
