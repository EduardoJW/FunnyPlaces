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
    public string baseGameplayScene = "MainTown";
    string currentGameplayScene = "MainTown";
    

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
        
        networkRoomManagerScript.ServerChangeScene(currentGameplayScene);
        
    }

    public void togTutorial() 
    {
        networkRoomManagerScript.TogTutorial();
        if (currentGameplayScene == "TutorialMovimentacao")
        {
            currentGameplayScene = "MainTown";
        }
        else 
        {
            currentGameplayScene = "TutorialMovimentacao";
        }
    }
    public void togReconhecimento()
    {
        networkRoomManagerScript.TogTutorial();
        if (currentGameplayScene == "TutorialReconhecimento")
        {
            currentGameplayScene = "MainTown";
        }
        else
        {
            currentGameplayScene = "TutorialReconhecimento";
        }
    }


    public class PreSet
    {
        public string p1Hotel;
        public string p2Hotel;
        public string p3Hotel;
        public string p1Item1;
        public string p1Item2;
        public string p1Item3;
        public string p2Item1;
        public string p2Item2;
        public string p2Item3;
        public string p3Item1;
        public string p3Item2;
        public string p3Item3;
        public bool p1CalcularCompra;
        public bool p2CalcularCompra;
        public bool p3CalcularCompra;
        public int p1WorkingMemoryDigits;
        public int p2WorkingMemoryDigits;
        public int p3WorkingMemoryDigits;
    }

    public void SaveGameplayConfigurationSettings() 
        {
        PreSet preSet = new PreSet();
        preSet.p1Hotel = p1HotelCombobox.options[p1HotelCombobox.value].text;
        preSet.p2Hotel = p2HotelCombobox.options[p2HotelCombobox.value].text;
        preSet.p3Hotel = p3HotelCombobox.options[p3HotelCombobox.value].text;
        preSet.p1Item1 = p1Item1Combobox.options[p1Item1Combobox.value].text;
        preSet.p1Item2 = p1Item2Combobox.options[p1Item2Combobox.value].text;
        preSet.p1Item3 = p1Item3Combobox.options[p1Item3Combobox.value].text;
        preSet.p2Item1 = p1Item1Combobox.options[p2Item1Combobox.value].text;
        preSet.p2Item2 = p1Item2Combobox.options[p2Item2Combobox.value].text;
        preSet.p2Item3 = p1Item3Combobox.options[p2Item3Combobox.value].text;
        preSet.p3Item1 = p1Item1Combobox.options[p3Item1Combobox.value].text;
        preSet.p3Item2 = p1Item2Combobox.options[p3Item2Combobox.value].text;
        preSet.p3Item3 = p1Item3Combobox.options[p3Item3Combobox.value].text;
        preSet.p1CalcularCompra = true;
        preSet.p2CalcularCompra = true;
        preSet.p3CalcularCompra = true;
        preSet.p1WorkingMemoryDigits = int.Parse(p1WMDigitsCombobox.options[p1WMDigitsCombobox.value].text);
        preSet.p2WorkingMemoryDigits = int.Parse(p2WMDigitsCombobox.options[p2WMDigitsCombobox.value].text);
        preSet.p3WorkingMemoryDigits = int.Parse(p3WMDigitsCombobox.options[p3WMDigitsCombobox.value].text);

        string json = JsonUtility.ToJson(preSet);

        /*
        string all = @"{ ""preSet"" : [ {" + @"""p1Hotel"":" + p1Hotel + "," + @"""p2Hotel"":" + p2Hotel + "," + @"""p3Hotel"":" + p3Hotel + ",";
        all += @"""p1Item1"":" + p1Item1 + "," + @"""p1Item2"":" + p1Item2 + "," + @"""p1Item3"":" + p1Item3 + ",";
        all += @"""p2Item1"":" + p2Item1 + "," + @"""p2Item2"":" + p2Item2 + "," + @"""p2Item3"":" + p2Item3 + ",";
        all += @"""p3Item1"":" + p3Item1 + "," + @"""p3Item2"":" + p3Item2 + "," + @"""p3Item3"":" + p3Item3 + ",";
        all += @"""p1CalcularCompra"":" + p1CalcularCompra + "," + @"""p2CalcularCompra"":" + p2CalcularCompra + "," + @"""p3CalcularCompra"":" + p3CalcularCompra + ",";
        all += @"""p1WorkingMemoryDigits"":" + p1WorkingMemoryDigits + "," + @"""p2WorkingMemoryDigits"":" + p2WorkingMemoryDigits + "," + @"""p3WorkingMemoryDigits"":" + p3WorkingMemoryDigits;
        all += "} ] }";*/

        string path = "D:/GameDevGeral/FunnyPlacesPreSets/PreSet0.json";
        string basepath = "D:/GameDevGeral/FunnyPlacesPreSets/PreSet";
        int numfile = 0;
        string pathnum;
        while (System.IO.File.Exists(path))
        {
            numfile++;
            pathnum = numfile.ToString();
            path = basepath + pathnum + ".json";
        }
        System.IO.File.WriteAllText(path, json);

    }

    public void OpenPreSet()
    {
        string path = "D:/GameDevGeral/FunnyPlacesPreSets/PreSet0.json";
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            PreSet loadPreset = JsonUtility.FromJson<PreSet>(json);
            Debug.Log(loadPreset.p1Hotel);
            
        }
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
