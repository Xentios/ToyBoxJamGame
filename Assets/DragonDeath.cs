using System.Collections;
using System.Collections.Generic;
using TNRD.Autohook;
using UnityEngine;

public class DragonDeath : MonoBehaviour
{
    [SerializeField, AutoHook]
    private Animator animator;
    [SerializeField, AutoHook]
    private Collider2D col2D;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetTrigger("Death");       
    }

    public void AnimationEnding()
    {
        col2D.offset = Vector2.up/2;
    }
}
