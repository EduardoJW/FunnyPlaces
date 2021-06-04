using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindNearestPlayer : MonoBehaviour
{
    private GameObject indicationArrow;
    public float timeNearestPlayerIndicationStart=0.0f;
    public float maximumTimeNearestPlayerIndicationShowed = 20.0f;
    public float minimumDeltaTimeToAllowPlayerSearch = 40.0f;
    
    private int localPlayerNumber;
    public GameObject[] gamePlayersArray;

    // Start is called before the first frame update
    void Start()
    {
        indicationArrow = gameObject.transform.root.gameObject.transform.Find("Arrow").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        if (indicationArrow.activeSelf==true){
            if (!isNearestPlayerIndicationShown()){
                indicationArrow.SetActive(false);                
            }else{
                pointToNearestPlayer();
            }
        }

        if (gameObject.GetComponent<Button>().interactable == false){
            if ((Time.time-timeNearestPlayerIndicationStart) >= minimumDeltaTimeToAllowPlayerSearch){
                gameObject.GetComponent<Button>().interactable = true;
            }
        }
        
    }

    public void showNearestPlayer(){
        if ((timeNearestPlayerIndicationStart == 0.0) || ((Time.time - timeNearestPlayerIndicationStart) > minimumDeltaTimeToAllowPlayerSearch)){
            timeNearestPlayerIndicationStart = Time.time;
            pointToNearestPlayer();
            indicationArrow.SetActive(true);
            gameObject.GetComponent<Button>().interactable = false;
        }

    }
    public bool isNearestPlayerIndicationShown(){
        if ((Time.time - timeNearestPlayerIndicationStart) <= maximumTimeNearestPlayerIndicationShowed){
            return true;
        }else{
            return false;
        }
    }

    public void pointToNearestPlayer(){
        gamePlayersArray = GameObject.FindGameObjectsWithTag("Player");
        float minimumDistance = 100000000.0f;
        GameObject localPlayer = gameObject.transform.root.gameObject;
        float distanceDifference;
        Vector3 differenceVector;
        int indiceMenorDistancia=0;
        for (int indicePlayer =0;indicePlayer < gamePlayersArray.Length;indicePlayer++){
            if ( localPlayer.transform.position  != gamePlayersArray[indicePlayer].transform.position){
                distanceDifference = Vector3.Distance(localPlayer.transform.position,gamePlayersArray[indicePlayer].transform.position);
                if (distanceDifference < minimumDistance){
                    minimumDistance = distanceDifference;
                    indiceMenorDistancia = indicePlayer;
                }
            }
        }
        differenceVector = gamePlayersArray[indiceMenorDistancia].transform.position-localPlayer.transform.position;
        indicationArrow.transform.rotation = Quaternion.LookRotation(differenceVector.normalized);
                
        
    }


}




