using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_AnimationControllerScript : MonoBehaviour
{
    private Animator animatorController;

    [Header("States")]
    public bool idle;
    public bool capoeira;
    public bool sentado;
    public bool dancing;
    public bool talking;
    public bool waving;
    public bool clapping;
    public bool cheering;
    public bool walking;
    public bool walkingBackwards;
    public int currentAnimationId=0;

    // Start is called before the first frame update
    void Start()
    {
        animatorController = GetComponent<Animator>();
        AnimationActivate();
        
    }

    // Update is called once per frame
    void Update()
    {
        AnimationActivate();
    }

    public void AnimationReplacer(int n) 
    {
        if (currentAnimationId == 0) 
        {
            idle = false;
        }
        else if (currentAnimationId == 1)
        {
            capoeira = false;
        }
        else if (currentAnimationId == 2)
        {
            sentado = false;
        }
        else if (currentAnimationId == 3)
        {
            dancing = false;
        }
        else if (currentAnimationId == 4)
        {
            talking = false;
        }
        else if (currentAnimationId == 5)
        {
            waving = false;
        }
        else if (currentAnimationId == 6)
        {
            clapping = false;
        }
        else if (currentAnimationId == 7)
        {
            cheering = false;
        }
        else if (currentAnimationId == 8)
        {
            walking = false;
        }
        else if (currentAnimationId == 9)
        {
            walkingBackwards = false;
        }

        if (n == 0)
        {
            idle = true;
        }
        else if (n== 1)
        {
            capoeira = true;
        }
        else if (n == 2)
        {
            sentado = true;
        }
        else if (n == 3)
        {
            dancing = true;
        }
        else if (n == 4)
        {
            talking = true;
        }
        else if (n == 5)
        {
            waving = true;
        }
        else if (n == 6)
        {
            clapping = true;
        }
        else if (n == 7)
        {
            cheering = true;
        }
        else if (n == 8)
        {
            walking = true;
        }
        else if (n == 9)
        {
            walkingBackwards = true;
        }
    }

    public void AnimationActivate() 
    {
        if (capoeira)
        {
            animatorController.SetBool("isCapoeira", true);
            currentAnimationId = 1;
        }
        else if (sentado)
        {
            animatorController.SetBool("isSentado", true);
            currentAnimationId = 2;
        }
        else if (dancing)
        {
            animatorController.SetBool("isDancing", true);
            currentAnimationId = 3;
        }
        else if (talking)
        {
            animatorController.SetBool("isTalking", true);
            currentAnimationId = 4;
        }
        else if (waving)
        {
            animatorController.SetBool("isWaving", true);
            currentAnimationId = 5;
        }
        else if (clapping)
        {
            animatorController.SetBool("isClapping", true);
            currentAnimationId = 6;
        }
        else if (cheering)
        {
            animatorController.SetBool("isCheering", true);
            currentAnimationId = 7;
        }
        else if (walking)
        {
            animatorController.SetBool("isWalking", true);
            currentAnimationId = 8;
        }
        else if (walkingBackwards)
        {
            animatorController.SetBool("isWalkingBackwards", true);
            currentAnimationId = 9;
        }
        else 
        {
            currentAnimationId = 0;
        }
    }
}
