using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(DogAI))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(DogLocomotion))]
public class Dog : MonoBehaviour, IInteractible {
	public LayerMask dogLayerMask;
	public Transform player;
	public Transform itemBone;
	public Terrain terrain;
	public Mood currentMood;

	[Header("Debug Tools")]
	public Transform TestWaypoint;
	public float forwardd;
	public float rightd;
    public Item grabbedItem;
    public GameObject itemObject;
	public DogAction currentAction;

	private DogAI ai;
	private Animator animator;
	private NavMeshAgent navAgent;
	private const float defaultSpeed = 8f;

	void Start () {
		if (player == null)player = GameObject.FindGameObjectWithTag ("Player").transform;
		if (itemBone == null)itemBone = transform;
		if(terrain == null) terrain = Terrain.activeTerrain;
		ai = GetComponent<DogAI>();
		animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }
	void Update(){
		if (currentAction != null) {
			Debug.Log (currentAction.ToString ());
			currentAction.UpdateAction ();
		} else {
			Debug.Log ("No action");
		}
    }
	public void Interact(){
		
	}
	public bool IsIdle(){
		return currentAction == null;
	}
	public void Call(Transform player){
		ai.StartAction (new Call (this, player));
	}
	public void Pet(){
		if(currentAction == null || currentAction.GetImportance() != DogAction.Importance.HIGH)
			ai.StartAction (new Pet (this));
	}
	public void PickupDog(){
		ai.StartAction (new PickupDog (this));
	}
	public void ParentDog(){
		transform.parent = player;
	}
	public void BreakLoose(){
		transform.parent = null;
	}
	public void AddEffectToMood(Mood effect){
		currentMood = currentMood + effect;
	}
    public Item GrabbedItem{
        get{ return grabbedItem; }
        set{
            if (value != null) { 
				DropGrabbedItem();
                grabbedItem = value;
				itemObject = Instantiate(grabbedItem.getAssociatedGameobject(), itemBone) as GameObject;
				itemObject.transform.parent = itemBone;
                itemObject.GetComponent<Rigidbody>().isKinematic = true;
                itemObject.transform.localPosition = Vector3.zero;
                itemObject.transform.localRotation = Quaternion.identity;
			}
        }
    }
    public void DropGrabbedItem(){
        if(itemObject != null){
			itemObject.transform.parent = null;
            itemObject.GetComponent<Rigidbody>().isKinematic = false;
			grabbedItem = null;
			itemObject = null;
        }
    }
	public float GetDefaultSpeed(){
		return defaultSpeed;
	}
}
