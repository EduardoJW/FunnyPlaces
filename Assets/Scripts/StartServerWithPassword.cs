using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartServerWithPassword : MonoBehaviour
{
    public NetworkRoomManagerExtTest manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startServerWithPassword(){
        if (gameObject.transform.Find("ServerPasswordInput").gameObject.GetComponent<TMP_InputField>().text == "psi-inf"){
            gameObject.SetActive(false);
            manager.StartServer();
        }
    }

}
