using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bark : DogAction
{
    public Bark(Dog d) : base(d)
    {
    }
    public override void StartAction()
    {
        isDone = false;
        animator.SetTrigger("Bark");
    }
    public override void UpdateAction()
    {

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f)
            isDone = true;
    }
    public override void EndAction()
    {
        isDone = true;
    }
}
