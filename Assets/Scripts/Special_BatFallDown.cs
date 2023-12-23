using System.Collections;
using System.Collections.Generic;
using TNRD.Autohook;
using UnityEngine;
using DG.Tweening;

public class Special_BatFallDown : MonoBehaviour
{
    [SerializeField, AutoHook]
    private AudioSource hitSound;

    [SerializeField, AutoHook]
    private MoveObjects moScript;

    [SerializeField]
    private Transform[] pathTransforms;

    private Vector3[] path;

    private bool test;
    private void Start()
    {
        path = new Vector3[pathTransforms.Length];
        for (int i = 0; i < pathTransforms.Length; i++)
        {
            path[i] = pathTransforms[i].position;
        }
    }
       
    private void OnCollisionEnter2D(Collision2D collision)
    {
        moScript.enabled = false;
        hitSound.Play();

        if (test) return;
        test = true;
        transform.DOPath(path, 3f, PathType.CatmullRom, PathMode.Sidescroller2D);
    }

    private void OnBecameInvisible()
    {
        GameObject.Destroy(gameObject);
    }
}
