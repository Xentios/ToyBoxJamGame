using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private GameEvent gameEventTimeChanged;

    [SerializeField]
    private GameEvent gameOverEvent;

    [SerializeField]
    private FloatVariable timeLeft;


    private IEnumerator Start()
    {
        timeLeft.SetValue(60);
        gameEventTimeChanged.TriggerEvent();

        while (timeLeft.Value > 0f)
        {
            yield return new WaitForSeconds(1);
            timeLeft.ApplyChange(-1f);
            gameEventTimeChanged.TriggerEvent();
        }

        gameOverEvent.TriggerEvent();
    }
}
