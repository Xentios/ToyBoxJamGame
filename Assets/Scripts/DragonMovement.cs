using UnityEngine;
using UnityEngine.InputSystem;
using TNRD.Autohook;
using System;

public class DragonMovement : MonoBehaviour
{
    [SerializeField]
    private float forwardSpeed=10;
    [SerializeField]
    private float upSpeed = 3;
    [SerializeField]
    private float downSpeed = 30;
    [SerializeField]
    private float maxSpeed = 30f;


    [SerializeField]
    private Transform dragonPivot;

    [SerializeField]
    private InputActionAsset actionsAsset;

    [SerializeField]
    private InputActionReference inputFlyUp;
    [SerializeField]
    private InputActionReference inputFlyDown;
    [SerializeField]
    private InputActionReference inputMoveRight;

    [SerializeField , AutoHook]
    private Rigidbody2D rb2d;

    [SerializeField, AutoHook(AutoHookSearchArea.DirectChildrenOnly)]
    private Animator animator;

    [SerializeField, AutoHook]
    private AudioSource audioSource;


    private bool PressingDown;
    private bool PressingUp;
    private bool PressingRight;
    private void OnEnable()
    {
        actionsAsset.Enable();
        inputFlyUp.action.started += PressUp;
        inputFlyUp.action.canceled += PressUpCancel;
        inputFlyDown.action.started += PressDown;
        inputFlyDown.action.canceled += PressDownCancel;
        inputMoveRight.action.started += PressRight;
        inputMoveRight.action.canceled += PressRightCancel;
    }

   

    private void OnDisable()
    {
        inputFlyUp.action.started -= PressUp;
        inputFlyUp.action.canceled -= PressUpCancel;
        inputFlyDown.action.started -= PressDown;
        inputFlyDown.action.canceled -= PressDownCancel;
        inputMoveRight.action.started -= PressRight;
        inputMoveRight.action.canceled -= PressRightCancel;
        actionsAsset.Disable();
    }


    private void Update()
    {
        animator.SetBool("Diving", PressingDown == true && PressingUp == false);
        if (PressingDown==true&& PressingUp == false)
        {
            rb2d.AddRelativeForce(Vector2.down * downSpeed*Time.deltaTime);
            rb2d.AddRelativeForce(Vector2.left * (forwardSpeed/2f) * Time.deltaTime);
            audioSource.enabled = false;
           
        }
        else
        {
            audioSource.enabled = true;
        }

        if (PressingUp==true)
        {
            rb2d.AddRelativeForce(Vector2.up * upSpeed * rb2d.velocity.x * 2f * Time.deltaTime);
        }

        if (PressingRight == true&& PressingDown==false)
        {
            rb2d.AddRelativeForce(Vector2.right * forwardSpeed * Time.deltaTime);
        }

       
        animator.SetFloat("Velocity", rb2d.velocity.sqrMagnitude);
        SetAudio(rb2d.velocity.sqrMagnitude);
    }



    void FixedUpdate()
    {
        Debugger.Log("Dragon velocity" + rb2d.velocity.magnitude, Debugger.PriorityLevel.LeastImportant);
        if (rb2d.velocity.magnitude > maxSpeed)
        {
            rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
        }
    }
    private void PressUp(InputAction.CallbackContext callbackContext )
    {
        PressingUp = true;
    }
    private void PressUpCancel(InputAction.CallbackContext obj)
    {
        PressingUp = false;
    }

    private void PressDown(InputAction.CallbackContext callbackContext)
    {
        PressingDown = true;
    }

    private void PressDownCancel(InputAction.CallbackContext callbackContext)
    {
        PressingDown = false;
    }

    private void PressRight(InputAction.CallbackContext callbackContext)
    {
        PressingRight = true;
    }

    private void PressRightCancel(InputAction.CallbackContext obj)
    {
        PressingRight = false;
    }

    

    private void SetAudio(float value)
    {
        if(value<2)
        {
            audioSource.pitch = 0f;
            return;
        }

        var result = 0.5f;
        result=Map(value,  2, 1200, 0.5f, 1.5f);
        audioSource.pitch = result;
    }

    float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
