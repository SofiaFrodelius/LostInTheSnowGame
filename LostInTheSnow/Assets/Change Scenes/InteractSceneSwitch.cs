using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSceneSwitch : MonoBehaviour, IInteractible
{
    [SerializeField]
    private SceneSwitchScript sceneSwitcher = null;
    [SerializeField]
    private bool active = true;
    [SerializeField]
    private int targetSceneBuildIndex = 0;
    public bool Active
    {
        set
        {
            active = value;
        }
    }
    public void Interact()
    {
        if (active == true)
        {
            sceneSwitcher.ActivateSceneSwitch(targetSceneBuildIndex);
        }

    }
	
	public void AlternateInteract()
	{
		
	}
}
