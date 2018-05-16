using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSceneSwitch : MonoBehaviour, IInteractible
{
    [SerializeField]
    private SceneSwitchScript sceneSwitcher = null;
    [SerializeField]
    private bool Active = true;
    [SerializeField]
    private int targetSceneBuildIndex = 0;

    public void Interact()
    {
        if (Active == true)
        {
            sceneSwitcher.ActivateSceneSwitch(targetSceneBuildIndex);
        }

    }
}
