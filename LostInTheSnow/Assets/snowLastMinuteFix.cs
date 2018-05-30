using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowLastMinuteFix : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ParticleSystem[] pss = other.transform.gameObject.GetComponentsInChildren<ParticleSystem>();
            foreach(ParticleSystem p in pss)
            {
                p.transform.parent = null;
            }
        }
    }
}
