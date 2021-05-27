using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;


public class ServerLobbyControler : MonoBehaviour
{
    //[SerializeField] private NetworkRoomManager RoomManager;
    [SerializeField] private Button startGameButton;
    [SerializeField] private GameObject playerOptionsMonitor;
    private GameObject roomMananer;
    

    [Header("Gameplay Options Elements")]
    public TMP_Dropdown p1HotelCombobox;
    public TMP_Dropdown p2HotelCombobox;
    public TMP_Dropdown p3HotelCombobox;
    public TMP_Dropdown p1Item1Combobox;
    public TMP_Dropdown p1Item2Combobox;
    public TMP_Dropdown p1Item3Combobox;
    public TMP_Dropdown p2Item1Combobox;
    public TMP_Dropdown p2Item2Combobox;
    public TMP_Dropdown p2Item3Combobox;
    public TMP_Dropdown p3Item1Combobox;
    public TMP_Dropdown p3Item2Combobox;
    public TMP_Dropdown p3Item3Combobox;
    public Toggle p1CalcularCompraToggle;
    public Toggle p2CalcularCompraToggle;
    public Toggle p3CalcularCompraToggle;
    public TMP_Dropdown p1WMDigitsCombobox;
    public TMP_Dropdown p2WMDigitsCombobox;
    public TMP_Dropdown p3WMDigitsCombobox;
    


    //alteraçao para tentar override no spawn
    //private NetworkRoomManager networkRoomManagerScript;
    private NetworkRoomManagerExtTest  networkRoomManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        //alteracao para tentar override no spawn
        //networkRoomManagerScript =  GameObject.Find("RoomManager").GetComponent<NetworkRoomManagerExtTest>();
        networkRoomManagerScript =  GameObject.Find("RoomManagerExtTest").GetComponent<NetworkRoomManagerExtTest>();    
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
        StoreGameplayConfigurationOnDummyObject();
        GameObject[] roomPlayerArray = GameObject.FindGameObjectsWithTag("RoomPlayer");
        foreach (GameObject roomPlayer in roomPlayerArray){
            roomPlayer.GetComponent<RoomPlayerUIControler>().RpcDisableUI();
        }
        
        networkRoomManagerScript.ServerChangeScene("MainTown");
        
    }



    public void StoreGameplayConfigurationOnDummyObject(){
        PlayerChoiceTracking playerMonitorScript = GameObject.Find("DummyObjectPlayerOptions").GetComponent<PlayerChoiceTracking>();
        playerMonitorScript.p1Hotel = p1HotelCombobox.options[p1HotelCombobox.value].text;
        playerMonitorScript.p2Hotel = p2HotelCombobox.options[p2HotelCombobox.value].text;
        playerMonitorScript.p3Hotel = p3HotelCombobox.options[p3HotelCombobox.value].text;
        playerMonitorScript.p1Item1 = p1Item1Combobox.options[p1Item1Combobox.value].text;
        playerMonitorScript.p1Item2 = p1Item2Combobox.options[p1Item2Combobox.value].text;
        playerMonitorScript.p1Item3 = p1Item3Combobox.options[p1Item3Combobox.value].text;
        playerMonitorScript.p2Item1 = p1Item1Combobox.options[p2Item1Combobox.value].text;
        playerMonitorScript.p2Item2 = p1Item2Combobox.options[p2Item2Combobox.value].text;
        playerMonitorScript.p2Item3 = p1Item3Combobox.options[p2Item3Combobox.value].text;
        playerMonitorScript.p3Item1 = p1Item1Combobox.options[p3Item1Combobox.value].text;
        playerMonitorScript.p3Item2 = p1Item2Combobox.options[p3Item2Combobox.value].text;
        playerMonitorScript.p3Item3 = p1Item3Combobox.options[p3Item3Combobox.value].text;
        playerMonitorScript.p1CalcularCompra = p1CalcularCompraToggle.isOn;
        playerMonitorScript.p2CalcularCompra = p2CalcularCompraToggle.isOn;
        playerMonitorScript.p3CalcularCompra = p3CalcularCompraToggle.isOn;
        playerMonitorScript.p1WorkingMemoryDigits = int.Parse(p1WMDigitsCombobox.options[p1WMDigitsCombobox.value].text);
        playerMonitorScript.p2WorkingMemoryDigits = int.Parse(p2WMDigitsCombobox.options[p2WMDigitsCombobox.value].text);
        playerMonitorScript.p3WorkingMemoryDigits = int.Parse(p3WMDigitsCombobox.options[p3WMDigitsCombobox.value].text);

        playerMonitorScript.RpcSetGameplayOptionsVariables(playerMonitorScript.p1Hotel,playerMonitorScript.p2Hotel,playerMonitorScript.p3Hotel,
        playerMonitorScript.p1Item1,playerMonitorScript.p1Item2,playerMonitorScript.p1Item3,
        playerMonitorScript.p2Item1,playerMonitorScript.p2Item2,playerMonitorScript.p2Item3,
        playerMonitorScript.p3Item1,playerMonitorScript.p3Item2,playerMonitorScript.p3Item3,
        playerMonitorScript.p1CalcularCompra,playerMonitorScript.p1CalcularCompra,playerMonitorScript.p3CalcularCompra,
        playerMonitorScript.p1WorkingMemoryDigits,playerMonitorScript.p2WorkingMemoryDigits,playerMonitorScript.p3WorkingMemoryDigits);

        Debug.Log(p1HotelCombobox.value);








    }
    
}
