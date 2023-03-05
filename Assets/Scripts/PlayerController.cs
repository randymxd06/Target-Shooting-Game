using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [Header ("References")]
    public Camera playerCamera;

    [Header ("General")]
    public float gravityScale = -20f;

    [Header ("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    [Header ("Rotation")]
    public float rotationSensibility = 1600f;

    [Header ("Jump")]
    public float jumpHeight = 1.9f;

    private float cameraVerticalAngle;
    Vector3 moveInput = Vector3.zero;
    Vector3 rotationInput = Vector3.zero;
    CharacterController characterController;

    private void Awake()
    {

        characterController = GetComponent<CharacterController>();

    }

    private void Update()
    {

        Look();

        Move();

    }

    private void Move()
    {

        if(characterController.isGrounded)
        {

            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            moveInput = Vector3.ClampMagnitude(moveInput, 1f);

            if(Input.GetButton("Sprint"))
            {
                moveInput = transform.TransformDirection(moveInput) * runSpeed;
            } else {
                moveInput = transform.TransformDirection(moveInput) * walkSpeed;
            }

            if(Input.GetButtonDown("Jump"))
            {
                moveInput.y = Mathf.Sqrt(jumpHeight * -2f * gravityScale);
            }

        }

        moveInput.y += gravityScale * Time.deltaTime;

        characterController.Move(moveInput * Time.deltaTime);

    }

    private void Look() 
    {

        rotationInput.x = Input.GetAxis("Mouse X") * rotationSensibility * Time.deltaTime;
        rotationInput.y = Input.GetAxis("Mouse Y") * rotationSensibility * Time.deltaTime;

        cameraVerticalAngle += rotationInput.y;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -70, 70);

        transform.Rotate(Vector3.up * rotationInput.x);
        playerCamera.transform.localRotation = Quaternion.Euler(-cameraVerticalAngle, 0f , 0f);

    }

}
