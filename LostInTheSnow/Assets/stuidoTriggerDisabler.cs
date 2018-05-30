using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class stuidoTriggerDisabler : MonoBehaviour {
    [SerializeField] private StudioEventEmitter emitter;
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
            if (emitter)
            {
                emitter.Stop();
            }
        }
    }
}
