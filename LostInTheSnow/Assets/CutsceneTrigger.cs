using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject cutsceneToTrigger = null;

    [SerializeField]
    private bool disableCamera, disableMovement, disableInteract;

    private GameObject player;
    bool hasTriggered = false;
    Vector3 scale;




    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !hasTriggered)
        {
            scale = other.transform.localScale;
            player = other.gameObject;
            if(disableMovement)
                other.gameObject.GetComponent<CharacterMovement>().CutsceneLock = true;
            if(disableCamera)
                other.gameObject.GetComponentInChildren<CameraController>().CutsceneLock = true;

            cutsceneToTrigger.transform.position = player.transform.position;
            cutsceneToTrigger.transform.rotation = player.transform.rotation;
            player.transform.parent = cutsceneToTrigger.transform;

            cutsceneToTrigger.GetComponent<PlayableDirector>().Play();
            Debug.Log("hello");
            StartCoroutine(Remover());


        }
    }
    IEnumerator Remover()
    {
        bool isDone = false;
        while (!isDone)
        {
            if(PlayState.Paused == cutsceneToTrigger.GetComponent<PlayableDirector>().state)
            {
                isDone = !isDone;
            }
            yield return null;
        }

        player.transform.parent = null;

        player.gameObject.GetComponent<CharacterMovement>().CutsceneLock = false;
        player.gameObject.GetComponentInChildren<CameraController>().CutsceneLock = false;

        Vector3 tmp = player.transform.eulerAngles;
        tmp.x = 0;
        tmp.z = 0;
        player.transform.eulerAngles = tmp;
        player.transform.localScale = scale;
        Destroy(gameObject);

    }
}
