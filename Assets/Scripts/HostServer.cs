using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class HostServer : MonoBehaviour
{
    [SerializeField] private NetworkRoomManager networkManager = null;
    [Header("UI")]
    [SerializeField] private TMP_InputField passwordInputField = null;
    [SerializeField] private Button hostServerButton = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ValidatePassword(){
        string password = passwordInputField.text;
        if (password == "psi-inf"){
            networkManager.StartServer();
            //GameObject.Find("ServerManager").transform.Find("ServerCamera").gameObject.SetActive(true);
            //GameObject.Find("ServerManager").transform.Find("ServerControlerUI").gameObject.SetActive(true);
            
            gameObject.SetActive(false);
        }else{
            Debug.Log("senha incorreta");    
        }
        
    }




}
