using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 15.0f;
    public float runSpeedModifier = 2.0f;
    public float crouchSpeedModifier = 0.5f;

    private CharacterController playerMove;
    private bool jumpHeld = false;

    void Start()
    {
        GameObject firstPersonCrosshair = GameObject.Find("RedDotCrosshair");
        if(firstPersonCrosshair != null)
        {
            firstPersonCrosshair.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerMove = GetComponent<CharacterController>();
    }

    void Update()
    {
        float movement = Input.GetAxis("Vertical") * movementSpeed;
        float direction = Input.GetAxis("Mouse X") * rotationSpeed;

        bool run = Input.GetAxis("Fire3") > 0.1f; // shift
        bool crouch = Input.GetAxis("Fire1") > 0.1f; // ctrl

        if (run && movement > 0.0f)
        {
            movement *= runSpeedModifier;
        }
        else if(crouch)
        {
            movement *= crouchSpeedModifier;
        }

        Vector3 moveThisFrame = new Vector3(0.0f, 0.0f, movement);
        moveThisFrame = transform.TransformDirection(moveThisFrame);

        playerMove.SimpleMove(moveThisFrame);
        transform.Rotate(0.0f, direction, 0.0f);

        bool jump = Input.GetAxis("Jump") > 0.1f; // space
        if (jump && !jumpHeld)
        {
            jumpHeld = true;
        }
        else if (!jump && jumpHeld)
        {
            jump = false;
            jumpHeld = false;
        }
        else
        {
            jump = false;
        }

        ApplyAnimations(movement, jump, crouch);
    }

    void ApplyAnimations(float movement, bool jumpPressed, bool crouchHeld)
    {
        Animator animator = GetComponentInChildren<Animator>();
        animator.SetFloat("movementSpeed", movement);
    }
}
