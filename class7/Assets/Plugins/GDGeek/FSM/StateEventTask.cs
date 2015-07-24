using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
public class StateEventTask{
	private FSM fsm_ = null;
	public StateEventTask(FSM fsm){
		fsm_ = fsm;
	}
	public Task postEvtToFSM(string msg){
		FSMEvent evt = new FSMEvent ();
		evt.msg = msg;
		return postEvtToFSM (evt);
	}
	public Task postEvtToFSM(FSMEvent evt){
		TaskWait wt = new TaskWait ();
		wt.setAllTime(0.1f)
		TaskManager.PushBack (wt, delegate {
			fsm_.postEvent(evt);
		});
		return (wt);
	}

}
*/