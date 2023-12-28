using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class GameOverManager : MonoBehaviour
{

    [SerializeField]
    private DragonMovement dragonMovement;
    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private FloatVariable score;
    [SerializeField]
    private FloatVariable HighScore;
    [SerializeField]
    private FloatVariable timeLeft;

    [SerializeField]
    private AudioSource goodEndingSound;
    [SerializeField]
    private AudioSource badEndingSound;


    private bool alreadyEnded;

    public void GameEnding()
    {
        if (alreadyEnded == true) return;
        alreadyEnded = true;

        dragonMovement.enabled = false;

        gameOverScreen.SetActive(true);

        if (timeLeft.Value > 0)
        {
            goodEndingSound.Play();
        }
        else
        {
            badEndingSound.Play();
        }
        if(score.Value> HighScore.Value)
        {
            SubmitScore((int) score.Value);
        }
    }


    private void SaveScore()
    {

    }

    public void SubmitScore(int playerScore)
    {
        
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
            new StatisticUpdate {
                StatisticName = "High Scores",
                Value = playerScore
            }
        }
        }, result => OnStatisticsUpdated(result), FailureCallback);
    }

    private void FailureCallback(PlayFabError obj)
    {
        
    }

    private void OnStatisticsUpdated(UpdatePlayerStatisticsResult result)
    {
        HighScore.Value = score.Value;
    }
}
