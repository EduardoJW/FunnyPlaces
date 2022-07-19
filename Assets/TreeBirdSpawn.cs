using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TreeBirdSpawn : NetworkBehaviour
{
    [SyncVar]
    public bool Bird = false;
    public bool BirdalreadyTrue = false;
    public GameObject self;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Bird) 
        {
            if (!BirdalreadyTrue) 
            { 
                gameObject.transform.Find("Bird").gameObject.SetActive(true);
                GameObject[] arrayGO = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject gameobject in arrayGO)
                {
                    NetworkIdentity nId = gameobject.GetComponent<NetworkIdentity>();
                    clientbirdspawn(nId.connectionToClient, self);
                }
                BirdalreadyTrue = true;
            }
        }
    }
    [TargetRpc]
    public void clientbirdspawn(NetworkConnection client, GameObject self)
    {
        self.transform.Find("Bird").gameObject.SetActive(true);
    }
}
