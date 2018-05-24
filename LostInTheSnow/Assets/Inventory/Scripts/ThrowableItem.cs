using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThrowableItem : MonoBehaviour, IUsable
{
    Rigidbody rb;
    Inventory inventory;
    [SerializeField] private float throwForce = 20f;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Use(ItemHand ih)
    {
        inventory = Inventory.instance;
        //Debug.Log(ih.ActiveItem);
        Vector3 throwDirection = Camera.main.transform.forward;
        rb.isKinematic = false;
        rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        rb.AddRelativeTorque((new Vector3(0f, -1f, 0f)) * 10000);
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.transform.GetComponent<Collider>().enabled = true;
		rb.gameObject.layer = 0;
        transform.parent = null;
        inventory.removeHoldableItem(ih.getSelectedItem());
		GameObject.FindGameObjectWithTag ("Dog").GetComponent<Dog> ().Fetch (transform);
        //ih.ActiveItem = null;
    }
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("ColliderHit, Ha sönder snöboll och spawna partiklar");
    }
}

