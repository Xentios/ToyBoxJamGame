using System.Collections;
using System.Collections.Generic;
using TNRD.Autohook;
using UnityEngine;
using UnityEngine.Events;

public class CloudMover : MonoBehaviour
{
    [SerializeField]
    private float lifeTime;

    [SerializeField]
    private float movementSpeed;

    [SerializeField, AutoHook(HideWhenFound = true)]
    private Animator animator;

    public UnityEvent<CloudMover> EndOfLifeEvent;


    private bool OnScreen;
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        lifeTime = Random.Range(5,15f);
        movementSpeed= Random.Range(0, 1f);
    }


    private void OnDestroy()
    {
        EndOfLifeEvent.RemoveAllListeners();
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        transform.position += movementSpeed * Time.deltaTime * Vector3.left;
        if (lifeTime<0)
        {
            Death();
        }

    }

    private void OnBecameVisible()
    {
        OnScreen = true;
    }

    private void OnBecameInvisible()
    {
        if (OnScreen == true)
        {
            OnScreen = false;
            AnimationEndEvent();
        }
    }

    private void OnEnable()
    {
        Init();
    }

    private void Death()
    {
        animator.SetTrigger("End");
    }

    public void AnimationEndEvent()
    {
        EndOfLifeEvent?.Invoke(this);
        gameObject.SetActive(false);
    }
}
