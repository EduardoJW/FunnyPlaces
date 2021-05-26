using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


public class ServerLobbyControler : MonoBehaviour
{
    //[SerializeField] private NetworkRoomManager RoomManager;
    [SerializeField] private Button startGameButton;
    [SerializeField] private GameObject playerOptionsMonitor;
    private GameObject roomMananer;
    private NetworkRoomManager networkRoomManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        networkRoomManagerScript =  GameObject.Find("RoomManager").GetComponent<NetworkRoomManager>();    
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayersReady();
    }

    public void CheckPlayersReady(){
        PlayerChoiceTracking playerChoicesScript = playerOptionsMonitor.GetComponent<PlayerChoiceTracking>();

        if (playerChoicesScript.player1Ready && playerChoicesScript.player2Ready && playerChoicesScript.player3Ready){
            startGameButton.interactable = true;
        }else{
            startGameButton.interactable = false;
        }
    }

    public void startGame(){
        GameObject[] roomPlayerArray = GameObject.FindGameObjectsWithTag("RoomPlayer");
        foreach (GameObject roomPlayer in roomPlayerArray){
            roomPlayer.GetComponent<RoomPlayerUIControler>().RpcDisableUI();
        }
        
        networkRoomManagerScript.ServerChangeScene("MainTown");
        
    }



    
}
