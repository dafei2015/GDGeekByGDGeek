using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek{
	public class StateWithEventMap:State {
		public delegate void Action();
		public delegate void EvtAction(FSMEvent evt);
		public delegate string StateAction(FSMEvent evt);

		private Dictionary<string, StateAction> actionMap_ = new Dictionary<string,StateAction>();
	
		public event Action onOver;
		public event Action onStart;
		
		public void addEvent(string evt, string nextState){
			actionMap_.Add (evt, delegate {
								return nextState;
							});
		}
		
		public void addAction(string evt, StateAction action){
			actionMap_.Add (evt, action);
		}
		public void addAction(string evt, EvtAction action){
			actionMap_.Add (evt, delegate(FSMEvent e) {
				action(e);
				return "";
			});
		}
	
		public override string postEvent(FSMEvent evt){
			string ret = "";
			if(actionMap_.ContainsKey(evt.msg)){
				ret = actionMap_[evt.msg](evt);
			}
			return ret;			

		}
		public override void start ()
		{
			if(onStart != null)
				onStart ();
		}
		public override void over ()
		{
			if(onOver != null)
				onOver ();
		}
	}
}