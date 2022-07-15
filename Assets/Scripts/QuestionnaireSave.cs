using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class QuestionnaireSave : NetworkBehaviour
{
    public int A1 = 0;
    public int A2 = 0;
    public int A3 = 0;
    public int A4 = 0;
    public int A5 = 0;
    public int A6 = 0;
    public int A7 = 0;
    public int A8 = 0;
    public int A9 = 0;
    public int A10 = 0;
    public int A11 = 0;
    public int A12 = 0;
    public int A13 = 0;
    public int A14 = 0;
    public int A15 = 0;

    public GameObject Questionnaire;

    public class Answers
    {
        public int A1;
        public int A2;
        public int A3;
        public int A4;
        public int A5;
        public int A6;
        public int A7;
        public int A8;
        public int A9;
        public int A10;
        public int A11;
        public int A12;
        public int A13;
        public int A14;
        public int A15;
    }

    //[Command]
    public void RecordAnswers1()
    {
        A1 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet1/Pergunta1/RespostaPergunta1").gameObject.GetComponent<TMP_Dropdown>().value;
        A2 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet1/Pergunta2/RespostaPergunta2").gameObject.GetComponent<TMP_Dropdown>().value;
        A3 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet1/Pergunta3/RespostaPergunta3").gameObject.GetComponent<TMP_Dropdown>().value;
        A4 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet1/Pergunta4/RespostaPergunta4").gameObject.GetComponent<TMP_Dropdown>().value;
        A5 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet1/Pergunta5/RespostaPergunta5").gameObject.GetComponent<TMP_Dropdown>().value;
    }
    //[Command]
    public void RecordAnswers2()
    {
        A6 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet2/Pergunta6/RespostaPergunta6").gameObject.GetComponent<TMP_Dropdown>().value;
        A7 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet2/Pergunta7/RespostaPergunta7").gameObject.GetComponent<TMP_Dropdown>().value;
        A8 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet2/Pergunta8/RespostaPergunta8").gameObject.GetComponent<TMP_Dropdown>().value;
        A9 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet2/Pergunta9/RespostaPergunta9").gameObject.GetComponent<TMP_Dropdown>().value;
        A10 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet2/Pergunta10/RespostaPergunta10").gameObject.GetComponent<TMP_Dropdown>().value;
    }
    //[Command]
    public void RecordAnswers3()
    {
        A11 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet3/Pergunta11/RespostaPergunta11").gameObject.GetComponent<TMP_Dropdown>().value;
        A12 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet3/Pergunta12/RespostaPergunta12").gameObject.GetComponent<TMP_Dropdown>().value;
        A13 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet3/Pergunta13/RespostaPergunta13").gameObject.GetComponent<TMP_Dropdown>().value;
        A14 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet3/Pergunta14/RespostaPergunta14").gameObject.GetComponent<TMP_Dropdown>().value;
        A15 = Questionnaire.transform.Find("PrePlayQuestionnaireContent/Sheet3/Pergunta15/RespostaPergunta15").gameObject.GetComponent<TMP_Dropdown>().value;
    }

    [Command]
    public void SaveQuestionnaire()
    {
        Answers answer = new Answers();
        answer.A1 = A1;
        answer.A2 = A2;
        answer.A3 = A3;
        answer.A4 = A4;
        answer.A5 = A5;
        answer.A6 = A6;
        answer.A7 = A7;
        answer.A8 = A8;
        answer.A9 = A9;
        answer.A10 = A10;
        answer.A11 = A11;
        answer.A12 = A12;
        answer.A13 = A13;
        answer.A14 = A14;
        answer.A15 = A15;

        string json = JsonUtility.ToJson(answer);

        string path = "D:/GameDevGeral/FunnyPlacesQuestionnaire/QuestionnaireAnswer0.json";
        string basepath = "D:/GameDevGeral/FunnyPlacesQuestionnaire/QuestionnaireAnswer";
        int numfile = 0;
        string pathnum;
        while (System.IO.File.Exists(path))
        {
            numfile++;
            pathnum = numfile.ToString();
            path = basepath + pathnum + ".json";
        }
        System.IO.File.WriteAllText(path, json);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
