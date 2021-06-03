using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;


public class PlayerControler : NetworkBehaviour
{
    public static Dictionary<string,string> hotelDecorationMapping = new Dictionary<string,string>{
        {"SonoBom","quadro colorido na parede."},
        {"DormeBem","quadro com o homem de chapéu na parede."},
        {"RoncoAlto","quadro com o casal de guarda-chuva na parede."}
    };   
    
    public static Dictionary<string,string> itemDicaMapping = new Dictionary<string,string>{
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
    
    
    private GameObject clickedObject;
    [Header("Interaction Variables")]
    public float interactDistance;
    private GameObject playercharacterchoice;
    public static string[,] characterModelMapping= new string [9,2] {{"1","Character_Father_01"},{"2","Character_SchoolBoy_01"},{"3","Character_Mother_02"},{"4","Character_Father_02"},{"5","Character_Daughter_01"},{"6","Character_Mother_01"},{"7","Character_SchoolGirl_01"},{"8","Character_ShopKeeper_01"},{"9","Character_Son_01"}};
    public int playerIdNumber = 0;
    public float timeLastInteraction=0.0f;
    public float minimumTimeBetweenInteractions = 20.0f;

    
    [Header("Wallet")]
    public Text walletMoneyDisplay;
    public int walletMoneyValue = 2000;



    
    public string[] localPlayerMissionItems = new string[3];
    public string localPlayerHotel;
    public bool localPlayerCalcularCompra;
    public int localPlayerWorkingMemoryDigits;

    
    

    private string wellcomeString ="Bem Vindo a Lugares Divertidos!!!\n"+
     "A festa de 80 anos do seu amigo vai ser um arraso! \n"+
    "Mas para isso você precisa encontrar alguns itens e "+
    "trazê-los para a festa e trazê-los de volta ao seu hotel. \n"+
    "Aqui está sua Lista:\n\n"+
    "- 1 {0}\n"+
    "- 1 {1}\n"+
    "- 1 {2}\n\n"+
    "Boa Sorte! Ah! E lembre-se seu hotel é o Hotel {3}, aquele com o {4}!";


    [Header("Movement")]
        public float rotationSpeed = 100;

    

    // Start is called before the first frame update
    void Start()
    {   

        if (isLocalPlayer) gameObject.transform.Find("HUD").gameObject.SetActive(true);
        animatorController = GetComponent<Animator>();


        if (isLocalPlayer){
            playerCamera.SetActive(true);
            cam = playerCamera.GetComponent<Camera>();
            playerIdNumber = getCharacterId();
            CmdRegisterPlayerNumberOnServer(playerIdNumber);
            switch (playerIdNumber){
                case 1:
                    CmdChangePlayerCharacterModel(GameObject.FindGameObjectWithTag("PlayerOptionsContainer").GetComponent<PlayerChoiceTracking>().p1CharId);
                    break;
                case 2:
                    CmdChangePlayerCharacterModel(GameObject.FindGameObjectWithTag("PlayerOptionsContainer").GetComponent<PlayerChoiceTracking>().p2CharId);
                    break;
                case 3:
                    CmdChangePlayerCharacterModel(GameObject.FindGameObjectWithTag("PlayerOptionsContainer").GetComponent<PlayerChoiceTracking>().p3CharId);
                    break;

            }
        }

        if (!isServer){
            DisableServerControls();
        }

        if (isLocalPlayer){
            RegisterMissionLocalPlayerMissionItemsGameOptions(playerIdNumber);
            showWellcomePopUp();
            energyBarConsumeRate = 0.0001f;

        }

        



        
    }

    // Update is called once per frame
    void Update()
    {
        // movement for local player
        if (!isLocalPlayer) return;


        float x= Input.GetAxis("Horizontal")* Time.deltaTime *150.0f;
        float z = Input.GetAxis("Vertical")*Time.deltaTime *3.0f;
        
        transform.Rotate(0,x,0);
        transform.Translate(0,0,z);

        if (z==0){
            animatorController.SetBool("isWalking",false);
            animatorController.SetBool("isWalkingBackwards",false);
        }

        if (z>0){
            animatorController.SetBool("isWalking",true);
            animatorController.SetBool("isWalkingBackwards",false);
        }else if (z<0){
            animatorController.SetBool("isWalking",false);
            animatorController.SetBool("isWalkingBackwards",true);
        }

        if (z!=0){
            ConsumeEnergyWalking();
        }

        //get lef mouse button click
        if (Input.GetMouseButtonDown(0)){
            Debug.Log("name");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit)){
                clickedObject = hit.collider.gameObject;
                if (clickedObject.tag == "Pickupable"){
                    gameObject.transform.Find("HUD/CreditCardMachine").gameObject.SetActive(true);
                    if (Vector3.Distance(transform.position,clickedObject.transform.position) <= interactDistance){
                        Destroy(clickedObject);
                    }else{
                        Debug.Log("O objeto está muito longe!");
                    }   
                }else if(clickedObject.tag =="RefreshmentPoint") {
                    UseRefreshmentPoint(clickedObject.tag);
                }else if (clickedObject.tag =="Player"){
                    if (Vector3.Distance(transform.position,clickedObject.transform.position) <= interactDistance){
                        if ((timeLastInteraction ==0.0f) || ((Time.time-timeLastInteraction)>minimumTimeBetweenInteractions)){
                            timeLastInteraction = Time.time;
                            RefreshThroughInteraction();
                        }
                    }else{

                    }

                }
            }
        }




    }
    

    [Command]
    public void CmdChangePlayerCharacterModel(int charNumber){
        foreach(Transform child in gameObject.transform){
            if (child.CompareTag("PlayerCharacterModel")){
                child.gameObject.SetActive(false);
                if (child.gameObject.name == characterModelMapping[charNumber,1]){
                    child.gameObject.SetActive(true);
                }
                
            }

        }
        RpcNotifyClientsPlayerCharacterModelChange(charNumber);
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


    public string getHotelDecorationTip(string hotelName){
        return hotelDecorationMapping[hotelName];
    }

    
    public void showItemListPanelPopUp(){                                   
        if (isLocalPlayer){
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
    }

    public void UseRefreshmentPoint(string clickedObjectTag){
        if (clickedObjectTag == "RefreshmentPoint"){
            CmdUpdateQuantidadeDeVezesQueTentouSeHidratar();
            if (ReduceWalletMoney(100)){
                CmdUpdateQuantidadeDeVezesQueSeHidratou();
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

    public bool ReduceWalletMoney(int itemOrActionPrice){
        if (walletMoneyValue -itemOrActionPrice>0){
            walletMoneyValue -= itemOrActionPrice;
            walletMoneyDisplay.text = walletMoneyValue.ToString();
            Debug.Log("walletMoneyValue.ToString()");
            return true;
        }
        return false;
    }

    [Command]
    public void CmdRegisterPlayerNumberOnServer(int IdNumber){
        playerIdNumber = IdNumber;
    }

    [Command]
    public void CmdUpdateQuantidadeDeVezesQueTentouSeHidratar(){
        gameObject.GetComponent<GameAnalytics>().M7_QuantidadeDeVezesQueTentouSeHidratar++;
    }

    [Command]
    public void CmdUpdateQuantidadeDeVezesQueSeHidratou(){
        gameObject.GetComponent<GameAnalytics>().M8_QuantidadeDeVezesQueSeHidratou++;
    }

    [Command]
    public void CmdUpdateQuantidadeDeVezesQueInteragiu(){
        gameObject.GetComponent<GameAnalytics>().M25_QuantidadeDeVezesQueInteragiu++;
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
