using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Pickup : MonoBehaviour
{
    private Inventory inventory;
    public GameObject itemButton;
    void Start()
    {

    }

    void OnTriggerEnter(Collider otherObjectCollider) {
        if (otherObjectCollider.gameObject.tag == "Pickupable"){
            inventory = gameObject.GetComponent<Inventory>();
            //for (int i = 0; i < inventory.slots.Length; i++)
            

                        
        }
    }
    void Update()
    {
        
    }
}
