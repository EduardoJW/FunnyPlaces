using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPopUp : MonoBehaviour
{
    public GameObject popUpToOpen = null;

    public void OpenPopupMessage(){
        popUpToOpen.SetActive(true);
    }

}
