using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbob : MonoBehaviour
{
    private Animator anim;
    private CharacterController cc;
    private CharacterMovement cm;
    private int bobIsOn;



	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        cc = transform.parent.GetComponent<CharacterController>();
        cm = transform.parent.GetComponent<CharacterMovement>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        bobIsOn = PlayerPrefs.GetInt("Headbob"); //this should only happen once when setting is changed in options, not every frame
        if (bobIsOn == 1)
        {
            if (cc.velocity.x != 0 && cc.isGrounded || cc.velocity.z != 0 && cc.isGrounded)
            {
                 anim.SetBool("IsWalking", true);
             }
            else
            {
                anim.SetBool("IsWalking", false);
            }

            if(cm.getSprint() && cc.isGrounded)
            {
                anim.SetBool("IsSprinting", true);
            }
            else
            {
                anim.SetBool("IsSprinting", false);
            }

            if((anim.GetBool("IsSprinting") || anim.GetBool("IsWalking")) && cm.CutsceneLock)
            {
                anim.SetBool("IsSprinting", false);
                anim.SetBool("IsWalking", false);
            }
        }

        else
        {
            if (cc.velocity.x != 0 && cc.isGrounded || cc.velocity.z != 0 && cc.isGrounded)
            {
                anim.SetBool("IsWalkingNoAnim", true);
            }
            else
            {
                anim.SetBool("IsWalkingNoAnim", false);
            }

            if (cm.getSprint() && cc.isGrounded)
            {
                anim.SetBool("IsSprintingNoAnim", true);
            }
            else
            {
                anim.SetBool("IsSprintingNoAnim", false);
            }

            if ((anim.GetBool("IsSprintingNoAnim") || anim.GetBool("IsWalkingNoAnim")) && cm.CutsceneLock)
            {
                anim.SetBool("IsSprintingNoAnim", false);
                anim.SetBool("IsWalkingNoAnim", false);
            }
        }

    }

    public Animator getAnimator()
    {
        return anim;
    }


}
