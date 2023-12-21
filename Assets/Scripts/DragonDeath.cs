using System.Collections;
using System.Collections.Generic;
using TNRD.Autohook;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class DragonDeath : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset dragonMovement;
    [SerializeField, AutoHook(AutoHookSearchArea.DirectChildrenOnly)]
    private Animator animator;
    [SerializeField, AutoHook(AutoHookSearchArea.DirectChildrenOnly)]
    private SpriteRenderer dragonModel;
    [SerializeField, AutoHook]
    private Collider2D col2D;
    [SerializeField, AutoHook]
    private AudioSource wingSound;

    [SerializeField]
    private AudioClip hitSound;

    [SerializeField]
    private List<AudioClip> softHitSounds;

    [SerializeField]
    private AudioSource deathSound;

    private bool IsDead;

    private int hitSoundLooper;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsDead == false)
        {
            Vector2 relativeVelocity = collision.relativeVelocity;
            Vector2 collisionNormal = collision.GetContact(0).normal;

            float dotProduct = Vector2.Dot(relativeVelocity, collisionNormal);
            float headOnThreshold = (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) ? 7f : 4f;
                       
            if (dotProduct > headOnThreshold)
            {            
                Dying();
            }
        }
        else
        {           
            deathSound.PlayOneShot(softHitSounds[hitSoundLooper]);
            hitSoundLooper++;
            hitSoundLooper %= softHitSounds.Count;
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin")) return;

        Dying();
        DOVirtual.Color(Color.yellow, Color.red, 2, (value) =>
        {
            dragonModel.color = value;
        }).SetEase(Ease.InExpo);
       
    }

    private void Dying()
    {
        if (IsDead == true) return;

        IsDead = true;
        wingSound.volume = 0;
        animator.SetTrigger("Death");
        col2D.attachedRigidbody.gravityScale = 1f;
        deathSound.Play();
    }

    public void AnimationEnding()
    {
       
        col2D.offset = Vector2.up/2;
        col2D.attachedRigidbody.gravityScale = 2f;
        dragonMovement.FindActionMap("Dragon Movement")?.Disable();
        deathSound.clip = null;
       
    }
}
