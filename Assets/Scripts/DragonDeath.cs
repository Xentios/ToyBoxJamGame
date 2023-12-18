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



    private void OnCollisionEnter2D2(Collision2D collision)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        Vector2 relativeVelocity = collision.relativeVelocity;
        Vector2 collisionNormal = collision.GetContact(0).normal;

      
        float dotProduct = Vector2.Dot(relativeVelocity, collisionNormal);


        float headOnThreshold = (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) ? 7f : 4f;

        Debug.Log(headOnThreshold);
        // Check if the dot product is below the threshold
        if (dotProduct < headOnThreshold)
        {
            // The collision is not direct or head-on
            Debug.Log("Not a head-on collision");

            // Add your custom logic or actions here
        }
        else
        {
            animator.SetTrigger("Death");
            col2D.attachedRigidbody.gravityScale = 1f;
        }
    }

    public void AnimationEnding()
    {
        col2D.offset = Vector2.up/2;
        col2D.attachedRigidbody.gravityScale = 2f;
        dragonMovement.FindActionMap("Dragon Movement")?.Disable();
    }
}
