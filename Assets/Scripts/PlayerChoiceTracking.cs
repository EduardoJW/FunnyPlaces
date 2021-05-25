using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerChoiceTracking : NetworkBehaviour
{
    [SyncVar]
    public int clientsCount;

    [SyncVar]
    public bool player1Ready = false;

    [SyncVar]
    public bool player2Ready = false;
    
    [SyncVar]
    public bool player3Ready = false;
    
    [SyncVar]
    public int p1CharId =1;

    [SyncVar]
    public int p2CharId =1;
    
    [SyncVar]
    public int p3CharId =1;



    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        clientsCount = GetCountClientsConnected();
    }

    [Server]
    public void playerPool(){
        
    }

    int GetCountClientsConnected(){
        return NetworkServer.connections.Count;
    }

    
    public void changeReadyState(int number){
        switch (number){
            case 1:
                player1Ready = !player1Ready;    
                break;
            case 2:
                player2Ready = !player2Ready;    
                break;
            case 3:
                player3Ready = !player3Ready;    
                break;  
        }
    }
}
