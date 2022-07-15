using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public float interactRadius = 3.0f;
    public string itemName;

    void Start(){

    }

    /*void OnDrawGizmosSelected(){
        Debug.Log("gizmosdrawcalled");
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,interactRadius);
    }*/

    void OnMouseDown(){
        Debug.Log("funcao");
        Destroy(gameObject);
    }
    

}
