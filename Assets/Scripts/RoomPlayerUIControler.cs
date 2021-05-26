using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class RoomPlayerUIControler : NetworkBehaviour
{
    private const string playerImagePrefix ="PlayerType";
    private const int startingCharNumber = 1;
    private const int maxCharTypes = 8;

    public int charID {get;private set;} = startingCharNumber;
    public int playerNumber {get;private set;} = 0;
    public bool isReadyToPlay {get;private set;} = false;
    

    
    [Header("UI Elements")]
    [SerializeField] private Image player1Controls = null;
    [SerializeField] private Image player2Controls = null;
    [SerializeField] private Image player3Controls = null;
    [SerializeField] private Button player1NextCharButton = null;
    [SerializeField] private Button player1PreviousCharButton = null;
    [SerializeField] private Button player1ReadyButton = null;
    [SerializeField] private Button player2NextCharButton = null;
    [SerializeField] private Button player2PreviousCharButton = null;
    [SerializeField] private Button player2ReadyButton = null;
    [SerializeField] private Button player3NextCharButton = null;
    [SerializeField] private Button player3PreviousCharButton = null;
    [SerializeField] private Button player3ReadyButton = null;
    [SerializeField] private Image player1Card = null;
    [SerializeField] private Image player2Card = null;
    [SerializeField] private Image player3Card = null;
    


    // Start is called before the first frame update
    void Start()
    {
        
        if (isLocalPlayer){
            gameObject.transform.Find("LobbyPlayerHUD").gameObject.SetActive(true);
            startupUI();
        }
            
    }
    
    // Update is called once per frame
    void Update()
    {
        UpdateOtherPlayersDisplay();
    }



    public void startupUI(){
        playerNumber = GameObject.FindGameObjectsWithTag("RoomPlayer").Length;
        RegisterPlayerNumber(playerNumber);
        player1Card.sprite=Resources.Load<Sprite>("Sprites/" + playerImagePrefix + charID.ToString());
        player2Card.sprite=Resources.Load<Sprite>("Sprites/" + playerImagePrefix + charID.ToString());
        player3Card.sprite=Resources.Load<Sprite>("Sprites/" + playerImagePrefix + charID.ToString());
        
        switch (playerNumber){
            case 1:
                player2Controls.gameObject.SetActive(false);
                player3Controls.gameObject.SetActive(false);
                break;
            case 2:
                player1Controls.gameObject.SetActive(false);
                player3Controls.gameObject.SetActive(false);
                break;
            case 3:
                player1Controls.gameObject.SetActive(false);
                player2Controls.gameObject.SetActive(false);
                break;

            }

    }


      public void nextChar(){
        charID++;
        if (charID > 8) charID =0;
        switch (playerNumber){
            case 1:
                player1Card.sprite=Resources.Load<Sprite>("Sprites/" + playerImagePrefix + charID.ToString());
                break;
            case 2:
                player2Card.sprite=Resources.Load<Sprite>("Sprites/" + playerImagePrefix + charID.ToString());
                break;
            case 3:
                player3Card.sprite=Resources.Load<Sprite>("Sprites/" + playerImagePrefix + charID.ToString());
                break;

        }
        
        CmdUpdatePlayerMonitor(playerNumber,charID,isReadyToPlay);
    }

    public void previousChar(){
        charID--;
        if (charID < 0) charID =8;
        switch (playerNumber){
            case 1:
                player1Card.sprite=Resources.Load<Sprite>("Sprites/" + playerImagePrefix + charID.ToString());
                break;
            case 2:
                player2Card.sprite=Resources.Load<Sprite>("Sprites/" + playerImagePrefix + charID.ToString());
                break;
            case 3:
                player3Card.sprite=Resources.Load<Sprite>("Sprites/" + playerImagePrefix + charID.ToString());
                break;

        }
        Debug.Log(charID);
        CmdUpdatePlayerMonitor(playerNumber,charID,isReadyToPlay);
    }

    public void ToggleReady(){
        isReadyToPlay=!isReadyToPlay;
        switch (playerNumber){
            case 1:
                if (isReadyToPlay){
                    player1ReadyButton.GetComponentInChildren<TMPro.TMP_Text>().text = "Cancelar";
                    gameObject.transform.Find("LobbyPlayerHUD/PlayerSelectPanel/Player1ReadyText").gameObject.GetComponentInChildren<TMPro.TMP_Text>().text = "<color=green>Pronto</color>";
                }else{
                    player1ReadyButton.GetComponentInChildren<TMPro.TMP_Text>().text = "Pronto";
                    gameObject.transform.Find("LobbyPlayerHUD/PlayerSelectPanel/Player1ReadyText").gameObject.GetComponentInChildren<TMPro.TMP_Text>().text = "<color=red>Escolhendo</color>";
                }
                break;
            case 2:
                if (isReadyToPlay){
                    player2ReadyButton.GetComponentInChildren<TMPro.TMP_Text>().text = "Cancelar";
                    gameObject.transform.Find("LobbyPlayerHUD/PlayerSelectPanel/Player2ReadyText").gameObject.GetComponentInChildren<TMPro.TMP_Text>().text = "<color=green>Pronto</color>";
                }else{
                    player2ReadyButton.GetComponentInChildren<TMPro.TMP_Text>().text = "Pronto";
                    gameObject.transform.Find("LobbyPlayerHUD/PlayerSelectPanel/Player2ReadyText").gameObject.GetComponentInChildren<TMPro.TMP_Text>().text = "<color=red>Escolhendo</color>";
                }
                break;
            case 3:
                if (isReadyToPlay){
                    player3ReadyButton.GetComponentInChildren<TMPro.TMP_Text>().text = "Cancelar";
                    gameObject.transform.Find("LobbyPlayerHUD/PlayerSelectPanel/Player3ReadyText").gameObject.GetComponentInChildren<TMPro.TMP_Text>().text = "<color=green>Pronto</color>";
                }else{
                    player3ReadyButton.GetComponentInChildren<TMPro.TMP_Text>().text = "Pronto";
                    gameObject.transform.Find("LobbyPlayerHUD/PlayerSelectPanel/Player3ReadyText").gameObject.GetComponentInChildren<TMPro.TMP_Text>().text = "<color=red>Escolhendo</color>";
                }
                break;

        }
        CmdUpdatePlayerMonitor(playerNumber,charID,isReadyToPlay);
        
    }

    [Command]
    public void CmdUpdatePlayerMonitor(int playerNumber,int playerCharID,bool ready){
        PlayerChoiceTracking playerChoiceScript; 
        playerChoiceScript = GameObject.Find("DummyObjectPlayerOptions").GetComponent<PlayerChoiceTracking>();
        switch(playerNumber){
            case 1:
                playerChoiceScript.p1CharId = playerCharID;
                playerChoiceScript.player1Ready = ready;
                break;
            case 2:
                playerChoiceScript.p2CharId = playerCharID;
                playerChoiceScript.player2Ready = ready;
                break;
            case 3:
                playerChoiceScript.p3CharId = playerCharID;
                playerChoiceScript.player3Ready = ready;
                break;
        } 
    }

    [ClientRpc]
    public void RpcDisableUI(){
        gameObject.transform.Find("LobbyPlayerHUD").gameObject.SetActive(false);
    }

    public void UpdateOtherPlayersDisplay(){
        PlayerChoiceTracking playerChoiceScript; 
        playerChoiceScript = GameObject.Find("DummyObjectPlayerOptions").GetComponent<PlayerChoiceTracking>();
        if (playerNumber != 1){
            if (playerChoiceScript.player1Ready){
                gameObject.transform.Find("LobbyPlayerHUD/PlayerSelectPanel/Player1ReadyText").gameObject.GetComponentInChildren<TMPro.TMP_Text>().text = "<color=green>Pronto</color>";
            }else{
                gameObject.transform.Find("LobbyPlayerHUD/PlayerSelectPanel/Player1ReadyText").gameObject.GetComponentInChildren<TMPro.TMP_Text>().text = "<color=red>Escolhendo...</color>";
            }
            if (player1Card.sprite.name != (playerImagePrefix + playerChoiceScript.p1CharId.ToString()) ){
                player1Card.sprite=Resources.Load<Sprite>("Sprites/" + playerImagePrefix + playerChoiceScript.p1CharId.ToString());
            }
        }
        
        if (playerNumber != 2){
            if (playerChoiceScript.player2Ready){
                gameObject.transform.Find("LobbyPlayerHUD/PlayerSelectPanel/Player2ReadyText").gameObject.GetComponentInChildren<TMPro.TMP_Text>().text = "<color=green>Pronto</color>";
            }else{
                gameObject.transform.Find("LobbyPlayerHUD/PlayerSelectPanel/Player2ReadyText").gameObject.GetComponentInChildren<TMPro.TMP_Text>().text = "<color=red>Escolhendo...</color>";
            }
            if (player2Card.sprite.name != (playerImagePrefix + playerChoiceScript.p2CharId.ToString()) ){
                player2Card.sprite=Resources.Load<Sprite>("Sprites/" + playerImagePrefix + playerChoiceScript.p2CharId.ToString());
            }
        }

        if (playerNumber != 3){
            if (playerChoiceScript.player3Ready){
                gameObject.transform.Find("LobbyPlayerHUD/PlayerSelectPanel/Player3ReadyText").gameObject.GetComponentInChildren<TMPro.TMP_Text>().text = "<color=green>Pronto</color>";
            }else{
                gameObject.transform.Find("LobbyPlayerHUD/PlayerSelectPanel/Player3ReadyText").gameObject.GetComponentInChildren<TMPro.TMP_Text>().text = "<color=red>Escolhendo...</color>";
            }
            if (player3Card.sprite.name != (playerImagePrefix + playerChoiceScript.p3CharId.ToString()) ){
                player3Card.sprite=Resources.Load<Sprite>("Sprites/" + playerImagePrefix + playerChoiceScript.p3CharId.ToString());
            }
        }
    }

    public void RegisterPlayerNumber(int localPlayerNumber){
        PlayerChoiceTracking playerChoiceScript; 
        playerChoiceScript = GameObject.Find("DummyObjectPlayerOptions").GetComponent<PlayerChoiceTracking>();
        playerChoiceScript.localPlayerPlayerNumber = localPlayerNumber; 
    }






}
