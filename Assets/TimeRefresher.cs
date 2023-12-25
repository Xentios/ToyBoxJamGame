using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRefresher : MonoBehaviour
{
    [SerializeField]
    private FloatVariable timeLeft;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (timeLeft.Value > 0.4f)
        {
            timeLeft.ApplyChange(60);
        }
    }
  

}
