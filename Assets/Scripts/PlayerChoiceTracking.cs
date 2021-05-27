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

    public int localPlayerPlayerNumber = 0;


    public string p1Hotel;//{get;private set;}
    public string p2Hotel;//{get;private set;}
    public string p3Hotel;//{get;private set;}
    public string p1Item1;//{get;private set;}
    public string p1Item2;//{get;private set;}
    public string p1Item3;//{get;private set;}
    public string p2Item1;//{get;private set;}
    public string p2Item2;//{get;private set;}
    public string p2Item3;//{get;private set;}
    public string p3Item1;//{get;private set;}
    public string p3Item2;//{get;private set;}
    public string p3Item3;//{get;private set;}
    public bool p1CalcularCompra;//{get;private set;}
    public bool p2CalcularCompra;//{get;private set;}
    public bool p3CalcularCompra;//{get;private set;}
    public int p1WorkingMemoryDigits;//{get;private set;}
    public int p2WorkingMemoryDigits;//{get;private set;}
    public int p3WorkingMemoryDigits;//{get;private set;}
    
    







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


    [ClientRpc]
    public void RpcSetGameplayOptionsVariables(string p1h,string p2h,string p3h,string p1i1,string p1i2,string p1i3,string p2i1,string p2i2,string p2i3,string p3i1,string p3i2, string p3i3,bool p1cc,bool p2cc,bool p3cc,int p1wmd,int p2wmd, int p3wmd){
        p1Hotel = p1h;
        p2Hotel = p2h;
        p3Hotel = p3h;
        p1Item1 = p1i1;
        p1Item2 = p1i2;
        p1Item3 = p1i3;
        p2Item1 = p2i1;
        p2Item2 = p2i2;
        p2Item3 = p2i3;
        p3Item1 = p3i1;
        p3Item2 = p3i2;
        p3Item3 = p3i3;
        p1CalcularCompra = p1cc;
        p2CalcularCompra = p2cc;
        p3CalcularCompra = p3cc;
        p1WorkingMemoryDigits = p1wmd;
        p2WorkingMemoryDigits = p2wmd;
        p3WorkingMemoryDigits = p3wmd;
    }



}
