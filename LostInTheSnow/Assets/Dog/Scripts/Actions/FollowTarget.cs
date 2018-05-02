using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowTarget : DogAction{
    Transform target;
	Vector3 offset;
    bool isAtTarget;
    bool doneAtTarget;
    float width = 1.5f;
	public FollowTarget(Dog d, Transform target,Vector3 offset, bool doneAtTarget = true, float width = 1.5f) : base(d){
        this.target = target;
        this.doneAtTarget = doneAtTarget;
		this.offset = offset;
		this.width = width;
    }
	public override void StartAction(){
		isDone = false;	
	}
    public override void UpdateAction(){
        if (!isDone){
			if (Vector2.Distance(new Vector2(dog.transform.position.x, dog.transform.position.z), new Vector2((target.position + offset).x, (target.position + offset).z)) > width){
                isAtTarget = false;
				Vector3 targetPos;
				if (dog.terrain != null)
					targetPos = new Vector3 (target.position.x, dog.terrain.SampleHeight (target.position), target.position.z);
				else
					targetPos = target.position;
				offset.y = 0;
				navAgent.SetDestination(targetPos + offset);
            }
            else{
                if (doneAtTarget)
                    isDone = true;
                isAtTarget = true;
            }
        }
    }
    public override void EndAction(){
        navAgent.ResetPath();
		isDone = true;
    }
	public void SetTarget(Transform target){
		this.target = target;
		StartAction ();
	}
	public void SetOffset(Vector3 offset){
		this.offset = offset;
		StartAction ();
	}
    public bool IsAtTarget(){
        return isAtTarget;
    }
}
