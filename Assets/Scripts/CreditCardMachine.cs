using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditCardMachine : MonoBehaviour
{
    public Text creditCardMachineDisplay;

    // Start is called before the first frame update
    void Start()
    {
        creditCardMachineDisplay.GetComponent<Text>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DigitarTecla(string valor){
        switch(valor){
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
            case "6":
            case "7":
            case "8":
            case "9":
            case "0":
                creditCardMachineDisplay.GetComponent<Text>().text = creditCardMachineDisplay.GetComponent<Text>().text+valor;
                break;
            case "asterisco":
                creditCardMachineDisplay.GetComponent<Text>().text = creditCardMachineDisplay.GetComponent<Text>().text+ valor;
                break;
            case "tralha":
                creditCardMachineDisplay.GetComponent<Text>().text = creditCardMachineDisplay.GetComponent<Text>().text+valor;
                break;
            case "clear":
                creditCardMachineDisplay.GetComponent<Text>().text = "";
                break;
            case "backspace":
                creditCardMachineDisplay.GetComponent<Text>().text = creditCardMachineDisplay.GetComponent<Text>().text.Remove(creditCardMachineDisplay.GetComponent<Text>().text.Length -1);
                break;
            case "confirm":
                gameObject.SetActive(false);
                break;

        }

    }
}
