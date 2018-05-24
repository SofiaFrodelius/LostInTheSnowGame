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
    [SerializeField] private StudioEventEmitter needMoreWood;
    [SerializeField] private Item wood;
    [SerializeField] private int woodNeeded;

    [SerializeField]
    private SceneSwitchScript sceneSwitcher = null;
    [SerializeField]
    private int targetSceneBuildIndex = 0;


    private int[] needMoreWoodId = { 9, 13, 26 }; //Hårdkodade voiceline ids
    Inventory inv;

    private void Start()
    {
        inv = Inventory.instance;
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
        if (inv && !inv.isItemInInventory(wood, woodNeeded))
        {
            if (!needMoreWood.IsPlaying())
            {
                int i = Random.Range(0, needMoreWoodId.Length);

                needMoreWood.Play();
                needMoreWood.SetParameter("Voice Line", needMoreWoodId[i]);
            }

        }
        else
        {
            sceneSwitcher.ActivateSceneSwitch(targetSceneBuildIndex);
            enterSound.Play();
        }

    }
}
