using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class KeyActionEvent : MonoBehaviour
{
    [SerializeField] InputActionReference keyPress;
    [SerializeField] UnityEvent pressEvent;



    [SerializeField] UnityEvent canvelEvent;

    private void OnEnable()
    {
        if (keyPress == null) return;

        keyPress.action.Enable();
        keyPress.action.performed+=TriggerPerformedEvent;
        keyPress.action.canceled += TriggerCancelEvent;
    }

    private void OnDisable()
    {
        if (keyPress == null) return;

        keyPress.action.performed -= TriggerPerformedEvent;
        keyPress.action.canceled -= TriggerCancelEvent;
        keyPress.action.Disable();
    }



    private void TriggerPerformedEvent(InputAction.CallbackContext callbackContext)
    {
        pressEvent?.Invoke();
    }

    private void TriggerCancelEvent(InputAction.CallbackContext callbackContext)
    {
        canvelEvent?.Invoke();
    }

    public void TriggerPressEvents()
    {
        pressEvent?.Invoke();
    }

    public void TriggerCancelEvents()
    {
        canvelEvent?.Invoke();
    }
}
