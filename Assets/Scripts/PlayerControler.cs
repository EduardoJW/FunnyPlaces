using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerControler : NetworkBehaviour
{
    public static Dictionary<string, string> hotelDecorationMapping = new Dictionary<string, string>{
        {"SonoBom","quadro colorido na parede."},
        {"DormeBem","quadro com o homem de chapéu na parede."},
        {"RoncoAlto","quadro com o casal de guarda-chuva na parede."}
    };

    public static Dictionary<string, string> itemDicaMapping = new Dictionary<string, string>{
        {"Cachorro Quente","Você pode encontrar em barraquinhas espalhadas pela cidade."},
        {"Bolo","Eu ouvi dizer que a padaria atrás do shopping vende os melhores bolos."},
        {"Flores","Há uma barraquinha de flores no shopping."},
        {"Frutas","Um supermercado no shopping vende deliciosas frutas orgânicas!"},
        {"Batata Frita","Na lojinha de um posto de gasolina há muitas delas."},
        {"Sorvete","No posto de gasolina há um freezer cheio, mas cuidado! Eles derretem rápidamente!"},
        {"Croissant","O padeiro que trabalha no shopping é Francês."},
        {"Pão a Metro","Aquela padaria do shopping vende de tudo"},
        {"Refrigerante","Vai no posto BR! Mas cuidado! Refrigerante quente não desce!!"},
        {"Pipoca","A lanchonete de fast food no shopping agora tambem vende pipoca!"}
    };




    [Header("Barra de Energia")]
    public Slider energyBar = null;
    public float energyBarConsumeRate = 0.0f;


    private Animator animatorController;
    [Header("Camera")]
    public GameObject playerCamera;
    private Camera cam;

    public GameObject self;
    public GameObject roomPlayer;
    public GameObject Airplane;
    public Camera AirplaneCam;
    public bool isFlying;
    public int charId;
    private GameObject clickedObject;
    [Header("Interaction Variables")]
    public float interactDistance;
    private GameObject playercharacterchoice;
    public static string[,] characterModelMapping = new string[9, 2] { { "1", "Character_Father_01" }, { "2", "Character_SchoolBoy_01" }, { "3", "Character_Mother_02" }, { "4", "Character_Father_02" }, { "5", "Character_Daughter_01" }, { "6", "Character_Mother_01" }, { "7", "Character_SchoolGirl_01" }, { "8", "Character_ShopKeeper_01" }, { "9", "Character_Son_01" } };
    public int playerIdNumber = 0;
    public float timeLastInteraction = 0.0f;
    public float minimumTimeBetweenInteractions = 20.0f;


    [Header("Wallet")]
    public Text walletMoneyDisplay;
    public int walletMoneyValue = 2000;




    public string[] localPlayerMissionItems = new string[3];
    public string localPlayerHotel;
    public bool localPlayerCalcularCompra;
    public int localPlayerWorkingMemoryDigits;




    private string wellcomeString = "Bem Vindo a Lugares Divertidos!!!\n" +
     "A festa de 80 anos do seu amigo vai ser um arraso! \n" +
    "Mas para isso você precisa encontrar alguns itens e " +
    "trazê-los para a festa e trazê-los de volta ao seu hotel. \n" +
    "Aqui está sua Lista:\n\n" +
    "- 1 {0}\n" +
    "- 1 {1}\n" +
    "- 1 {2}\n\n" +
    "Boa Sorte! Ah! E lembre-se seu hotel é o Hotel {3}, aquele com o {4}!";


    [Header("Movement")]
    public float rotationSpeed = 100;



    // Start is called before the first frame update
    void Start()
    {

        if (isLocalPlayer) gameObject.transform.Find("HUD").gameObject.SetActive(true);
        animatorController = GetComponent<Animator>();


        if (isLocalPlayer) {
            playerCamera.SetActive(true);
            cam = playerCamera.GetComponent<Camera>();
            playerIdNumber = getCharacterId();
            CmdRegisterPlayerNumberOnServer(playerIdNumber);
            GAUpdate(playerIdNumber);
            switch (playerIdNumber) {
                case 1:
                    AdjustPlayerPos(GameObject.FindGameObjectWithTag("PlayerOptionsContainer").GetComponent<PlayerChoiceTracking>().p1Hotel);
                    CmdChangePlayerCharacterModel(GameObject.FindGameObjectWithTag("PlayerOptionsContainer").GetComponent<PlayerChoiceTracking>().p1CharId);
                    break;
                case 2:
                    AdjustPlayerPos(GameObject.FindGameObjectWithTag("PlayerOptionsContainer").GetComponent<PlayerChoiceTracking>().p2Hotel);
                    CmdChangePlayerCharacterModel(GameObject.FindGameObjectWithTag("PlayerOptionsContainer").GetComponent<PlayerChoiceTracking>().p2CharId);
                    break;
                case 3:
                    AdjustPlayerPos(GameObject.FindGameObjectWithTag("PlayerOptionsContainer").GetComponent<PlayerChoiceTracking>().p3Hotel);
                    CmdChangePlayerCharacterModel(GameObject.FindGameObjectWithTag("PlayerOptionsContainer").GetComponent<PlayerChoiceTracking>().p3CharId);
                    break;

            }
        }



        if (!isServer) {
            DisableServerControls();
        }

        if (isLocalPlayer)
        {
            if (SceneManager.GetActiveScene().name == "MainTown") {
                Airplane = GameObject.Find("WinnerPlane");
                RegisterMissionLocalPlayerMissionItemsGameOptions(playerIdNumber);
                showWellcomePopUp();
                energyBar.value = 1f;
                energyBarConsumeRate = 0.0001f;
            }
            if (SceneManager.GetActiveScene().name == "TutorialMovimentacao")
            {
                RegisterMissionLocalPlayerMissionItemsGameOptions(playerIdNumber);
                showWellcomePopUpTutorial();
                energyBar.value = 1f;
                energyBarConsumeRate = 0;
            }
            if (SceneManager.GetActiveScene().name == "TutorialReconhecimento")
            {
                RegisterMissionLocalPlayerMissionItemsGameOptions(playerIdNumber);
                showWellcomePopUpRecognition();
                energyBar.value = 1f;
                energyBarConsumeRate = 0;
            }
        }






    }

    // Update is called once per frame
    void Update()
    {
        // movement for local player
        if (!isLocalPlayer) return;

        if (Input.GetKeyDown(KeyCode.M))
        {
            CmdStartUpdateFinishedPlayersLocal(self);
        }
        if (!isFlying)
        {
            float x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
            float z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

            transform.Rotate(0, x, 0);

            if (energyBar.value != 0)
            {
                transform.Translate(0, 0, z);

                if (z == 0)
                {
                    animatorController.SetBool("isWalking", false);
                    animatorController.SetBool("isWalkingBackwards", false);
                }

                if (z > 0)
                {
                    animatorController.SetBool("isWalking", true);
                    animatorController.SetBool("isWalkingBackwards", false);
                }
                else if (z < 0)
                {
                    animatorController.SetBool("isWalking", false);
                    animatorController.SetBool("isWalkingBackwards", true);
                }

                if (z != 0)
                {
                    if (SceneManager.GetActiveScene().name == "MainTown")
                    {
                        CmdUpdateMovimento();
                        ConsumeEnergyWalking();
                    }
                    if (SceneManager.GetActiveScene().name == "TutorialMovimentacao")
                    {
                        CmdUpdateMovimentoTutorial();
                    }
                }
            }

            //get lef mouse button click
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("name");
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    clickedObject = hit.collider.gameObject;
                    if (clickedObject.tag == "Pickupable")
                    {
                        gameObject.transform.Find("HUD/CreditCardMachine").gameObject.SetActive(true);
                        if (Vector3.Distance(transform.position, clickedObject.transform.position) <= interactDistance)
                        {
                            Destroy(clickedObject);
                        }
                        else
                        {
                            Debug.Log("O objeto está muito longe!");
                        }
                    }
                    else if (clickedObject.tag == "RefreshmentPoint")
                    {
                        UseRefreshmentPoint(clickedObject.tag);
                    }
                    else if (clickedObject.tag == "NPC_Helper")
                    {
                        ShowHelpMessage(clickedObject);
                    }
                    else if (clickedObject.tag == "Player")
                    {
                        if (Vector3.Distance(transform.position, clickedObject.transform.position) <= interactDistance)
                        {
                            if ((timeLastInteraction == 0.0f) || ((Time.time - timeLastInteraction) > minimumTimeBetweenInteractions))
                            {
                                timeLastInteraction = Time.time;
                                RefreshThroughInteraction();
                            }
                        }
                        else
                        {

                        }

                    }
                }
            }



        }
    }


    [Command]
    public void AdjustPlayerPos(string hotel)
    {
        switch (hotel)
        {
            case ("SonoBom"):
                gameObject.transform.position = new Vector3(86.88f, 0, 39.74f);
                RpcNotifyClientAdjustPlayer(hotel);
                break;
            case ("DormeBem"):
                gameObject.transform.position = new Vector3(57.16f, 0, -78.74f);
                RpcNotifyClientAdjustPlayer(hotel);
                break;
            case ("RoncoAlto"):
                RpcNotifyClientAdjustPlayer(hotel);
                gameObject.transform.position = new Vector3(-29.31f, 0, 75.4f);
                break;


        }
    }

    [ClientRpc]
    public void RpcNotifyClientAdjustPlayer(string hotel)
    {
        switch (hotel)
        {
            case ("SonoBom"):
                gameObject.transform.position = new Vector3(86.88f, 0, 39.74f);
                break;
            case ("DormeBem"):
                gameObject.transform.position = new Vector3(57.16f, 0, -78.74f);
                break;
            case ("RoncoAlto"):
                gameObject.transform.position = new Vector3(-29.31f, 0, 75.4f);
                break;


        }
    }

    [Command]
    public void CmdUpdatePlayerFinishedServer(int place, GameObject self)
    {
        GameObject server = GameObject.Find("RoomManagerExtTest");
        switch (place)
        {
            case 1:
                server.GetComponent<NetworkRoomManagerExtTest>().firstToFinish = self;
                break;
            case 2:
                server.GetComponent<NetworkRoomManagerExtTest>().secondToFinish = self;
                break;
            case 3:
                server.GetComponent<NetworkRoomManagerExtTest>().thirdToFinish = self;
                break;
        }

    }

    [Command]
    public void CmdStartUpdateFinishedPlayersLocal(GameObject self)
    {
        RpcUpdateFinishedPlayersLocal(self);
    }

    [Command]
    public void CmdEndGameRequest() { 
    GameObject[] arrayGO = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject gameobject in arrayGO)
            {
                NetworkIdentity nId = gameobject.GetComponent<NetworkIdentity>();
                EndGameProcedure(nId.connectionToClient, gameobject);
            }
    }

    public void AirPlaneCameraChange()
    {
        Airplane.transform.Find("WinnerPlane/Main_Camera").gameObject.SetActive(true);
        cam = Airplane.GetComponent<Camera>();
        isFlying = true;
        Airplane.transform.Find("WinnerPlane").GetComponent<Plane_Fly>().isFlying = true;
    }


    [Command]
    public void CmdTogetherEnd()
    {
        GameObject[] arrayGO = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject gameobject in arrayGO)
        {
            NetworkIdentity nId = gameobject.GetComponent<NetworkIdentity>();
            TogetherEndProcedure(nId.connectionToClient, gameobject);
        }
    }

    

    [TargetRpc]
    public void TogetherEndProcedure(NetworkConnection client, GameObject self) 
    {
        self.transform.Find("HUD/GameEndMessageWindow").gameObject.SetActive(false);
        self.transform.GetComponent<PlayerControler>().AirPlaneCameraChange();
    }

    [TargetRpc]
    public void EndGameProcedure(NetworkConnection client, GameObject self) 
    {
        self.transform.Find("HUD/GameEndMessageWindow").gameObject.SetActive(true);
        GameObject server = GameObject.Find("RoomManagerExtTest");
        string textGeneric = "Parabéns você conseguiu ajudar a preparar"
                + " a festa do seu amigo.";
        if (server.GetComponent<NetworkRoomManagerExtTest>().firstToFinish == self)
        {
            self.transform.Find("HUD/GameEndMessageWindow/Panel_PopUpWindow/Panel_Content/GameEndMessage").gameObject.GetComponent<Text>().text = textGeneric + "\n E você foi o primeiro"
                + " a completar tudo, por isso você ganha um prêmio especial.\nUma viagem de avião pela cidade. Você quer ir sozinho ou quer levar seus amigos junto?";
            self.transform.Find("HUD/GameEndMessageWindow/Panel_PopUpWindow/Panel_Content/SoloButton").gameObject.SetActive(true);
            self.transform.Find("HUD/GameEndMessageWindow/Panel_PopUpWindow/Panel_Content/EveryoneButton").gameObject.SetActive(true);
        }
        else 
        {
            self.transform.Find("HUD/GameEndMessageWindow/Panel_PopUpWindow/Panel_Content/GameEndMessage").gameObject.GetComponent<Text>().text = textGeneric + "\n E quem sabe uma surpresa não te aguarda?";
        }
    }

    [ClientRpc]
    public void RpcUpdateFinishedPlayersLocal(GameObject self) 
    {
        GameObject server = GameObject.Find("RoomManagerExtTest");
        if (server.GetComponent<NetworkRoomManagerExtTest>().firstToFinish && server.GetComponent<NetworkRoomManagerExtTest>().firstToFinish != self) 
        {
            if (server.GetComponent<NetworkRoomManagerExtTest>().secondToFinish && server.GetComponent<NetworkRoomManagerExtTest>().secondToFinish != self)
            {
                if (server.GetComponent<NetworkRoomManagerExtTest>().thirdToFinish && server.GetComponent<NetworkRoomManagerExtTest>().thirdToFinish != self)
                {
                    Debug.Log("Player Already accounted for");
                }
                else if(!server.GetComponent<NetworkRoomManagerExtTest>().thirdToFinish)
                {
                    server.GetComponent<NetworkRoomManagerExtTest>().thirdToFinish = self;
                    CmdUpdatePlayerFinishedServer(3,self);
                    CmdEndGameRequest();
                }
            }
            else if (!server.GetComponent<NetworkRoomManagerExtTest>().secondToFinish)
            {
                server.GetComponent<NetworkRoomManagerExtTest>().secondToFinish = self;
                CmdUpdatePlayerFinishedServer(2,self);
            }
        }
        else if (!server.GetComponent<NetworkRoomManagerExtTest>().firstToFinish)
        {
            server.GetComponent<NetworkRoomManagerExtTest>().firstToFinish = self;
            CmdUpdatePlayerFinishedServer(1,self);
        }
    }

    [Command]
    public void CmdChangePlayerCharacterModel(int charNumber) {
        foreach (Transform child in gameObject.transform) {
            if (child.CompareTag("PlayerCharacterModel")) {
                child.gameObject.SetActive(false);
                if (child.gameObject.name == characterModelMapping[charNumber, 1]) {
                    child.gameObject.SetActive(true);
                }

            }

        }
        if (SceneManager.GetActiveScene().name == "MainTown")
        {
            RpcNotifyClientsPlayerCharacterModelChange(charNumber);
        }
        if (SceneManager.GetActiveScene().name == "TutorialMovimentacao")
        {
            RpcNotifyClientsPlayerCharacterModelChange(charNumber);
        }
        if (SceneManager.GetActiveScene().name == "TutorialReconhecimento")
        {
            CmdChangeNpcCharacterModels(charNumber);
            RpcNotifyClientsPlayerCharacterModelChange(charNumber);
            RpcNotifyClientsPlayerRecognition();
        }

    }

    [Command]
    public void GAUpdate(int charNumber) 
    {
        GameObject[] arrayGO = GameObject.FindGameObjectsWithTag("RoomPlayer");
        foreach (GameObject gameobject in arrayGO)
        {
            charId = charNumber;
            if (gameobject.GetComponent<NetworkRoomPlayerExtTest>().index == (charNumber-1))
            {
                roomPlayer = gameobject;
            }
        }
        GAUpdatePlayer(charNumber);
    }
    [ClientRpc]
    public void GAUpdatePlayer(int charNumber)
    {
        GameObject[] arrayGO = GameObject.FindGameObjectsWithTag("RoomPlayer");
        foreach (GameObject gameobject in arrayGO)
        {
            charId = charNumber;
            if (gameobject.GetComponent<NetworkRoomPlayerExtTest>().index == (charNumber-1))
            {
                roomPlayer = gameobject;
            }
        }
    }

    public void CmdChangeNpcCharacterModels(int charNumber)
    {
            switch (playerIdNumber)
            {
                case 1:
                    foreach (Transform child in GameObject.Find("Npc_Player1").transform)
                    {
                        if (child.CompareTag("Player1_Recognition_Model"))
                        {
                            child.gameObject.SetActive(false);
                            if (child.gameObject.name == characterModelMapping[charNumber, 1])
                            {
                                child.gameObject.SetActive(true);
                                GameObject.Find("RoomManagerExtTest").GetComponent<NetworkRoomManagerExtTest>().RecogPlayer1 = charNumber;
                                RpcNotifyClientsNPCValueChange(charNumber, playerIdNumber);
                            }

                        }

                    }
                    break;
                case 2:
                    foreach (Transform child in GameObject.Find("Npc_Player2").transform)
                    {
                        if (child.CompareTag("Player2_Recognition_Model"))
                        {
                            child.gameObject.SetActive(false);
                            if (child.gameObject.name == characterModelMapping[charNumber, 1])
                            {
                                child.gameObject.SetActive(true);
                                GameObject.Find("RoomManagerExtTest").GetComponent<NetworkRoomManagerExtTest>().RecogPlayer2 = charNumber;
                                RpcNotifyClientsNPCValueChange(charNumber, playerIdNumber);
                            }

                        }

                    }
                    break;
                case 3:
                    foreach (Transform child in GameObject.Find("Npc_Player3").transform)
                    {
                        if (child.CompareTag("Player3_Recognition_Model"))
                        {
                            child.gameObject.SetActive(false);
                            if (child.gameObject.name == characterModelMapping[charNumber, 1])
                            {
                                child.gameObject.SetActive(true);
                                GameObject.Find("RoomManagerExtTest").GetComponent<NetworkRoomManagerExtTest>().RecogPlayer3 = charNumber;
                                RpcNotifyClientsNPCValueChange(charNumber,playerIdNumber);
                            }

                        }

                    }
                    break;

           }
    }


    [ClientRpc]
    public void RpcNotifyClientsNPCValueChange(int charNumber, int player) 
    {
        switch (player)
        {
            case 1:

                GameObject.Find("RoomManagerExtTest").GetComponent<NetworkRoomManagerExtTest>().RecogPlayer1 = charNumber;
                break;
            case 2:
                GameObject.Find("RoomManagerExtTest").GetComponent<NetworkRoomManagerExtTest>().RecogPlayer2 = charNumber;
                break;
            case 3:
                GameObject.Find("RoomManagerExtTest").GetComponent<NetworkRoomManagerExtTest>().RecogPlayer3 = charNumber;
                break;

        }
    }

    [ClientRpc]
    public void RpcNotifyClientsPlayerCharacterModelChange(int charNumber){
        foreach(Transform child in gameObject.transform){
            if (child.CompareTag("PlayerCharacterModel")){
                child.gameObject.SetActive(false);
                if (child.gameObject.name == characterModelMapping[charNumber,1]){
                    child.gameObject.SetActive(true);
                }
                
            }

        }
    }

    [ClientRpc]
    public void RpcNotifyClientsPlayerRecognition()
    {

                int charNumber = GameObject.Find("RoomManagerExtTest").GetComponent<NetworkRoomManagerExtTest>().RecogPlayer1;
        foreach (Transform child in GameObject.Find("Npc_Player1").transform)
                {
                    if (child.CompareTag("Player1_Recognition_Model"))
                    {
                        child.gameObject.SetActive(false);
                        if (child.gameObject.name == characterModelMapping[charNumber, 1])
                        {
                            child.gameObject.SetActive(true);

                        }

                    }

                }
                charNumber = GameObject.Find("RoomManagerExtTest").GetComponent<NetworkRoomManagerExtTest>().RecogPlayer2;
                foreach (Transform child in GameObject.Find("Npc_Player2").transform)
                {
                    if (child.CompareTag("Player2_Recognition_Model"))
                    {
                        child.gameObject.SetActive(false);
                        if (child.gameObject.name == characterModelMapping[charNumber, 1])
                        {
                            child.gameObject.SetActive(true);
                        }

                    }

                }
                charNumber = GameObject.Find("RoomManagerExtTest").GetComponent<NetworkRoomManagerExtTest>().RecogPlayer3;
                foreach (Transform child in GameObject.Find("Npc_Player3").transform)
                {
                    if (child.CompareTag("Player3_Recognition_Model"))
                    {
                        child.gameObject.SetActive(false);
                        if (child.gameObject.name == characterModelMapping[charNumber, 1])
                        {
                            child.gameObject.SetActive(true);
                        }

                    }

                }
    }

    public int getCharacterId(){
        PlayerChoiceTracking playerChoices = GameObject.FindGameObjectWithTag("PlayerOptionsContainer").GetComponent<PlayerChoiceTracking>();
        return playerChoices.localPlayerPlayerNumber;
    }

    public void DisableServerControls(){
        GameObject sm = GameObject.Find("ServerManager");
        if (sm){
            sm.SetActive(false);
        }
        
    }

    public void RegisterMissionLocalPlayerMissionItemsGameOptions(int playerId){
        PlayerChoiceTracking playerChoices = GameObject.FindGameObjectWithTag("PlayerOptionsContainer").GetComponent<PlayerChoiceTracking>();
        switch (playerId){
            case 1:
                localPlayerMissionItems[0]= playerChoices.p1Item1;
                localPlayerMissionItems[1]= playerChoices.p1Item2;
                localPlayerMissionItems[2]= playerChoices.p1Item3;
                localPlayerHotel = playerChoices.p1Hotel;
                localPlayerCalcularCompra = playerChoices.p1CalcularCompra;
                localPlayerWorkingMemoryDigits = playerChoices.p1WorkingMemoryDigits;
                break;
            case 2:
                localPlayerMissionItems[0]= playerChoices.p2Item1;
                localPlayerMissionItems[1]= playerChoices.p2Item2;
                localPlayerMissionItems[2]= playerChoices.p2Item3;
                localPlayerHotel = playerChoices.p2Hotel;
                localPlayerCalcularCompra = playerChoices.p2CalcularCompra;
                localPlayerWorkingMemoryDigits = playerChoices.p2WorkingMemoryDigits;
                break;
            case 3:
                localPlayerMissionItems[0]= playerChoices.p3Item1;
                localPlayerMissionItems[1]= playerChoices.p3Item2;
                localPlayerMissionItems[2]= playerChoices.p3Item3;
                localPlayerHotel = playerChoices.p3Hotel;
                localPlayerCalcularCompra = playerChoices.p3CalcularCompra;
                localPlayerWorkingMemoryDigits = playerChoices.p3WorkingMemoryDigits;
                break;
        
        }

    }

    public void showWellcomePopUp(){
                                   
        gameObject.transform.Find("HUD/WellcomeMessagePopUp").gameObject.SetActive(true);
        gameObject.transform.Find("HUD/WellcomeMessagePopUp/Panel_PopUpWindow/Panel_Content/WellcomeMessage").gameObject.GetComponent<Text>().text=
        string.Format(wellcomeString,localPlayerMissionItems[0],localPlayerMissionItems[1],localPlayerMissionItems[2],localPlayerHotel,getHotelDecorationTip(localPlayerHotel));
    }

    public void showWellcomePopUpTutorial()
    {

        gameObject.transform.Find("HUD/WellcomeMessagePopUp").gameObject.SetActive(true);
        gameObject.transform.Find("HUD/WellcomeMessagePopUp/Panel_PopUpWindow/Panel_Content/WellcomeMessage").gameObject.GetComponent<Text>().text = "Bem vindo ao tutorial de movimentação"
            +"\nNão precisa se preocupar com as suas tarefas por enquanto"
            +"\nAqui você é livre para se mover e aprender os comandos sem pressa";
    }

    public void showWellcomePopUpRecognition()
    {

        gameObject.transform.Find("HUD/WellcomeMessagePopUp").gameObject.SetActive(true);
        gameObject.transform.Find("HUD/WellcomeMessagePopUp/Panel_PopUpWindow/Panel_Content/WellcomeMessage").gameObject.GetComponent<Text>().text = "Bem vindo ao tutorial de reconhecimento"
            +"\nTente encontrar os seus amigos entre os moradores";
    }


    public string getHotelDecorationTip(string hotelName){
        return hotelDecorationMapping[hotelName];
    }

    
    public void showItemListPanelPopUp(){                                   
        if (isLocalPlayer){
            CmdUpdateAcionouListaDeItens();
            gameObject.transform.Find("HUD/ItemListPopUp").gameObject.SetActive(true);
            Toggle item1 = gameObject.transform.Find("HUD/ItemListPopUp/Panel_PopUpWindow/Item1FoundToggle").gameObject.GetComponent<Toggle>();
            Toggle item2 = gameObject.transform.Find("HUD/ItemListPopUp/Panel_PopUpWindow/Item2FoundToggle").gameObject.GetComponent<Toggle>();
            Toggle item3 = gameObject.transform.Find("HUD/ItemListPopUp/Panel_PopUpWindow/Item3FoundToggle").gameObject.GetComponent<Toggle>();
            
            item1.transform.Find("Label").GetComponent<Text>().text = localPlayerMissionItems[0];
            item2.transform.Find("Label").GetComponent<Text>().text = localPlayerMissionItems[1];
            item3.transform.Find("Label").GetComponent<Text>().text = localPlayerMissionItems[2];   
        }
    }

    public void ShowItemTipPanelPopUp(int itemNumber){                                   
        if (isLocalPlayer){
            gameObject.transform.Find("HUD/ItemTipPopUp").gameObject.SetActive(true);
            gameObject.transform.Find("HUD/ItemTipPopUp/Panel_PopUpWindow/Panel_Content/Text_Description").gameObject.GetComponent<Text>().text =  itemDicaMapping[localPlayerMissionItems[itemNumber]];
        }
    }

    public void ConsumeEnergyWalking(){
        energyBar.value -=energyBarConsumeRate;
        if (energyBar.value <= 0) 
        {
            energyBar.value = 0;
            CmdUpdateQuantidadeDeVezesQueAEnergiaSeEsgotou();
        }
    }

    public void UseRefreshmentPoint(string clickedObjectTag){
        if (clickedObjectTag == "RefreshmentPoint"){
            CmdUpdateQuantidadeDeVezesQueTentouSeHidratar();
            if (ReduceWalletMoney(100)){
                CmdUpdateQuantidadeDeVezesQueSeHidratou();
                CmdUpdateValorDaBarraDeEnergiaQuandoSeHidratou(energyBar.value);
                if (energyBar.value >= 0.7f){
                    energyBar.value = 1.0f;
                }else{
                    energyBar.value += 0.3f;
                }

            }
            
        }
    }

    public void RefreshThroughInteraction(){
        CmdUpdateQuantidadeDeVezesQueInteragiu();
        if (energyBar.value >= 0.5f){
            energyBar.value = 1.0f;
        }else{
            energyBar.value += 0.5f;
        }


    }

    public void ShowHelpMessage(GameObject clickedObject)
    {
        gameObject.transform.Find("HUD/NPC_Hint").gameObject.GetComponent<TMP_Text>().text = clickedObject.transform.GetComponent<NPC_Dicas>().dica;
        gameObject.transform.Find("HUD/NPC_Hint").gameObject.SetActive(true);
        gameObject.transform.Find("HUD/NPC_Hint").gameObject.GetComponent<Animation>().Play("ServerMessageFading");
    }

    public void WalletInsuficientMoney()
    {
        gameObject.transform.Find("HUD/WalletInsuficientMoney").gameObject.GetComponent<TMP_Text>().text = "Infelizmente você não tem dinheiro suficiente para comprar isso";
        gameObject.transform.Find("HUD/WalletInsuficientMoney").gameObject.SetActive(true);
        gameObject.transform.Find("HUD/WalletInsuficientMoney").gameObject.GetComponent<Animation>().Play("ServerMessageFading");
    }

    public bool ReduceWalletMoney(int itemOrActionPrice){
        if (walletMoneyValue -itemOrActionPrice>=0){
            walletMoneyValue -= itemOrActionPrice;
            CmdUpdateDinheiroTotalGasto(itemOrActionPrice);
            CmdUpdatetotalCreditos(itemOrActionPrice);
            walletMoneyDisplay.text = walletMoneyValue.ToString();
            Debug.Log("walletMoneyValue.ToString()");
            return true;
        }
        WalletInsuficientMoney();
        return false;
    }

    [Command]
    public void CmdRegisterPlayerNumberOnServer(int IdNumber){
        playerIdNumber = IdNumber;
    }

    [Command]
    public void CmdUpdateMovimento(){
        roomPlayer.GetComponent<GameAnalytics>().M1_DistânciaTotalPercorrida++;      
    }
    [Command]
    public void CmdUpdateBotadoDeJogadorMaisProximo()
    {
        roomPlayer.GetComponent<GameAnalytics>().M12_AcionamentosDoBotaoDeJogadorMaisProximo++;
    }
    [Command]
    public void CmdUpdateLevouAmigosSobrevoo()
    {
        roomPlayer.GetComponent<GameAnalytics>().M22_OpcaoDeLevarOsAmigosAoSobrevoo = true;
    }

    [Command]
    public void CmdUpdateMovimentoTutorial()
    {
        roomPlayer.GetComponent<GameAnalytics>().M5_DistânciaPercorridaDuranteOTutorial++;
        //GameObject.Find("GameAnalyticsTracker").GetComponent<GameAnalytics>().M5_DistânciaPercorridaDuranteOTutorial++;

    }

    [Command]
    public void CmdUpdateDinheiroTotalGasto(float valor)
    {
        roomPlayer.GetComponent<GameAnalytics>().M2_TotalDeDinheiroGasto += valor;
    }

    [Command]
    public void CmdUpdatetotalCreditos(float valor)
    {
        roomPlayer.GetComponent<GameAnalytics>().M21_CreditosAoFinalDaPartida -= valor;
        //GameObject.Find("GameAnalyticsTracker").GetComponent<GameAnalytics>().M21_CreditosAoFinalDaPartida -= valor;
    }

    [Command]
    public void CmdUpdateQuantidadeDeVezesQueTentouSeHidratar()
    {
        roomPlayer.GetComponent<GameAnalytics>().M7_QuantidadeDeVezesQueTentouSeHidratar++;
        //GameObject.Find("GameAnalyticsTracker").GetComponent<GameAnalytics>().M7_QuantidadeDeVezesQueTentouSeHidratar++;
    }

    [Command]
    public void CmdUpdateQuantidadeDeVezesQueSeHidratou(){
        roomPlayer.GetComponent<GameAnalytics>().M8_QuantidadeDeVezesQueSeHidratou++;
        //GameObject.Find("GameAnalyticsTracker").GetComponent<GameAnalytics>().M8_QuantidadeDeVezesQueSeHidratou++;
    }

    [Command]
    public void CmdUpdateValorDaBarraDeEnergiaQuandoSeHidratou(float valor)
    {
        roomPlayer.GetComponent<GameAnalytics>().M9_ValorDaBarraDeEnergiaQuandoSeHidratou.Add(valor);
        
    }
    
    [Command]
    public void CmdUpdateQuantidadeDeVezesQueAEnergiaSeEsgotou()
    {
        roomPlayer.GetComponent<GameAnalytics>().M16_QuantidadeDeVezesQueAEnergiaSeEsgotou++;
    }

    [Command]
    public void CmdUpdateAcionouListaDeItens()
    {
        roomPlayer.GetComponent<GameAnalytics>().M23_QuantidadeDeVezesQueAcionouOBotaoDeListaDeItens++;
        //GameObject.Find("GameAnalyticsTracker").GetComponent<GameAnalytics>().M23_QuantidadeDeVezesQueAcionouOBotaoDeListaDeItens++;
    }

    [Command]
    public void CmdUpdateQuantidadeDeVezesQueInteragiu(){
        roomPlayer.GetComponent<GameAnalytics>().M25_QuantidadeDeVezesQueInteragiu++;
        //GameObject.Find("GameAnalyticsTracker").GetComponent<GameAnalytics>().M25_QuantidadeDeVezesQueInteragiu++;
    }

}
/*
 public void showTipPanelPopUp(){                                   
        
        foreach(GameObject gamePlayer in GameObject.FindGameObjectsWithTag("Player")){
            if (gamePlayer.GetComponent<PlayerControler>().isLocalPlayer){
                
                gamePlayer.transform.Find("HUD/ItemListPopUp").gameObject.SetActive(true);
        
                Toggle item1 = gamePlayer.transform.Find("HUD/ItemListPopUp/Panel_PopUpWindow/Item1FoundToggle").gameObject.GetComponent<Toggle>();
                Toggle item2 = gamePlayer.transform.Find("HUD/ItemListPopUp/Panel_PopUpWindow/Item2FoundToggle").gameObject.GetComponent<Toggle>();
                Toggle item3 = gamePlayer.transform.Find("HUD/ItemListPopUp/Panel_PopUpWindow/Item3FoundToggle").gameObject.GetComponent<Toggle>();
                
                
                item1.GetComponent<Text>().text = gamePlayer.GetComponent<PlayerControler>().localPlayerMissionItems[0];
                item2.GetComponent<Text>().text = gamePlayer.GetComponent<PlayerControler>().localPlayerMissionItems[1];
                item3.GetComponent<Text>().text = gamePlayer.GetComponent<PlayerControler>().localPlayerMissionItems[2];
            }

        }
        

    }*/
