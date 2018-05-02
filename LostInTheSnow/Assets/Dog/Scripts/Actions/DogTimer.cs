using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTimer {
	private float time;
	private float currentTime;
	private bool isDone = false;
	public DogTimer(float time){
		this.time = time;
		this.currentTime = currentTime;
	}
	public void AddTime(float t){
		if (time < 0)
			return;
		if (currentTime > time) 
			isDone = true;
		else
			currentTime += t;
	}
	public void ResetTimer(){
		currentTime = 0;
		isDone = false;
	}
	public bool IsDone(){
		return isDone;
	}
}
