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
        if (solicitarQuestionarioButtonP1.interactable == false){
            checkQuestionnairieAvailable(solicitarQuestionarioButtonP1,questionarioConcluidoToggleP1,playerChoiceTracking.player1Ready,p1QuestionarioEnviado);
        }
        if (solicitarQuestionarioButtonP2.interactable == false){
            checkQuestionnairieAvailable(solicitarQuestionarioButtonP2,questionarioConcluidoToggleP2,playerChoiceTracking.player2Ready,p2QuestionarioEnviado);
        }
        if (solicitarQuestionarioButtonP1.interactable == false){
            checkQuestionnairieAvailable(solicitarQuestionarioButtonP3,questionarioConcluidoToggleP3,playerChoiceTracking.player3Ready,p3QuestionarioEnviado);
        }
    }

    public void checkQuestionnairieAvailable(Button questionnairieButton,Toggle questionnairieCompletedToggle,bool playerReadyState, bool questionnairieEnviado){
        if (questionnairieButton.interactable == false && questionnairieCompletedToggle.isOn == false && playerReadyState == true && questionnairieEnviado ==false){
            questionnairieButton.interactable = true; 
        }
        
    }


    public void sendQuestionnairie(Button solicitarQuestionarioButton){
        solicitarQuestionarioButton.interactable = false;
        switch(solicitarQuestionarioButton.name.Substring(solicitarQuestionarioButton.name.Length-1)){
            case "1":
                Debug.Log("mostrar questionario na ui do jogador 1");
                break;
            case "2":
                Debug.Log("mostrar questionario na ui do jogador 2");
                break;
            case "3":
                Debug.Log("mostrar questionario na ui do jogador 3");
                break;
        }


    }


}
