using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsEnableDisable : MonoBehaviour
{
	private List<GameObject> items = new List<GameObject>();
    [SerializeField] private bool itemsEnabled;



	private void Start()
	{
		items.Clear();
		items.AddRange(GameObject.FindGameObjectsWithTag("ItemsCabin"));
	}
	
    private void OnDestroy()
    {
        for(int i = 0; i < items.Count; i++)
        {
            items[i].SetActive(itemsEnabled);
        }
    }
}
