// Ignore Spelling: Leaderboard

using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabLogin:MonoBehaviour
{
    const string FakeId= "4A78C25C8148E120";

    public string CurrentPlayerId;

     [SerializeField]
    public GameEvent connected;

    [SerializeField]
    private FloatVariable HighScore;

    public static string DeviceUniqueIdentifier
    {
        get
        {
            var deviceId = "";


#if UNITY_EDITOR
            deviceId = SystemInfo.deviceUniqueIdentifier + "-editor5";
#elif UNITY_ANDROID
                    AndroidJavaClass up = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
                    AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject> ("currentActivity");
                    AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject> ("getContentResolver");
                    AndroidJavaClass secure = new AndroidJavaClass ("android.provider.Settings$Secure");
                    deviceId = secure.CallStatic<string> ("getString", contentResolver, "android_id");
#elif UNITY_WEBGL
                    if (!PlayerPrefs.HasKey("UniqueIdentifier"))
                        PlayerPrefs.SetString("UniqueIdentifier", Guid.NewGuid().ToString());
                    deviceId = PlayerPrefs.GetString("UniqueIdentifier");
#else
                    deviceId = SystemInfo.deviceUniqueIdentifier;
#endif
            return deviceId;
        }
    }

    public void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            /*
            Please change the titleId below to your own titleId from PlayFab Game Manager.
            If you have already set the value in the Editor Extensions, this can be skipped.
            */
            PlayFabSettings.staticSettings.TitleId = "42";
        }
        var request = new LoginWithCustomIDRequest { CustomId=DeviceUniqueIdentifier, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
      
    }

    //void CreatePlayerAndUpdateDisplayName()
    //{
    //    PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest
    //    {
    //        CustomId = "PlayFabGetPlayerProfileCustomId",
    //        CreateAccount = true
    //    }, result => {
    //        Debug.Log("Successfully logged in a player with PlayFabId: " + result.PlayFabId);
    //        UpdateDisplayName();
    //    }, error => Debug.LogError(error.GenerateErrorReport()));
    //}

    void UpdateDisplayName()
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = "Anonymous" + UnityEngine.Random.Range(0, 10000)
        }, result =>
        {
            Debug.Log("The player's display name is now: " + result.DisplayName);
            if (!PlayerPrefs.HasKey("DisplayName"))
                PlayerPrefs.SetString("DisplayName", result.DisplayName);
        }, error => Debug.LogError(error.GenerateErrorReport())); ;
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        if (result.NewlyCreated)
        {
            UpdateDisplayName();
        }
        else
        {
            GetStatistics();
        }
        CurrentPlayerId = result.PlayFabId;
        GetStatisticsOfCurrentPlayer();
        GetStatisticsOfLastOne();
        connected.TriggerEvent();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }


    [ContextMenu("Save Score")]
    public void SaveFakeScore()
    {
        SubmitScore(UnityEngine.Random.Range(1, 1000));
    }

    [ContextMenu("Save Score SaveFakeScoreNegative")]
    public void SaveFakeScoreNegative()
    {
        SubmitScore(-1);
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

    

    private void OnStatisticsUpdated(UpdatePlayerStatisticsResult updateResult)
    {
        Debug.Log("Successfully submitted high score");
    }

    private void FailureCallback(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your API call. Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    [ContextMenu("RequestLeaderboard")]
    public void RequestLeaderboard()
    {
        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
        {
            StatisticName = "High Scores",
            StartPosition = 0,
            MaxResultsCount = 10
        }, result => DisplayLeaderboard(result), FailureCallback);
    }

    private void DisplayLeaderboard(GetLeaderboardResult result)
    {
        
        foreach (var item in result.Leaderboard)
        {
            Debug.Log(item.DisplayName+item.Profile+item.PlayFabId+"-"+item.Position+" stats "+item.StatValue);
        }
        
    }

    //private void FailureCallback(PlayFabError error)
    //{
    //    Debug.LogWarning("Something went wrong with your API call. Here's some debug information:");
    //    Debug.LogError(error.GenerateErrorReport());
    //}

    void GetStatistics()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStatistics,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    void GetStatisticsOfLastOne()
    {
        //var y=new GetFriendLeaderboardAroundPlayerRequest().;

        PlayFabClientAPI.GetFriendLeaderboardAroundPlayer(new GetFriendLeaderboardAroundPlayerRequest
        {
            StatisticName = "High Scores",
            PlayFabId= FakeId,           
            MaxResultsCount = 10
        }, result => DisplayLastGuy(result), FailureCallback); ;
    }

    void GetStatisticsOfCurrentPlayer()
    {
       
        PlayFabClientAPI.GetLeaderboardAroundPlayer(new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "High Scores",
            MaxResultsCount = 1
        }, result => DisplayMyStats(result), FailureCallback); ;
    }

    void DisplayMyStats(GetLeaderboardAroundPlayerResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            Debug.Log("My  position " + (item.Position+1));
            Debug.Log("My name " + item.DisplayName);
            Debug.Log("My score " + item.StatValue);
        }

    }



    void DisplayLastGuy(GetFriendLeaderboardAroundPlayerResult result)
    {
        

        foreach (var item in result.Leaderboard)
        {
            
            Debug.Log("Last guys position "+item.Position);
            Debug.Log("Last guys name " + item.DisplayName);
            Debug.Log("Last guys score " + item.StatValue);
        }
    }

    void OnGetStatistics(GetPlayerStatisticsResult result)
    {
        Debug.Log("Received the following Statistics:");        
        foreach (var eachStat in result.Statistics)
        {
            if(eachStat.StatisticName=="High Scores")
            {
                HighScore.Value = eachStat.Value;                
            }            
            Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
        }
           
    }
}