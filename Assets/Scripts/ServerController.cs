using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ServerController : NetworkBehaviour
{
    private Camera serverCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void LoadCamera()
    {
        GameObject.Find("ServerManager").GetComponent<Camera>().enabled=true;
    }

}
