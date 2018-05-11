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
        animator.SetBool("Sit", true);
	}
	public override void UpdateAction(){
        if (animator.GetBool("Sit") && animator.GetCurrentAnimatorStateInfo(0).IsName("SitIdle"))
        {
            animator.SetBool("Sit", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("SitIdle"))
        {
            if (!isDone)
            {
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
        }
	}
	public override void EndAction(){
        animator.SetBool("Sit", false);
        dog.AddEffectToMood (moodEffect);
    }
}


