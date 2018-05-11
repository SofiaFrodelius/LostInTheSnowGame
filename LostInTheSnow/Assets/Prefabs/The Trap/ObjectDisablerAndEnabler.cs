using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisablerAndEnabler : MonoBehaviour {
    [SerializeField] private bool onTrigger;
    [SerializeField] private bool onDestroy;

    [SerializeField] private GameObject objectToDisable;
	[SerializeField] private GameObject objectToDisable_2;
    [SerializeField] private GameObject objectToEnable;
    private void OnDestroy()
    {
        if (onDestroy)
        {
            DisablerEnabler();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && onTrigger)
        {
            DisablerEnabler();
        }
    }
    
    void DisablerEnabler()
    {
        objectToDisable.SetActive(false);
		objectToDisable_2.SetActive(false);
        objectToEnable.SetActive(true);
    }
}
