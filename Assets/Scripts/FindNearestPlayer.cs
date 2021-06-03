using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNearestPlayer : MonoBehaviour
{
    public GameObject indicationArrow;
    public float timeNearestPlayerIndicationStart=0.0f;
    public float maximumTimeNearestPlayerIndicationShowed = 10.0f;
    public float minimumDeltaTimeToAllowPlayerSearch = 30.0f;
    
    private int localPlayerNumber;
    public GameObject[] gamePlayersArray;

    // Start is called before the first frame update
    void Start()
    {
        gamePlayersArray = GameObject.FindGameObjectsWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf==true){
            if (!isNearestPlayerIndicationShown()){
                indicationArrow.SetActive(false);                
            }else{
                pointToNearestPlayer();
            }


        }
        
        
    }

    public void showNearestPlayer(){
        if ((timeNearestPlayerIndicationStart == 0.0) || ((Time.time - timeNearestPlayerIndicationStart) > minimumDeltaTimeToAllowPlayerSearch)){
            timeNearestPlayerIndicationStart = Time.time;
            indicationArrow.SetActive(true);
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
        float minimumDistance = 100000000.0f;
        GameObject localPlayer = gameObject.transform.parent.gameObject;
        Vector3 nearestPlayerPosition = localPlayer.transform.position;
        Vector3 differenceVector;
        foreach (GameObject player in gamePlayersArray){
            if ( localPlayer.transform.position  != player.transform.position){
                if (Vector3.Distance(localPlayer.transform.position,player.transform.position) < minimumDistance){
                    nearestPlayerPosition = player.transform.position;
                    differenceVector = localPlayer.transform.position - nearestPlayerPosition;
                    gameObject.transform.rotation = Quaternion.LookRotation(differenceVector.normalized);
                }
                
            }

        }
        
    }


}




