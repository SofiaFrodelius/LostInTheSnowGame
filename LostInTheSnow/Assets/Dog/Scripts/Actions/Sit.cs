using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sit : DogAction {
	DogTimer timer;
	public Sit(Dog d, float time): base(d){
		moodState.ChangeMood (50f, 0f, 0f, 0f);
		moodEffect.ChangeMood (5f, 5f, 5f, 0f);
		timer = new DogTimer (time);
	}
	public override void StartAction(){	
        
		actionTimer = actionDelay;
		timer.ResetTimer ();
		isDone = false;
		animator.SetTrigger ("Sit");
	}
	public override void UpdateAction(){
        
        if (!isDone)
            if (!timer.IsDone())
            {
                timer.AddTime(Time.deltaTime);
            }
            else
            {
                isDone = true;
                animator.SetTrigger("StandUp");
            }
	}
	public override void EndAction(){
		dog.AddEffectToMood (moodEffect);
	}
}


