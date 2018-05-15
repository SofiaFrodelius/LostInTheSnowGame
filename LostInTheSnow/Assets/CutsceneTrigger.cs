using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject cutsceneToTrigger = null;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private bool disableCamera, disableMovement, disableInteract;

    bool hasTriggered = false;



	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}



    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !hasTriggered)
        {
            
            GameObject temp = Instantiate(cutsceneToTrigger);
            player.transform.parent = temp.transform;
            temp.SetActive(true);
            Debug.Log("hello");
        }
    }
}
