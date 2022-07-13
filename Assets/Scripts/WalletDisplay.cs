using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WalletDisplay : MonoBehaviour
{
    private int walletMoney = 2000;
    public Text walletMoneyText;

    // Update is called once per frame
    void Update()
    {
        walletMoney = gameObject.GetComponent<PlayerControler>().walletMoneyValue;
        walletMoneyText.text = walletMoney.ToString();

    }
}
