using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePopUpMessage : MonoBehaviour
{
    public GameObject popUpToClose = null;

    public void ClosePopup(){
        popUpToClose.SetActive(false);
    }

}
