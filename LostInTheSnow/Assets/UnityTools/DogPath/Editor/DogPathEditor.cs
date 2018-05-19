using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DogPath))]
public class DogPathEditor : Editor {
	public override void OnInspectorGUI(){
		DrawDefaultInspector ();
		GUILayout.Space (5);
		DogPath myScript = (DogPath)target;
		if (GUILayout.Button ("Build Path"))
			myScript.BuildPath ();
		if (GUILayout.Button ("Clear Path"))
			myScript.ClearPath ();		
	}
}
