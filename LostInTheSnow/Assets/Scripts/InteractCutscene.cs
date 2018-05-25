using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCutscene : MonoBehaviour, IInteractible
{
    [SerializeField] private GameObject cutsceneToTrigger;
    [SerializeField] private GameObject player;


    public void AlternateInteract()
    {

    }

    public void Interact()
    {
        Camera.main.gameObject.GetComponent<CameraController>().CutsceneLock = true;
        player.GetComponent<CharacterMovement>().CutsceneLock = true;
        GameObject temp = Instantiate(cutsceneToTrigger);
        player.transform.parent = temp.transform;
        temp.SetActive(true);
    }
}
