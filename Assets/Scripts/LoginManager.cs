using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField playerNameInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Login(){
        if (playerNameInput.text =="Servidor"){
            LoadServerLobby();
        }else{
            LoadPlayerLobby();
        }
    }

    void LoadServerLobby(){
        GameObject.Find("LoginScreen").SetActive(false);
        GameObject.Find("MainScreen").transform.Find("PlayerLobbyScreen").gameObject.SetActive(true);
    }

    void LoadPlayerLobby(){
        GameObject.Find("LoginScreen").SetActive(false);
        GameObject.Find("MainScreen").transform.Find("ServerLobbyScreen").gameObject.SetActive(true);
    }

}
