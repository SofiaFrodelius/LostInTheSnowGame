using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingARound : DogAction {
	Transform rosie;
	Vector3 offset;
	float orbitRadius;
	int orbitStatus;
	FollowTarget followTarget;
	public RingARound(Dog d, Transform rosie, float orbitRadius) : base(d){
		this.rosie = rosie;
		this.orbitRadius = orbitRadius;
	}
	public override void StartAction(){
		isDone = false;
		orbitStatus = 0;
		SwitchTarget ();
		followTarget = new FollowTarget (dog, rosie, offset, true, 0.2f);
	}
	public override void UpdateAction(){
		if (!isDone) {
			if (followTarget.IsDone ()) {
				SwitchTarget ();
				followTarget.SetOffset (offset);
			} else
				followTarget.UpdateAction ();
		}
	}
	public override void EndAction(){}
	void SwitchTarget(){
		switch (orbitStatus) {
		case 0:
			offset =  rosie.transform.forward * orbitRadius;
			orbitStatus++;
			break;
		case 1:
			offset = -rosie.transform.right * orbitRadius;
			orbitStatus++;
			break;
		case 2:
			offset = -rosie.transform.forward * orbitRadius;
			orbitStatus++;
			break;
		case 3:
			offset = rosie.transform.right * orbitRadius;
			orbitStatus = 0;
			break;
		}
	}
}
