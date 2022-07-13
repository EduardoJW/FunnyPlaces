using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Walking : MonoBehaviour
{
    public GameObject target1;
    public GameObject target2;
    public GameObject self;
    public float targetDistance1;
    public float targetDistance2;
    public float destinationDistance = 0;
    public float walkspeed;
    private float distance;
    public RaycastHit ray;
    public int currentTarget = 0;

    // Update is called once per frame
    void Update()
    {
       
        if (currentTarget == 1) 
        {
            self.transform.LookAt(target1.transform);
            distance = Vector3.Distance(self.transform.position, target1.transform.position);
            if (distance > 0.1)
            {
                walkspeed = 0.03f;
                self.GetComponent<NPC_AnimationControllerScript>().AnimationReplacer(8);
                self.transform.position = Vector3.MoveTowards(self.transform.position, target1.transform.position, walkspeed);
            }
            else 
            {
                walkspeed = 0;
                //mudar anim
                currentTarget = 2;
            }
            
        }
        else if (currentTarget == 2)
        {
            self.transform.LookAt(target2.transform);

            distance = Vector3.Distance(self.transform.position, target2.transform.position);
            if (distance > 0.1)
            {
                walkspeed = 0.03f;
                self.GetComponent<NPC_AnimationControllerScript>().AnimationReplacer(8);
                self.transform.position = Vector3.MoveTowards(self.transform.position, target2.transform.position, walkspeed);
            }
            else
            {
                walkspeed = 0;
                //mudar anim
                currentTarget = 1;
            }
            
        }
        
    }
}
