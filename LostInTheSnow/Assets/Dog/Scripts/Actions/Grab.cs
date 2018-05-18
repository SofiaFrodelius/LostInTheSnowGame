using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Grab : DogAction{
    Item item;
    GameObject itemObject;
    public Grab(Dog d, GameObject itemObject) : base(d){
        this.itemObject = itemObject;
    }
    public override void StartAction(){
		isDone = false;
        ExecuteEvents.Execute<IGrabable>(itemObject, null, GrabEvent);
        isDone = true;
    }
    public void GrabEvent(IGrabable handler, BaseEventData baseEvent){
		animator.SetTrigger ("DogPickup");
        item = handler.getItemOnPickup();
        handler.destroyItem();
        dog.GrabbedItem = item;
    }
}
