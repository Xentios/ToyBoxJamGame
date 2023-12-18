using UnityEngine;
using UnityEngine.InputSystem;
using TNRD.Autohook;


public class DragonMovement : MonoBehaviour
{
    [SerializeField]
    private float speed=10;

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
    private void OnEnable()
    {
        actionsAsset.Enable();
        inputFlyUp.action.performed += PressUp;
        inputFlyDown.action.performed += PressDown;
        inputMoveRight.action.started += PressRight;
    }

    private void OnDisable()
    {
        inputFlyUp.action.performed -= PressUp;
        inputFlyDown.action.performed -= PressDown;
        inputMoveRight.action.started -= PressRight;
        actionsAsset.Disable();
    }

    private void Update()
    {
        if (dragonPivot.rotation != Quaternion.identity)
        {
            dragonPivot.rotation = Quaternion.Lerp(dragonPivot.rotation, Quaternion.identity,Time.deltaTime);
        }
        
    }

    private void PressUp(InputAction.CallbackContext callbackContext )
    {
        rb2d.AddRelativeForce(Vector2.up * speed);
       
    }

    private void PressDown(InputAction.CallbackContext callbackContext)
    {
        rb2d.AddRelativeForce(Vector2.down*speed);
        var rotation = dragonPivot.rotation.eulerAngles;
        dragonPivot.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z - 5);// - 10000f * (float) callbackContext.duration);
    }

    private void PressRight(InputAction.CallbackContext callbackContext)
    {
        rb2d.AddForce(Vector2.right* speed);
    }
}
