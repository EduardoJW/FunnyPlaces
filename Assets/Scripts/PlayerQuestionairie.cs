using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerQuestionairie : MonoBehaviour
{
    public GameObject playerOptionsTracker;
    private PlayerChoiceTracking playerChoiceTracking;
    public Button solicitarQuestionarioButtonP1;
    public Toggle questionarioConcluidoToggleP1;
    public Button solicitarQuestionarioButtonP2;
    public Toggle questionarioConcluidoToggleP2;
    public Button solicitarQuestionarioButtonP3;
    public Toggle questionarioConcluidoToggleP3;
    
    private bool p1QuestionarioEnviado = false;
    private bool p2QuestionarioEnviado = false;
    private bool p3QuestionarioEnviado = false;
    
    // Start is called before the first frame update
    void Start()
    {
        playerChoiceTracking = playerOptionsTracker.GetComponent<PlayerChoiceTracking>();

    }

    // Update is called once per frame
    void Update()
    {
        if (solicitarQuestionarioButtonP1.interactable == false && playerChoiceTracking.clientsCount >=1){
            checkQuestionnairieAvailable(solicitarQuestionarioButtonP1,questionarioConcluidoToggleP1,p1QuestionarioEnviado);
        }
        if (solicitarQuestionarioButtonP2.interactable == false && playerChoiceTracking.clientsCount >=2){
            checkQuestionnairieAvailable(solicitarQuestionarioButtonP2,questionarioConcluidoToggleP2,p2QuestionarioEnviado);
        }
        if (solicitarQuestionarioButtonP3.interactable == false && playerChoiceTracking.clientsCount >=3){
            checkQuestionnairieAvailable(solicitarQuestionarioButtonP3,questionarioConcluidoToggleP3,p3QuestionarioEnviado);
        }
    }

    public void checkQuestionnairieAvailable(Button questionnairieButton,Toggle questionnairieCompletedToggle, bool questionnairieEnviado){
        if (questionnairieButton.interactable == false && questionnairieCompletedToggle.isOn == false && questionnairieEnviado ==false){
            questionnairieButton.interactable = true; 
        }
        
    }


    public void SendQuestionnairie(Button solicitarQuestionarioButton){
        solicitarQuestionarioButton.interactable = false;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("RoomPlayer")){
            player.GetComponent<RoomPlayerUIControler>().RpcShowQuestionnaire(int.Parse(solicitarQuestionarioButton.name.Substring(solicitarQuestionarioButton.name.Length-1)));
            player.transform.Find("QuestionnaireCanvas").gameObject.SetActive(true);
            player.transform.Find("QuestionnaireCanvas").transform.Find("PrePlayQuestionnaire").transform.position = new Vector3(999, 999, 999);
        }
        
    }

    

}
