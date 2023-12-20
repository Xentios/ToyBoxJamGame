using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TNRD.Autohook;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private GameEvent collectedEvent;

    [SerializeField]
    private FloatVariable Score;

    [SerializeField]
    private float upSpeed=3f;

    [SerializeField]
    private float scaleDuration = 2f;

    [SerializeField, AutoHook]
    private AudioSource audioSource;

    [SerializeField, AutoHook]
    private Collider2D theCollider;

    public void Collected()
    {
        theCollider.enabled = false;
        transform.DOBlendableMoveBy(Vector2.up* upSpeed, 1f);
        transform.DOScale(0, scaleDuration);
        Score.ApplyChange(1);
        collectedEvent.TriggerEvent();
        audioSource.pitch = UnityEngine.Random.Range(1, 1.5f);
        audioSource.Play();
    }
}
