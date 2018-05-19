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
	public bool isSniffing = false;

	private DogAI ai;
	private Animator animator;
	private NavMeshAgent navAgent;
	private const float defaultSpeed = 8f;
	private ItemHand itemHand;
	private CharacterMovement characterMovement;

	void Start () {
		if (player == null)player = GameObject.FindGameObjectWithTag ("Player").transform;
		if (itemBone == null)itemBone = transform;
		if(terrain == null) terrain = Terrain.activeTerrain;
		ai = GetComponent<DogAI>();
		animator = GetComponent<Animator>();
		navAgent = GetComponent<NavMeshAgent> ();
		itemHand = player.GetComponentInChildren<ItemHand> ();
		characterMovement = player.GetComponent<CharacterMovement> ();
	}
	void Update(){
		RaycastHit hit;
		Ray ray = new Ray (transform.position, -Vector3.up);
		if (Physics.Raycast (ray, out hit))
			transform.eulerAngles = new Vector3 (Vector3.Angle (Vector3.up, hit.normal), transform.eulerAngles.y, transform.eulerAngles.z);
		if (itemHand.GetItemInHand () != null && itemHand.GetItemInHand ().name == "Stick") {
			if (currentAction == null || currentAction.GetImportance () == DogAction.Importance.LOW) {
				if (Vector3.Distance (transform.position, player.position) < 3) {
					ai.StartAction (new WaitForFetch (this));
				} else {
					ai.StartAction (new FollowPlayer (this, player));
				}
			}
		}
		if (currentAction != null) {
			currentAction.UpdateAction ();
		}
    }
	public void Interact(){
		
	}
	public void AlternateInteract(){
	}
	public bool IsIdle(){
		return currentAction == null;
	}
	public void Fetch(Transform stick = null) {
		if (stick != null)
			ai.StartAction (new Fetch (this, player, stick));
		else
			ai.StartAction (new Fetch (this, player));
	}
	public void Call(){
		if (characterMovement.CutsceneLock) 
			ai.StartAction (new Call (this, player, true));
		else 
			ai.StartAction (new Call (this, player));
	}
	public void Pet(){
		if(currentAction == null || currentAction.GetImportance() != DogAction.Importance.HIGH)
			ai.StartAction (new Pet (this));
	}
	public void PickupDog(){
		if(currentAction == null || currentAction.GetImportance() != DogAction.Importance.HIGH)
			ai.StartAction (new PickupDog (this));
	}
	public void ParentDog(){
		Debug.Log ("YaYAY");
		transform.parent = player.GetChild(2);
	}
	public void BreakLoose(){
		Debug.Log ("YaYAYBREAK");
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
