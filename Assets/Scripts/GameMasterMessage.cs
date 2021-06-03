using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class GameMasterMessage : MonoBehaviour
{
    public GameObject gameMasterMessageInput;
    public TMP_Dropdown playerToSendMessage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetPlayerRefferenceSendMessage(){
        PlayerControler playerControler;
        foreach (GameObject gamePlayer in GameObject.FindGameObjectsWithTag("Player")){
            playerControler = gamePlayer.GetComponent<PlayerControler>();
            Debug.Log ("Player ID: " + playerControler.GetComponent<PlayerControler>().playerIdNumber);
            Debug.Log("Options: " + playerToSendMessage.value);
            if (playerControler.GetComponent<PlayerControler>().playerIdNumber == playerToSendMessage.value + 1 ){
                gamePlayer.GetComponent<ServerMessageListener>().serverMessageFromPsychologist = gameMasterMessageInput.GetComponent<TMP_InputField>().text;
            }
        }
    }




}
