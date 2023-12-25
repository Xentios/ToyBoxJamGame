// Ignore Spelling: Leaderboard

using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabLogin:MonoBehaviour
{
    [SerializeField]
    public GameEvent connected;

    public static string DeviceUniqueIdentifier
    {
        get
        {
            var deviceId = "";


#if UNITY_EDITOR
            deviceId = SystemInfo.deviceUniqueIdentifier + "-editor";
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

    void CreatePlayerAndUpdateDisplayName()
    {
        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest
        {
            CustomId = "PlayFabGetPlayerProfileCustomId",
            CreateAccount = true
        }, result => {
            Debug.Log("Successfully logged in a player with PlayFabId: " + result.PlayFabId);
            UpdateDisplayName();
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }

    void UpdateDisplayName()
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = "Xentios"
        }, result => {
            Debug.Log("The player's display name is now: " + result.DisplayName);
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        UpdateDisplayName();
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
}