using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PlayerNameInput : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_InputField playerNameInputField = null;
    [SerializeField] private Button continueButton = null;
    

    public static string DisplayName {get; private set;}
    private const string PlayerPrefsNameKey = "PlayerName";

    private void Start() => SetUpInputField();

    private void SetUpInputField(){
        if (!PlayerPrefs.HasKey(PlayerPrefsNameKey)) {return;}
        
        string previousGamePlayerName = PlayerPrefs.GetString(PlayerPrefsNameKey);
        playerNameInputField.text = previousGamePlayerName;
        

        EnableContinueButtonIfNameValid();

    }
    
    public void EnableContinueButtonIfNameValid(){
        if (!string.IsNullOrEmpty(playerNameInputField.text)){
            continueButton.interactable = true;
        }else{
            continueButton.interactable = false;
        }
        
        
    }

    public void SavePlayerName(){
        DisplayName = playerNameInputField.text;
        PlayerPrefs.SetString(PlayerPrefsNameKey,DisplayName);
    }
    
    
}
