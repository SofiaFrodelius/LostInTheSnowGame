using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSceneSwitch : MonoBehaviour, IInteractible
{
    [SerializeField]
    private SceneSwitchScript sceneSwitcher = null;
    [SerializeField]
    private int targetSceneBuildIndex = 0;

    public void Interact()
    {
        sceneSwitcher.ActivateSceneSwitch(targetSceneBuildIndex);
    }
}
