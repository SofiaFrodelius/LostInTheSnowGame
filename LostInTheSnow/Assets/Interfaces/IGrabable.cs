using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IGrabable : IEventSystemHandler{
    Item getItemOnPickup();
    void Drop();
    void destroyItem();

}
