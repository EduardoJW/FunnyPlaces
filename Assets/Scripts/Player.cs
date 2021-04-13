using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    private Animator animatorController;
    public GameObject playerCamera;
    private Camera cam;
    private GameObject clickedObject;
    public float interactDistance;
    private GameObject playercharacterchoice;

    [Header("Movement")]
        public float rotationSpeed = 100;

    

    // Start is called before the first frame update
    void Start()
    {   
        if (isLocalPlayer) gameObject.transform.Find("HUD").gameObject.SetActive(true);
        animatorController = GetComponent<Animator>();

        if (isLocalPlayer){
            playerCamera.SetActive(true);
            cam = playerCamera.GetComponent<Camera>();
            
            /*if (charactertype >1){
                gameObject.transform.Find("Character_Father_01").gameObject.SetActive(false);
                gameObject.transform.Find("Character_Father_02").gameObject.SetActive(true);
            }else{
                gameObject.transform.Find("Character_Father_01").gameObject.SetActive(false);
                gameObject.transform.Find("Character_Mother_01").gameObject.SetActive(true);
            }*/

        }


        
    }

    // Update is called once per frame
    void Update()
    {
        // movement for local player
        if (!isLocalPlayer) return;

        /*
        //movement 1
        float horizontal = Input.GetAxis("Horizontal");
        transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);
        float vertical = Input.GetAxis("Vertical");
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        */

        

        //movement 2
        float x= Input.GetAxis("Horizontal")* Time.deltaTime *150.0f;
        float z = Input.GetAxis("Vertical")*Time.deltaTime *3.0f;
        
        transform.Rotate(0,x,0);
        transform.Translate(0,0,z);

        if (z==0){
            animatorController.SetBool("isWalking",false);
            animatorController.SetBool("isWalkingBackwards",false);
        }

        if (z>0){
            animatorController.SetBool("isWalking",true);
            animatorController.SetBool("isWalkingBackwards",false);
        }else if (z<0){
            animatorController.SetBool("isWalking",false);
            animatorController.SetBool("isWalkingBackwards",true);
        }

        //get lef mouse button click
        if (Input.GetMouseButtonDown(0)){
            Debug.Log("name");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit)){
                clickedObject = hit.collider.gameObject;
                if (clickedObject.tag == "Pickupable"){
                    if (Vector3.Distance(transform.position,clickedObject.transform.position) <= interactDistance){
                        Destroy(clickedObject);
                    }else{
                        Debug.Log("O objeto está muito longe!");
                    }
                    
                }
                
    
            }



        }





    }
}