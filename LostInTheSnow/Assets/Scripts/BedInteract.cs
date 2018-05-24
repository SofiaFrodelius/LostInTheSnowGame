using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;


public class BedInteract : MonoBehaviour, IInteractible
{
    [SerializeField]
    private GameObject cutsceneToTrigger = null;
    [SerializeField]
    private GameObject player;
    [SerializeField] private StudioEventEmitter see;

    [SerializeField] private List<int> voiceLineIds; //11,12,16

    [SerializeField] private FirePlace fireplace;

    public void AlternateInteract()
    {

    }

    public void Interact()
    {
        if (fireplace.HasLitten)
        {
            Camera.main.gameObject.GetComponent<CameraController>().CutsceneLock = true;
            player.GetComponent<CharacterMovement>().CutsceneLock = true;
            GameObject temp = Instantiate(cutsceneToTrigger);
            player.transform.parent = temp.transform;
            temp.SetActive(true);
        }
        else
        {
            if (!see.IsPlaying())
            {
                //cant sleep its too cold voicelines
                int id = Random.Range(0, voiceLineIds.Count);
                see.Play();
                see.SetParameter("Voice Line", voiceLineIds[id]);
            }
        }
    }
}
