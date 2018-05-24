using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDependencies : MonoBehaviour{
    [SerializeField] private Item[] itemsNeeded;
    [SerializeField] private int[] itemsAmount;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public bool CheckDependency(Inventory inv)
    {
        Debug.Log(itemsNeeded.Length);
        for(int i = 0; i < itemsNeeded.Length; i++)
        {
            if (!inv.isItemInInventory(itemsNeeded[i], itemsAmount[i]))
            {
                return false;
                
            }
        }
        return true;
    }

}
