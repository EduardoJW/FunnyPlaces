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
    public static string[,] characterModelMapping= new string [9,2] {{"1","Character_Father_01"},{"2","Character_SchoolBoy_01"},{"3","Character_Mother_02"},{"4","Character_Father_02"},{"5","Character_Daughter_01"},{"6","Character_Mother_01"},{"7","Character_SchoolGirl_01"},{"8","Character_ShopKeeper_01"},{"9","Character_Son_01"}};
    public int playerIdNumber = 0;


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
            playerIdNumber = getCharacterId();
            switch (playerIdNumber){
                case 1:
                    CmdChangePlayerCharacterModel(GameObject.FindGameObjectWithTag("PlayerOptionsContainer").GetComponent<PlayerChoiceTracking>().p1CharId);
                    break;
                case 2:
                    CmdChangePlayerCharacterModel(GameObject.FindGameObjectWithTag("PlayerOptionsContainer").GetComponent<PlayerChoiceTracking>().p2CharId);
                    break;
                case 3:
                    CmdChangePlayerCharacterModel(GameObject.FindGameObjectWithTag("PlayerOptionsContainer").GetComponent<PlayerChoiceTracking>().p3CharId);
                    break;

            }
        }

        if (!isServer){
            DisableServerControls();
        }



        
    }

    // Update is called once per frame
    void Update()
    {
        // movement for local player
        if (!isLocalPlayer) return;


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
    

    [Command]
    public void CmdChangePlayerCharacterModel(int charNumber){
        foreach(Transform child in gameObject.transform){
            if (child.CompareTag("PlayerCharacterModel")){
                child.gameObject.SetActive(false);
                if (child.gameObject.name == characterModelMapping[charNumber,1]){
                    child.gameObject.SetActive(true);
                }
                
            }

        }
        RpcNotifyClientsPlayerCharacterModelChange(charNumber);
    }

    [ClientRpc]
    public void RpcNotifyClientsPlayerCharacterModelChange(int charNumber){
        foreach(Transform child in gameObject.transform){
            if (child.CompareTag("PlayerCharacterModel")){
                child.gameObject.SetActive(false);
                if (child.gameObject.name == characterModelMapping[charNumber,1]){
                    child.gameObject.SetActive(true);
                }
                
            }

        }
    }

    public int getCharacterId(){
        PlayerChoiceTracking playerChoices = GameObject.FindGameObjectWithTag("PlayerOptionsContainer").GetComponent<PlayerChoiceTracking>();
        return playerChoices.localPlayerPlayerNumber;
    }

    public void DisableServerControls(){
        GameObject.Find("ServerManager").SetActive(false);
    }

}