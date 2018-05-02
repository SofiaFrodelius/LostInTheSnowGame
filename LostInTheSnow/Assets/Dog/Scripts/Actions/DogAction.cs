using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class DogAction{
	protected int actionDelay;
	protected int actionTimer;
	protected Mood moodState;
	protected Mood moodEffect;
	protected Dog dog;
	protected NavMeshAgent navAgent;
	protected Animator animator;
    protected bool isDone;
	public enum Importance { LOW, MEDIUM, HIGH };
	protected Importance importance;
    public DogAction(Dog dog){
        this.dog = dog;
        navAgent = dog.GetComponent<NavMeshAgent>();
        animator = dog.GetComponent<Animator>();
		isDone = false;
		importance = Importance.MEDIUM;
    }
    public virtual void StartAction(){
		isDone = false;
    }
    public virtual void UpdateAction(){
    }
    public virtual void EndAction(){
    }
	public void DecrementActionTimer(){
		if(actionTimer != 0)
			actionTimer--;
	}
	public Importance GetImportance(){
		return importance;
	}
	public bool IsReady(){
		return actionTimer == 0;
	}
    public bool IsDone(){
        return isDone;
    }
	public Mood GetMoodState(){
		return moodState;
	}
	public Mood GetMoodEffect(){
		return moodEffect;
	}
}