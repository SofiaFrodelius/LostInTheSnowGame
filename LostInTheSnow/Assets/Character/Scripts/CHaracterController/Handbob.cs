using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handbob : MonoBehaviour
{
    private Animator anim;
    public CharacterController cc;
    private int bobIsOn;
	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        bobIsOn = 1;// PlayerPrefs.GetInt("Headbob");
        if (bobIsOn == 1) { 
            if (cc.velocity.x != 0 && cc.isGrounded || cc.velocity.z != 0 && cc.isGrounded)
            {
                 anim.SetBool("IsWalking", true);
             }
            else
            {
                anim.SetBool("IsWalking", false);
            }

            if(cc.transform.GetComponent<CharacterMovement>().getSprint() && cc.isGrounded)
            {
                anim.SetBool("IsRunning", true);
            }
            else
            {
                anim.SetBool("IsRunning", false);
            }
        }
    }

    public Animator getAnimator()
    {
        return anim;
    }


}
