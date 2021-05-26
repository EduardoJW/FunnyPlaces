using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharType : MonoBehaviour
{
    public static int charactertype;
    // Start is called before the first frame update
    void Start()
    {
       DontDestroyOnLoad(this); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void choseType1(){
        charactertype = 1;
    }

    public void choseType2(){
        charactertype = 2;
    }



}
