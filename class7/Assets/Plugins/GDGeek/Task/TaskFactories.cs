using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using TaskDelegate;
/*
public class TaskFactories : MonoBehaviour {
	private Hashtable taskMap_ = new Hashtable();
	
	public void registerTask(string key, Factory factory){
		this.taskMap_[key] = factory;
	}
	
	public void unregisterTask(string key){
		this.taskMap_.Remove(key);// = factory;
	}
	
	
	public bool hasTask(string key){
		return this.taskMap_.ContainsKey(key);
	}
	public Task createTask(string key){
		Factory func = this.taskMap_[key] as Factory;
		if(func != null)
			return func();
		Debug.LogError("can't find task '"+key+"'.");
		return new Task();
		
	}
}*/
