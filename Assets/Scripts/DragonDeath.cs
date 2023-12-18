using System.Collections;
using System.Collections.Generic;
using TNRD.Autohook;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragonDeath : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset dragonMovement;
    [SerializeField, AutoHook(AutoHookSearchArea.DirectChildrenOnly)]
    private Animator animator;
    [SerializeField, AutoHook]
    private Collider2D col2D;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debugger.Log("Collision sqrMagnitude is: " + collision.relativeVelocity.sqrMagnitude, Debugger.PriorityLevel.High);
        ContactPoint2D[] contacts = new ContactPoint2D[collision.contactCount];
        var contactsNumber=collision.GetContacts(contacts);
        Debugger.Log("Collision contactsNumber is : " + contactsNumber, Debugger.PriorityLevel.Medium);
        float sum = 0;
        for (int i = 0; i < contactsNumber; i++)
        {
            var contact=collision.GetContact(i);
            Debugger.Log("Contact "+i+" is " + contact.normalImpulse, Debugger.PriorityLevel.Low);
            sum += contact.normalImpulse;
        }

        if (sum< 15) return;
        Debugger.Log("Death Collision sqrMagnitude is: " + collision.relativeVelocity.sqrMagnitude,Debugger.PriorityLevel.MustShown);
        animator.SetTrigger("Death");
        col2D.attachedRigidbody.gravityScale = 1f;
    }

    public void AnimationEnding()
    {
        col2D.offset = Vector2.up/2;
        col2D.attachedRigidbody.gravityScale = 2f;
        dragonMovement.FindActionMap("Dragon Movement")?.Disable();
    }
}
