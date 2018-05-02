using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IGrabable : IEventSystemHandler{
    void getItemOnPickup(out Item item);
    void Drop();

}
