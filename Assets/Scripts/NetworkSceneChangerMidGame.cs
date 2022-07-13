using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkSceneChangerMidGame : MonoBehaviour
{
    private GameObject roomMananer;
    public int RecogPlayer1 = 0;
    public int RecogPlayer2 = 0;
    public int RecogPlayer3 = 0;

    private NetworkRoomManagerExtTest networkRoomManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        networkRoomManagerScript = GameObject.Find("RoomManagerExtTest").GetComponent<NetworkRoomManagerExtTest>();
    }

    // Update is called once per frame
    public void SceneChangeMainTown()
    {
        networkRoomManagerScript.ServerChangeScene("MainTown");
    }
    public void SceneChangeReconhecimento()
    {
        networkRoomManagerScript.ServerChangeScene("TutorialReconhecimento");
    }

}
