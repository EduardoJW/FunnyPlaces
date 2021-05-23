using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerChoiceTracking : NetworkBehaviour
{
    [SyncVar]
    public int clientsCount;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        clientsCount = GetCountClientsConnected();
    }

    int GetCountClientsConnected(){
        return NetworkServer.connections.Count;
    }


}
