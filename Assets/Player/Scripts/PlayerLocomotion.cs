using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerLocomotion : MonoBehaviour
{
    CharacterController characterController;
    Transform playerContainer, cameraContainer;

    public float speed = 6.0f;    
    public float jumpSpeed = 10f;
    public float mouseSensitivity = 2000f;
    public float gravity = 20.0f;
    public float LookUpClamp = -30f;
    public float lookDownClamp = 60f;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 sprint = Vector3.zero;
    float rotateX, rotateY;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        GameManager.ResetGame();

        characterController = GetComponent<CharacterController>();
        SetCurrentCamera();        
    }

    // Update is called once per frame
    void Update()
    {
        if (!MenuController.IsGamePaused)
        {
            Locomotion();
            RotateAndLook();
            PerspectiveCheck();
        }        
    }

    void SetCurrentCamera()
    {
        SwitchPerspective switchPerspective = GetComponent<SwitchPerspective>();
        if (switchPerspective.GetPerspective() == SwitchPerspective.Perspective.First)
        {
            playerContainer = gameObject.transform.Find("Container1P");
            cameraContainer = playerContainer.transform.Find("Camera1PContainer");
        }
        else
        {
            playerContainer = gameObject.transform.Find("Container3P");
            cameraContainer = playerContainer.transform.Find("Camera3PContainer");
        }
    }
    void Locomotion()
    {
        if (characterController.isGrounded) // When grounded, set y-axis to zero (to ignore it)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
            if (Input.GetKey(KeyCode.C))
            {
                characterController.height = 0.65f;
                characterController.center = new Vector3(0f, 0.5f, 0f);
            }            
            else //if (Input.GetKeyUp(KeyCodDe.C))
            {
                characterController.height = 2f;
                characterController.center = new Vector3(0f, 1f, 0f);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 11.0f;
            }
            else
            {
                speed = 6.0f;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void RotateAndLook()
    {
        rotateX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        rotateY -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotateY = Mathf.Clamp(rotateY, LookUpClamp, lookDownClamp);

        transform.Rotate(0f, rotateX, 0f);

        cameraContainer.transform.localRotation = Quaternion.Euler(rotateY, 0f, 0f);
    }
    void PerspectiveCheck()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchPerspective switchPerspective = GetComponent<SwitchPerspective>();

            if (switchPerspective != null)
            {
                if (switchPerspective.GetPerspective() == SwitchPerspective.Perspective.First)
                {
                    switchPerspective.SetPerspective(SwitchPerspective.Perspective.Third);
                }
                else
                {
                    switchPerspective.SetPerspective(SwitchPerspective.Perspective.First);
                }
                SetCurrentCamera();
            }
        }
    }
}
