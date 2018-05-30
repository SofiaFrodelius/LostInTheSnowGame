using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAnimationBoolOnStart : MonoBehaviour {
    [SerializeField] private string booleanName;
    [SerializeField] private bool condition;
	// Use this for initialization
	void Start () {
        GetComponent<Animator>().SetBool(booleanName, condition);
	}

}
