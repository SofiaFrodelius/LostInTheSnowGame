using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAwayTrees : MonoBehaviour {
	[SerializeField]
	private GameObject ttc;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	   public void OnTriggerEnter(Collider other)
	   {
		   if(ttc != null)
		   {
		   ttc.active = false;
		   }
	   }
}
