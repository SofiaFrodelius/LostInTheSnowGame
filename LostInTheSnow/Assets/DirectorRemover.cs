using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DirectorRemover : MonoBehaviour {
    GameObject player;
    Vector3 lastFrameRot;
    // Use this for initialization
    void Start () {
        lastFrameRot = new Vector3(0, 0, 0);
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "Player")
            {

                player = transform.GetChild(0).gameObject;
                player.GetComponent<CharacterMovement>().CutsceneLock = true;
                player.GetComponentInChildren<CameraController>().CutsceneLock = true;
                break;
            }
        }

	}
	
	// Update is called once per frame
	void Update () {

        if (player)
        {
            lastFrameRot = player.transform.localEulerAngles;

            if (PlayState.Paused == GetComponent<PlayableDirector>().state)
            {
                player.transform.parent = null;
                player.GetComponent<CharacterMovement>().CutsceneLock = false;
                player.GetComponentInChildren<CameraController>().CutsceneLock = false;
                Destroy(gameObject);
                //Debug.Log("Thi");
            }
        }
    }
    private void OnDestroy()
    {
                    //player.transform.eulerAngles = lastFrameRot;
    }
}
