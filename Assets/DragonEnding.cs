using System.Collections;
using System.Collections.Generic;
using TNRD.Autohook;
using UnityEngine;
using DG.Tweening;

public class DragonEnding : MonoBehaviour
{

    [SerializeField]
    private GameEvent gameOverEvent;

    [SerializeField, AutoHook]
    private Rigidbody2D rb;

    [SerializeField, AutoHook]
    private DragonMovement dragonMovement;

    [SerializeField]
    private Transform[] pathTransforms;

    private Vector3[] paths;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ending")){
            Ending();
        }
    }

    private void Start()
    {
        paths = new Vector3[pathTransforms.Length];
        for (int i = 0; i < pathTransforms.Length; i++)
        {
            paths[i] = pathTransforms[i].position;
        }
    }

    private void Ending()
    {               
        rb.isKinematic = true;       
        dragonMovement.enabled = false;
        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (paths[i].x < transform.position.x)
            {
                paths[i] = transform.position;
            }
        }
        rb.velocity = Vector2.zero;
        transform.DOPath(paths,5f,PathType.CatmullRom, PathMode.Sidescroller2D, 5,Color.red).OnComplete(()=>gameOverEvent.TriggerEvent());
     
    }


}
