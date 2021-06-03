using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class ServerMessageListener : NetworkBehaviour
{
    
    [SyncVar(hook="ShowServerMessageFromPsychologist")]
    public string serverMessageFromPsychologist="";
    public GameObject serverMessageArea = null;
    //private float timeServerMessageArrived;
    //private float serverMessageExibitionTime=10.0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void ShowServerMessageFromPsychologist(string oldMessage,string newMessage){
        Debug.Log("chegou aqui");
        serverMessageArea.SetActive(true);
        serverMessageArea.GetComponent<TMP_Text>().text=newMessage;
        serverMessageArea.GetComponent<Animation>().Play("ServerMessageFading");
    }
}
