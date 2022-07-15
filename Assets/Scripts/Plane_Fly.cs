using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_Fly : MonoBehaviour
{
    public GameObject self;
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject target4;
    public GameObject target5;
    public GameObject target6;
    public GameObject targetMid;
    public GameObject target7;
    public GameObject target8;
    public GameObject target9;
    public GameObject target10;
    public GameObject target11;
    public GameObject target12;
    public GameObject target13;
    public GameObject target14;
    public GameObject target15;
    public GameObject target16;
    public GameObject targetEnd;
    public GameObject targetStart;
    public float targetDistance1;
    public float targetDistance2;
    public float destinationDistance = 0;
    public float walkspeed = 0.3f;
    public float walkspeedbase = 0.3f;
    private float distance;
    public RaycastHit ray;
    public int currentTarget = 0;
    public int TargetNum = 19;
    public GameObject[] targetList;
    public bool isFlying = false;
    // Start is called before the first frame update
    void Start()
    {
        targetList = new GameObject[19];
        targetList[0]=target1;
        targetList[1]=target2;
        targetList[2] = target3;
        targetList[3] = target4;
        targetList[4] = target5;
        targetList[5] = target6;
        targetList[6] = targetMid;
        targetList[7] = target7;
        targetList[8] = target8;
        targetList[9] = target9;
        targetList[10] = target10;
        targetList[11] = target11;
        targetList[12] = target12;
        targetList[13] = target13;
        targetList[14] = target14;
        targetList[15] = target15;
        targetList[16] = target16;
        targetList[17] = targetEnd;
        targetList[18] = targetStart;

    }

    // Update is called once per frame
    void Update()
    {
        if (isFlying) { 
            if (currentTarget < TargetNum)
            {
                FlyTowardsTarget(currentTarget);
            }
        }
    }
    
    void FlyTowardsTarget(int target) 
    {
        self.transform.LookAt(targetList[target].transform);
        distance = Vector3.Distance(self.transform.position, targetList[target].transform.position);
        if (distance > 0.1)
        {
            walkspeed = walkspeedbase;
            self.transform.position = Vector3.MoveTowards(self.transform.position, targetList[target].transform.position, walkspeed);
        }
        else
        {
            walkspeed = 0;
            //mudar anim
            currentTarget++;
        }
    }
}
