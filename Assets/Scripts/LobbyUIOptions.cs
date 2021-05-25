using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class LobbyUIOptions : NetworkBehaviour
{



    [SerializeField] private Canvas psiControlPannel = null;
    [SerializeField] private Canvas playerSelectPannel = null;
    [SerializeField] private GameObject playe1ControlPanel = null;
    [SerializeField] private GameObject playe2ControlPanel = null;
    [SerializeField] private GameObject playe3ControlPanel = null;
    [SerializeField] private GameObject playerChoiceMonitorDummyObject = null;

    private int playerCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(isServer){
            psiControlPannel.gameObject.SetActive(true);           
            playerSelectPannel.gameObject.SetActive(false);
        
        }else{
            
        }

        if(!isServer){
            //PlayerChoiceTracking playerChoices = playerChoiceMonitorDummyObject.GetComponent<PlayerChoiceTracking>();
            playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
            
            switch (playerCount){
                case 0:
                    playe2ControlPanel.SetActive(false);
                    playe3ControlPanel.SetActive(false);
                    break;
                case 1:
                    playe1ControlPanel.SetActive(false);
                    playe3ControlPanel.SetActive(false);
                    break;
                case 2:
                    playe1ControlPanel.SetActive(false);
                    playe2ControlPanel.SetActive(false);
                    break;

            }



        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
