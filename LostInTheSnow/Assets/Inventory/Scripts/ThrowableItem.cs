using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class ThrowableItem : MonoBehaviour, IUsable {
    Rigidbody rb;
	[SerializeField]private float throwForce = 20f;
	void Awake(){
		rb = GetComponent<Rigidbody> ();
	}
    public void Use(ItemHand ih){
        Vector3 throwDirection = Camera.main.transform.forward;
		rb.isKinematic = false;
		rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        rb.AddRelativeTorque((Vector3.down) * 10);
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        transform.parent = null;
        ih.ActiveItem = null;
    }
    void OnCollisionEnter(Collision collision){
		Debug.Log (name + " hit something");
        //Debug.Log("ColliderHit, Ha sönder snöboll och spawna partiklar");
    }
}

