using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDependencies : MonoBehaviour{
    [SerializeField] private bool antingenEller = true;

    [SerializeField] private Item[] itemsNeeded;
    [SerializeField] private int[] itemsAmount;


	// Update is called once per frame
	void Update () {
		
	}
    public bool CheckDependency(Inventory inv)
    {

        if (antingenEller)
        {
            for(int i = 0; i < itemsNeeded.Length; i++)
            {
                if(inv.isItemInInventory(itemsNeeded[i], itemsAmount[i]))
                {
                    return true;
                }
            }
            return false;
        }
        else
        {
            for (int i = 0; i < itemsNeeded.Length; i++)
            {
                if (!inv.isItemInInventory(itemsNeeded[i], itemsAmount[i]))
                {
                    return false;
                }
            }
            return true;
        }

    }

}
