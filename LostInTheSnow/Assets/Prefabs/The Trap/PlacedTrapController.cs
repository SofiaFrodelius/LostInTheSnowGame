using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedTrapController : MonoBehaviour {
    Rigidbody rb;
    Animator anim;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("IsOpen"))
        {
            anim.SetBool("ShouldOpen", false);
        }
		if(rb.velocity.magnitude == 0 && !rb.isKinematic)
        {
            rb.isKinematic = true;
            print("hej");
            anim.SetBool("ShouldOpen", true);
        }
	}
}
