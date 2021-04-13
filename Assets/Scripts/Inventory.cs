using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public bool[] slotOcupado = new bool[3];
    public GameObject[] slots = new GameObject[3];
    

    void start(){
        Debug.Log("start do inventory");
    }



    public void fillSlot(int slot){
        slotOcupado[slot] = true;
    }


}
