using System.Collections;
using System.Collections.Generic;
using TNRD.Autohook;
using UnityEngine;

public class TimeRefresher : MonoBehaviour
{
    [SerializeField]
    private FloatVariable timeLeft;

    [SerializeField, AutoHook]
    private AudioSource timeUpgradeSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (timeLeft.Value > 0.4f)
        {
            timeLeft.ApplyChange(60);
            timeUpgradeSound.Play();
        }
    }
  

}
