using System.Collections;
using System.Collections.Generic;
using TNRD.Autohook;
using UnityEngine;
using DG.Tweening;

public class Special_IncreasePitchOverTime : MonoBehaviour
{
    [SerializeField, AutoHook]
    private AudioSource music;

    private void Start()
    {
        music.DOPitch(1f, music.clip.length / 2).SetEase(Ease.InCirc);
    }

}
