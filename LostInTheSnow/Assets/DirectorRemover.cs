using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DirectorRemover : MonoBehaviour {
    GameObject player;
    
	// Use this for initialization
	void Start () {
        if (transform.GetChild(0))
        {
            
            player = transform.GetChild(0).gameObject;
            player.GetComponent<CharacterMovement>().CutsceneLock = true;
            player.GetComponentInChildren<CameraController>().CutsceneLock = true;
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (PlayState.Paused == GetComponent<PlayableDirector>().state)
        {
            player.transform.parent = null;
            player.GetComponent<CharacterMovement>().CutsceneLock = false;
            player.GetComponentInChildren<CameraController>().CutsceneLock = false;
            Destroy(gameObject);
            Debug.Log("Thi");
        }
    }
}
