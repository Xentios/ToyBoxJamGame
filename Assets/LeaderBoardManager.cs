using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using TMPro;

public class LeaderBoardManager : MonoBehaviour
{
    [SerializeField]
    private GameObject highScoresRoot;

    [SerializeField]
    private PlayFabLogin playFabLogin;


    private void Start()
    {
       
    }


    public void UpdateLeaderBoard()
    {
        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
        {
            StatisticName = "High Scores",
            StartPosition = 0,
            MaxResultsCount = 10
        }, result => DisplayLeaderboard(result), FailureCallback);
    }

    private void FailureCallback(PlayFabError obj)
    {

        Debugger.Log(obj.Error,Debugger.PriorityLevel.MustShown);
      
    }

    private void DisplayLeaderboard(GetLeaderboardResult result)
    {
        bool foundMyself = false;
        for (int i = 0; i < 10; i++)
        {
            if (result.Leaderboard.Count <= i) break;
            var row = highScoresRoot.transform.GetChild(i);

            row.GetChild(0).GetComponent<TextMeshProUGUI>().text = " " + (i + 1) + ": ";
            row.GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + result.Leaderboard[i].DisplayName;
            row.GetChild(2).GetComponent<TextMeshProUGUI>().text = "" + result.Leaderboard[i].StatValue;
            if (result.Leaderboard[i].PlayFabId.Equals(playFabLogin.CurrentPlayerId))
            {
                foundMyself = true;
            }

        }

        if (foundMyself == false)
        {
            PlayFabClientAPI.GetLeaderboardAroundPlayer(new GetLeaderboardAroundPlayerRequest
            {
                StatisticName = "High Scores",               
                MaxResultsCount = 1
            }, result => DisplayMyPlace(result), FailureCallback);

        }
    }

    private void DisplayMyPlace(GetLeaderboardAroundPlayerResult result)
    {
        var row = highScoresRoot.transform.GetChild(11);
        foreach (var myStats in result.Leaderboard)
        {
            row.GetChild(0).GetComponent<TextMeshProUGUI>().text = " " + (myStats.Position+1)+" : ";
            row.GetChild(1).GetComponent<TextMeshProUGUI>().text = " " + myStats.DisplayName;
            row.GetChild(2).GetComponent<TextMeshProUGUI>().text = " " + myStats.StatValue;
        }
    }
}
