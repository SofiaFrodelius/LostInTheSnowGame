using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveTrees : MonoBehaviour 
{
	[SerializeField]
	private GameObject trees;
	[SerializeField]
	private GameObject cabin1;
	[SerializeField]
	private GameObject VoiceLine;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnDestroy()
	{
		if(trees != null)
		   {
			trees.active = false; 
		   }
		   if(cabin1 != null)
		   {
			cabin1.active = false; 
		   }
		   if(VoiceLine != null)
		   {
			VoiceLine.active = true; 
		   }
		  
	}
	
	
}
