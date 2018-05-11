using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : DogAction
{
    DogTimer timer;
    public Rest(Dog d, float time) : base(d)
    {
        timer = new DogTimer(time);
    }
    public override void StartAction()
    {
        actionTimer = actionDelay;
        timer.ResetTimer();
        isDone = false;

        animator.SetBool("Rest", true);
    }
    public override void UpdateAction()
    {
        if (animator.GetBool("Rest") && animator.GetCurrentAnimatorStateInfo(0).IsName("RestIdle"))
        {
            animator.SetBool("Rest", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("RestIdle"))
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
    public override void EndAction()
    {
        animator.SetBool("Rest", false);
        dog.AddEffectToMood(moodEffect);
    }

}
