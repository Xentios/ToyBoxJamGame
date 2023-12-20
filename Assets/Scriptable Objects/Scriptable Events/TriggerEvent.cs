using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField]
    private InputActionReference ESC;

    [SerializeField]
    private  UnityEvent unityEvents;
    [SerializeField]
    private List<GameEvent> gameEvents;


   
    private float timeToTrigger;
    [SerializeField]
    private float setTimerToTrigger;

    [SerializeField]
    private bool TriggerOnAwake;
    [SerializeField]
    private bool TriggerOnStart;
    [SerializeField]
    private bool TriggerOnEnable;
    [SerializeField]
    private bool TriggerOnDisable;
    [SerializeField]
    private bool TriggerOnDestroy;

    private bool skipFlag;

    //[SerializeField]
    //private bool OnAwake;
    //[SerializeField]
    //private bool OnAwake;
    //[SerializeField]
    //private bool OnAwake;
    //[SerializeField]
    //private bool OnAwake;
    //[SerializeField]
    //private bool OnAwake;
    //[SerializeField]
    //private bool OnAwake;
    private void Awake()
    {
        if (TriggerOnAwake == true)
        {
            TriggerInvokeAll();
        }
    }
    void Start()
    {
        if (TriggerOnStart == true)
        {
            TriggerInvokeAll();
        }
    }

    private void OnEnable()
    {
        timeToTrigger = setTimerToTrigger;
        EnableInput();        
        if (TriggerOnEnable == true)
        {
            TriggerInvokeAll();
        }
    }

    private void OnDisable()
    {
        DisableInput();

        if (TriggerOnDisable == true)
        {
            TriggerInvokeAll();
        }
    }

    private void OnDestroy()
    {
        if (TriggerOnDestroy == true)
        {
            TriggerInvokeAll();
        }
    }


    [ContextMenu("TriggerEvents")]
    public void TriggerInvokeAll()
    {
        if (timeToTrigger == 0)
        {
            TIALL();
        }
        else if(timeToTrigger>0)
        {
            StartCoroutine(TriggerAllWithTime());
        }
        
    }


    IEnumerator TriggerAllWithTime()
    {
        timeToTrigger = setTimerToTrigger;
        skipFlag = false;
        float elapsedTime = 0f;
        
        while (elapsedTime < timeToTrigger && skipFlag==false)
        {   
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        timeToTrigger = setTimerToTrigger;
        TIALL();
    }


    private void TIALL()
    {
        unityEvents.Invoke();
        foreach (var gameEvent in gameEvents)
        {
            gameEvent.TriggerEvent();
        }
    }

    private void RemoveTimer(InputAction.CallbackContext callbackContext)
    {
        skipFlag = true;
        if (setTimerToTrigger < 0)
        {
            TIALL();
        }
    }


    private void EnableInput()
    {
        if (ESC == null) return;

        ESC.action.performed += RemoveTimer;
    }

    private void DisableInput()
    {
        if (ESC == null) return;

        ESC.action.performed -= RemoveTimer;
    }
}
