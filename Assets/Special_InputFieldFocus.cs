using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TNRD.Autohook;

using PlayFab;
using PlayFab.ClientModels;

public class Special_InputFieldFocus : MonoBehaviour
{

    [SerializeField,AutoHook]
    private TMP_InputField input;

    
    void Start()
    {
        var name = PlayerPrefs.GetString("DisplayName", "Enter A New Name");

        input.text = name;
        input.ActivateInputField();
        input.Select();
    }

    public void UpdateDisplayName()
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = input.text
        }, result =>
        {
            Debug.Log("The player's display name is now: " + result.DisplayName);            
            PlayerPrefs.SetString("DisplayName", result.DisplayName);
        }, error => Debug.LogError(error.GenerateErrorReport())); 
    }


}
