using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    GameObject serverCamera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x= Input.GetAxis("Horizontal")* Time.deltaTime *150.0f;
        float z = Input.GetAxis("Vertical")*Time.deltaTime *10.0f;
        
        transform.Rotate(0,x,0);
        transform.Translate(0,0,z);
    }

    public void callCommandtoSaveAll() 
    {
     
        GameObject[] arrayGO = GameObject.FindGameObjectsWithTag("RoomPlayer");
        foreach (GameObject gameobject in arrayGO)
        {
            gameobject.GetComponent<GameAnalytics>().AllSaveAll();
        }
        
    }

    public void MoveBackward(){
        gameObject.transform.Translate(Vector3.right * Time.deltaTime * 50.0f,Space.World);
    }


    public void MoveForward(){
        gameObject.transform.Translate(Vector3.left * Time.deltaTime * 50.0f,Space.World);
    }

    
    public void MoveRight(){
        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 50.0f,Space.World);
    }

    public void MoveLeft(){
        gameObject.transform.Translate(Vector3.back * Time.deltaTime * 50.0f,Space.World);
    }

    public void MoveUp(){
        gameObject.transform.Translate(Vector3.up * Time.deltaTime * 50.0f,Space.World);
    }

    public void MoveDown(){
        gameObject.transform.Translate(Vector3.down * Time.deltaTime * 50.0f,Space.World);
    }


}
