using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractPrompt : MonoBehaviour
{
    [SerializeField]
    private List<Text> promptToToggle;
    [SerializeField]
    private List<Text> dependencyPrompts;
    [SerializeField]
    private List<Item> itemDependencies;
    [SerializeField]
    private bool useDependencies;

    public void promptToggle(bool toggle) //can be used for prompts with and without dependencies
    {
        if (itemDependencies.Count > 0 && checkDependencies() || !useDependencies)
        {
            for (int i = 0; i < promptToToggle.Count; i++)
            {
                promptToToggle[i].enabled = toggle;
            }
        }
        else if (!checkDependencies() && useDependencies)
        {
            for (int i = 0; i < dependencyPrompts.Count; i++)
            {
                dependencyPrompts[i].enabled = toggle;
            }
        }
    }

    public void promptToggleSpecific(bool toggle, int id) //should not be used when prompts have dependencies
    {
        promptToToggle[id].enabled = toggle;
    }


    public void dependencyPromptToggleSpecific(bool toggle, int id) //should be used when prompts have dependencies
    {
        dependencyPrompts[id].enabled = toggle;
    }

    public void removePrompt(int id)
    {
        promptToToggle.RemoveAt(id);
    }

    public void disableAllPrompts()
    {
        for (int i = 0; i < promptToToggle.Count; i++)
        {
            promptToToggle[i].enabled = false;
        }
        for (int i = 0; i < dependencyPrompts.Count; i++)
        {
            dependencyPrompts[i].enabled = false;
        }

    }

    private bool checkDependencies()
    {
        Inventory inv;
        inv = Inventory.instance;
        if (!inv || itemDependencies.Count == 0) return false;
        for(int i = 0; i < itemDependencies.Count; i++)
        {
            if (inv.isItemInInventory(itemDependencies[i])) continue;
            else return false;
        }
        return true;
    }

    public void updatePrompts()
    {
            disableAllPrompts();
            promptToggle(true);
    }
}
