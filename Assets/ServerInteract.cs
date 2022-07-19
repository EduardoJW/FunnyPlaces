using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ServerInteract : NetworkBehaviour
{
    [Header("Camera")]
    public GameObject serverCamera;
    private Camera cam;

    public GameObject clickedObject;
    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            cam = serverCamera.GetComponent<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer) return;
         
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    clickedObject = hit.collider.gameObject;
                    if (clickedObject.tag == "Tree")
                    {
                    clickedObject.transform.GetComponent<TreeBirdSpawn>().Bird = true;
                        /*
                        GameObject[] arrayGO = GameObject.FindGameObjectsWithTag("Player");
                        foreach (GameObject gameobject in arrayGO)
                        {
                            NetworkIdentity nId = gameobject.GetComponent<NetworkIdentity>();
                            clientbirdspawn(nId.connectionToClient, clickedObject);
                        }*/
                    }
                }
            }
        

    }

    /*[TargetRpc]
    public void clientbirdspawn(NetworkConnection client, GameObject clickedObject) 
    {
        clickedObject.transform.Find("Bird").gameObject.SetActive(true);
    }*/
}
