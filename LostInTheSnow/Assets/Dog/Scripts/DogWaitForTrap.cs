using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogWaitForTrap : MonoBehaviour {

    ScriptedDog sdog;
	void Start () {
        sdog = GameObject.FindGameObjectWithTag("Dog").GetComponent<ScriptedDog>();
	}
    void OnDestroy(){
        sdog.ForceStopSit();
    }
}
