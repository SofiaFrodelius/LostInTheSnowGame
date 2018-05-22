using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DogGetStickEvent : MonoBehaviour {
	public GameObject stick;
	void OnTriggerEnter(Collider other) {
		Dog dog = GameObject.FindGameObjectWithTag ("Dog").GetComponent<Dog> ();
		dog.Fetch (stick.transform);
		Destroy (this);
	}
}
