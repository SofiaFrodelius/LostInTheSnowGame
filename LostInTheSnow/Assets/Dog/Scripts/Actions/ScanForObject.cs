using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScanForObject {
	private static GameObject foundObject;
	public static GameObject Scan(Vector3 position, float radius, string tag, LayerMask layerMask){
		foundObject = null;
		List<Collider> colliders = GetCollidersWithTag(Physics.OverlapSphere(position, radius, layerMask), tag);
		if (colliders.Count > 0) {
			foundObject = colliders [Random.Range(0,colliders.Count)].gameObject;
		}
		return foundObject;
	}
	private static List<Collider> GetCollidersWithTag(Collider[] col, string tag){
		List<Collider> cols = new List<Collider> ();
		foreach (Collider c in col) {
			if (c.tag == tag)
				cols.Add (c);
		}
		return cols;
	}
}
