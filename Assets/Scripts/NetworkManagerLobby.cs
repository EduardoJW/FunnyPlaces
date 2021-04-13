using UnityEngine;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;


public class NetworkManagerLobby : NetworkManager
{
    [SerializeField] private int minimoJogadores = 3;
    [Scene] [SerializeField] private string menuScene = string.Empty;
    
    [Header("Room")]
    [SerializeField] private NetworkRoomPlayerLobby roomPlayerPrefab = null;

    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;

    public List<NetworkRoomPlayerLobby> RoomPlayers {get;} = new List<NetworkRoomPlayerLobby>();


    public override void OnStartServer()
    {
        //base.OnStartServer();
        spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();

    }

    public override void OnStartClient()
    {
        //base.OnStartClient();()
        var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");
        foreach(var prefab in spawnablePrefabs){
            ClientScene.RegisterPrefab(prefab);
        }
        
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        OnClientConnected?.Invoke();

    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        OnClientDisconnected?.Invoke();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (numPlayers >= maxConnections){
            conn.Disconnect();
            return;
        }

        if (SceneManager.GetActiveScene().name != menuScene){
            conn.Disconnect();
            return;
        }

    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        
        if (SceneManager.GetActiveScene().name == menuScene){
            //apenas para o caso de usar host. para modelo com psicologo como servidor, nao precisa
            //bool isLeader = RoomPlayers.Count ==0;
            
            NetworkRoomPlayerLobby roomPlayerInstance = Instantiate(roomPlayerPrefab);
            NetworkServer.AddPlayerForConnection(conn,roomPlayerInstance.gameObject);

            //apenas para o caso de host
            //roomPlayerInstance = isLeader;

        }
        base.OnServerAddPlayer(conn);
    }

    public void hello(){
        Debug.Log("Hello");
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if (conn.identity !=null){
            var player = conn.identity.GetComponent<NetworkRoomPlayerLobby>();
            RoomPlayers.Remove(player);
            NotifyPlayersOfReadyState();
        }
        base.OnServerDisconnect(conn);
    }

    public override void OnStopServer()
    {
        RoomPlayers.Clear();
        //base.OnStopServer();
    }


    public void NotifyPlayersOfReadyState(){
        foreach(var player in RoomPlayers){
            player.HandleReadyToStart(IsReadyToStart());
        }
    }


    private bool IsReadyToStart(){
        if(numPlayers < minimoJogadores){return false;}
        foreach(var player in RoomPlayers){
            if (!player.IsReady){return false;}
        }
        return true;
    }

















    
}