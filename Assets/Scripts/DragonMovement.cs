using UnityEngine;
using UnityEngine.InputSystem;
using TNRD.Autohook;


public class DragonMovement : MonoBehaviour
{
    [SerializeField]
    private float speed=10;
    [SerializeField]
    private float upSpeed = 3;
    [SerializeField]
    private float downSpeed = 30;


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

    private float timer;
    private void Update()
    {
       
        if (dragonPivot.rotation != Quaternion.identity)
        {
            timer += Time.deltaTime;

            if (timer > 1f)
            {
               // dragonPivot.rotation = Quaternion.Lerp(dragonPivot.rotation, Quaternion.identity, 0.5f);
                timer = 0;
            }
           
            //PixelRotate p = dragonPivot.GetChild(0).GetComponent<PixelRotate>();
            //p.SetRotate(rotation.z - 5);
        }
        
    }

    private void PressUp(InputAction.CallbackContext callbackContext )
    {
        rb2d.AddRelativeForce(Vector2.up * upSpeed * rb2d.velocity.x*2f);
        


    }

    private void PressDown(InputAction.CallbackContext callbackContext)
    {
        rb2d.AddRelativeForce(Vector2.down*downSpeed);
        //var rotation = dragonPivot.rotation.eulerAngles;
        //dragonPivot.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z - 5);// - 10000f * (float) callbackContext.duration);
        //PixelRotate p = dragonPivot.GetChild(0).GetComponent<PixelRotate>();
        //p.SetRotate(rotation.z - 5);
    }

    private void PressRight(InputAction.CallbackContext callbackContext)
    {
        rb2d.AddForce(Vector2.right* speed);
    }
}
