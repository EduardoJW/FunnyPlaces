using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionnaireControl : MonoBehaviour
{
    public Button sheet1NextButton;
    public Button sheet2NextButton;
    public Button sheet3NextButton;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckReadyToNextSheet(){
        bool button1Interactable=false;
        bool button2Interactable=false;
        bool button3Interactable=false;
        for (int i = 1;i<=15;i++){
            if (i<6){
                if (gameObject.transform.Find("PrePlayQuestionnaireContent/Sheet1/Pergunta" + i + "/RespostaPergunta" + i).gameObject.GetComponent<TMP_Dropdown>().value ==0){
                    button1Interactable = false;
                    break;
                }else{
                    button1Interactable=true;
                }
            }else if (i<11){
                if (gameObject.transform.Find("PrePlayQuestionnaireContent/Sheet2/Pergunta" + i + "/RespostaPergunta" + i).gameObject.GetComponent<TMP_Dropdown>().value ==0){
                    button2Interactable = false;
                    break;
                }else{
                    button2Interactable=true;
                }
            }else{
                if (gameObject.transform.Find("PrePlayQuestionnaireContent/Sheet3/Pergunta" + i + "/RespostaPergunta" + i).gameObject.GetComponent<TMP_Dropdown>().value ==0){
                    Debug.Log(i);
                    button3Interactable = false;
                    break;
                }else{
                    button3Interactable = true;
                }
            }
        }
        if (button1Interactable == true){
            sheet1NextButton.interactable = true;
        }else{
            sheet1NextButton.interactable = false;
        }
        
        if (button2Interactable == true){
            sheet2NextButton.interactable = true;
        }else{
            sheet2NextButton.interactable = false;
        }

        if (button3Interactable == true){
            sheet3NextButton.interactable = true;
        }else{
            sheet3NextButton.interactable = false;
        }
    }

    public void NotifyServer(){
        gameObject.transform.root.gameObject.GetComponent<RoomPlayerUIControler>().CmdNotifyServerQuestionaireCompleted(GameObject.FindGameObjectWithTag("PlayerOptionsContainer").GetComponent<PlayerChoiceTracking>().localPlayerPlayerNumber);
    }

}
