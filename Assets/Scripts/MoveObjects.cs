using System.Collections;
using System.Collections.Generic;
using TNRD.Autohook;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private float  speed;

    private bool onScreen;

    [SerializeField, AutoHook(AutoHookSearchArea.DirectChildrenOnly)]
    private Animator animator;

    void Start()
    {

        animator.Play(0, -1, Random.value);
    }


    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnBecameVisible()
    {
        onScreen = true;
    }

    private void OnBecameInvisible()
    {
        if (onScreen == true)  Destroy(gameObject);
    }
}
