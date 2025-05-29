using Cinemachine;
using UnityEngine;
using System.Collections;

public class FPScontroller : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float mouseSensivity = 2f;

    [Header("Crouching")]
    [SerializeField] private float crouchSpeed = 5f;
    [SerializeField] private float crouchTransitionSpeed = 5f;
    private bool isCrouching = false;

    [Header("Footstep Sounds")]
    [SerializeField] private AudioSource footstepAudioSource;
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private float walkStepInterval = 0.5f;
    [SerializeField] private float runStepInterval = 0.3f;
    private float stepTimer;

    [Header("Cinemachine")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform cameraPivot;

    private CharacterController controller;
    private float verticalSpeed;
    private Vector3 originalCenter;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalCenter = controller.center;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleLook();
        HandleFootsteps();
    }

    private void HandleMovement()
    {
        float speed;
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
            speed = runSpeed; 
        else if (isCrouching)
            speed = crouchSpeed;
        else
            speed = walkSpeed;

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 move = transform.TransformDirection(input) * speed;

        if (controller.isGrounded && verticalSpeed < 0)
        {
            verticalSpeed = -2f;

            if (Input.GetButtonDown("Jump") && !isCrouching)
            {
                verticalSpeed = jumpForce;
            }
        }

        verticalSpeed += gravity * Time.deltaTime;
        move.y = verticalSpeed;

        controller.Move(move * Time.deltaTime);
    }

    private void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity;

        transform.Rotate(Vector3.up * mouseX);

        float verticalRotation = -mouseY;

        float currentCameraAngle = cameraPivot.localEulerAngles.x;
        float newAngle = currentCameraAngle + verticalRotation;

        if (newAngle > 180)
            newAngle -= 360;
        newAngle = Mathf.Clamp(newAngle, -90, 70);

        cameraPivot.localEulerAngles = new Vector3(newAngle, 0, 0);
    }

    private void HandleFootsteps()
    {
        if (controller.isGrounded && controller.velocity.magnitude > 0.2f)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0)
            {
                float currentInterval = (Input.GetKey(KeyCode.LeftShift) && !isCrouching) ? runStepInterval : walkStepInterval;
                stepTimer = currentInterval;

                if (footstepSounds.Length > 0 && footstepAudioSource != null)
                {
                    int randomIndex = Random.Range(0, footstepSounds.Length);
                    footstepAudioSource.PlayOneShot(footstepSounds[randomIndex]);
                }
            }
        }
    }
    private IEnumerator CrouchStandTransition()
    {
        float time = 0;
        float currentHeight = controller.height;
        Vector3 currentCenter = controller.center;

        while (time < 1)
        {
            time += Time.deltaTime * crouchTransitionSpeed;
            yield return null;
        }
    }
}