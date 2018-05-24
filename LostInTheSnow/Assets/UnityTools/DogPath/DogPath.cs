using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[ExecuteInEditMode]
public class DogPath : MonoBehaviour {
	public List<Transform> positions;
	private List<Transform[]> lines = new List<Transform[]> ();
	public void BuildPath(){
		Debug.Log ("Building DogPath");
		UpdateLines ();
		foreach (Transform[] line in lines) {
			NavMeshModifierVolume vol;
			if (line [0].gameObject.GetComponent<NavMeshModifierVolume> () == null)
				vol = line [0].gameObject.AddComponent<NavMeshModifierVolume> ();
			else
				vol = line [0].gameObject.GetComponent<NavMeshModifierVolume> ();
            float distance = Vector3.Distance(line[0].position, line[1].position);
			vol.area = 3;
			vol.size = new Vector3 (5, 20, (distance+1.5f)/2);
			vol.center = new Vector3 (0, -10, vol.size.z / 2);
			float angle = 180 + Mathf.Atan2 (line [0].position.x - line [1].position.x, line [0].position.z - line [1].position.z) * Mathf.Rad2Deg;
			line [0].eulerAngles = new Vector3 (0, angle, 0);

		}
	}
	public void ClearPath(){
		Debug.Log ("Clearing DogPath");
		UpdateLines ();
		foreach (Transform[] line in lines) {
			DestroyImmediate(line [0].gameObject.GetComponent<NavMeshModifierVolume> ());
		}
	}
	void OnDrawGizmos(){
		UpdateLines ();
		Gizmos.color = new Color(1, 0, 0);
		foreach (Transform t in positions) {
			Gizmos.DrawSphere (t.position, 1f);
		}
		foreach (Transform[] line in lines) {
			Gizmos.DrawLine (line [0].position, line [1].position);
		}
	}
	void UpdateLines(){
		lines.Clear ();
		for (int i = 1; i < positions.Count; i++) {
			Transform[] line = new Transform[2];
			line [0] = positions [i - 1];
			line [1] = positions [i];
			lines.Add (line);
		}
	}
}
