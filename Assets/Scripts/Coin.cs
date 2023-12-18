using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TNRD.Autohook;

public class Coin : MonoBehaviour
{
    [SerializeField, AutoHook]
    private Animator animator;

    [SerializeField]
    private float upSpeed=3f;

    [SerializeField]
    private float scaleDuration = 2f;

    public void Collected()
    {
        //Play Coin Collected Sound
        animator.enabled = false;
        transform.DOBlendableMoveBy(Vector2.up* upSpeed, 1f);
        transform.DOScale(0, scaleDuration);
    }
}
