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
        walletMoneyText.text = walletMoney.ToString();
        if (Input.GetKeyDown(KeyCode.Space)){
            walletMoney = walletMoney - 100;

        }

    }
}
