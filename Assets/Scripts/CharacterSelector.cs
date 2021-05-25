using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;



public class CharacterSelector : MonoBehaviour
{
    private const string playerImagePrefix ="PlayerType";
    private const int startingCharNumber = 1;
    private const int maxCharTypes = 8;
    [SerializeField] private Button nextCharButton = null;
    [SerializeField] private Button previousCharButton = null;
    private int charID = startingCharNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().sprite=Resources.Load<Sprite>("Sprites/" + playerImagePrefix + charID.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextChar(){
        charID++;
        if (charID > 8) charID =1;
        gameObject.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/" + playerImagePrefix + charID.ToString());
    }

    public void previousChar(){
        charID--;
        if (charID < 1) charID =8;
        gameObject.GetComponent<Image>().sprite =Resources.Load<Sprite>("Sprites/" + playerImagePrefix + charID.ToString());
    }



}
