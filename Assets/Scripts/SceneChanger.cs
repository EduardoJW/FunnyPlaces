using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SceneChanger : MonoBehaviour
{
    public NetworkManager manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change(){
        Debug.Log("teste");
        manager.ServerChangeScene("cena2");

    }

}
