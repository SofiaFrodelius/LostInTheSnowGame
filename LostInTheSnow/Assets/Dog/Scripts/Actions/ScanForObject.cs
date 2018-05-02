using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanForObject : DogAction {
	private GameObject foundObject;
	private bool grabableObject;
	private string tag;
	private float radius;
	private LayerMask layerMask;
	public ScanForObject(Dog d, float radius, string tag, bool grabableObject, LayerMask layerMask):base(d){
		this.grabableObject = grabableObject;
		this.tag = tag;
		this.radius = radius;
		this.layerMask = layerMask;
	}
	public override void StartAction(){
		isDone = false;
		List<Collider> colliders = GetCollidersWithTag(Physics.OverlapSphere(dog.transform.position, radius, layerMask), tag);
		if (colliders.Count > 0) {
			foundObject = colliders [Random.Range(0,colliders.Count)].gameObject;
		}
		isDone = true;
	}
	private List<Collider> GetCollidersWithTag(Collider[] col, string tag){
		List<Collider> cols = new List<Collider> ();
		foreach (Collider c in col) {
			if (c.tag == tag)
				cols.Add (c);
		}
		return cols;
	}
	public GameObject GetObject(){
		return foundObject;
	}
}
