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
        rb.velocity = new Vector3(0f, -0.01f, 0f);
	}
	
    private void FixedUpdate()
    {
        if (rb.velocity.magnitude == 0 && !rb.isKinematic)
        {
            rb.isKinematic = true;
            anim.SetTrigger("Open");
			anim.SetBool("ShouldOpen", true);
        }
    }
}
