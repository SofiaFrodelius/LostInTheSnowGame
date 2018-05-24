using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class DoorInteract : MonoBehaviour, IInteractible
{
    bool hasKnocked = false;
    bool knockPrompt = true;
    [SerializeField]
    private int doorKnockId = 0;
    [SerializeField] private bool knockEnabled;
    [SerializeField] private StudioEventEmitter knockSound;
    [SerializeField] private StudioEventEmitter enterSound;
    [SerializeField] private StudioEventEmitter denialVoiceLine;
    [SerializeField] private int[] denialVoiceLineAlternatives;

    [SerializeField]
    private SceneSwitchScript sceneSwitcher = null;
    [SerializeField]
    private int targetSceneBuildIndex = 0;


    private int[] needMoreWoodId = { 9, 13, 26 }; //Hårdkodade voiceline ids

    private void Start()
    {
    }



    public void AlternateInteract()
    {
        if (knockEnabled)
        {
            if (!hasKnocked)
            {
                knockSound.Play();
                hasKnocked = true;
            }
            if (hasKnocked && knockPrompt)
            {
                knockPrompt = false;
                InteractPrompt ip;
                ip = transform.GetComponent<InteractPrompt>();
                ip.promptToggleSpecific(false, doorKnockId);
                ip.removePrompt(doorKnockId);
            }
        }
    }

    public void Interact()
    {
        ItemDependencies id = gameObject.GetComponent<ItemDependencies>();
        if (id)
        {
            if (id.CheckDependency(Inventory.instance))
            {
                sceneSwitcher.ActivateSceneSwitch(targetSceneBuildIndex);
            }
            else
            {
                if(denialVoiceLineAlternatives.Length > 0 && !denialVoiceLine.IsPlaying())
                {
                    int i = Random.Range(0, denialVoiceLineAlternatives.Length);

                    denialVoiceLine.Play();
                    denialVoiceLine.SetParameter("Voice Line", denialVoiceLineAlternatives[i]);
                }
            }
        }

        else
        {
            sceneSwitcher.ActivateSceneSwitch(targetSceneBuildIndex);
            if(enterSound)
                enterSound.Play();
        }

    }
}
