using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObject : MonoBehaviour, IGrabable {
    [SerializeField]
    private Item itemType;

    public void getItemOnPickup(out Item item)
    {
        //all i want from c# is pointers :(
        item = itemType;

        Destroy(gameObject);

    }

    public void Drop()
    {

    }



    //public GameObject Grab(GameObject parent){
    //	print ("GRABS ITEM");
    //	/* SHIT */
    //	Quaternion rot = transform.localRotation;
    //	transform.parent = parent.transform;
    //	transform.localPosition = Vector3.zero;
    //	transform.localRotation = rot;
    //	return gameObject;
    //}
    //public void Drop(){
    //	print ("DROPS ITEM");
    //	transform.parent = null;
    //}
}
