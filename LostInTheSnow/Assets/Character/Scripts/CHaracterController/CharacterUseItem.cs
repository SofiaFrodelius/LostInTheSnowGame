using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterUseItem : MonoBehaviour {
    [SerializeField] private ItemHand ih;

    private void Start()
    {
        
    }
    void Update () {
        if (Input.GetButtonDown("Use"))
        {
            Use();
        }
    }
    void Use()
    {
       ExecuteEvents.Execute<IUsable>(ih.ActiveItem, null, (handler, eventData) => handler.Use(ih));
        
    }

}
