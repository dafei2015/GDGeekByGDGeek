using UnityEngine;
using System.Collections;
namespace GDGeek{


	public class TaskState{
		//public 
		//private TaskDelegate.Factory _creater;
		protected static int index_ =  0; 
		//public delegate string NextState();

		static public StateWithEventMap Create(TaskFactory creater, FSM fsm, StateWithEventMap.StateAction nextState){
			string over = "over" + index_.ToString();
			index_++;
			StateWithEventMap state = new StateWithEventMap ();
			Task task = null;
			state.onStart += delegate {
				task = creater();
				TaskManager.PushBack (task, delegate {
					fsm.post(over);
				});
				TaskManager.Run (task);
			};
			state.onOver += delegate {
				task.isOver = delegate{
					return true;
				};
			};
			state.addAction (over, nextState);
			return state;
		}

		static public StateWithEventMap Create(TaskFactory creater, FSM fsm, string nextState){
			return Create (creater, fsm, delegate {
				return nextState;
			});
		}


		/*

		static public State Create(GDGeek.TaskDelegate.Factory creater, FSM fsm, string nextState){
			
			string over = "over" + index_.ToString();
			index_++;
			StateWithEventMap state = new StateWithEventMap ();
			state.onStart += delegate {
				Task task = creater();
				TaskManager.PushBack (task, delegate {
					Debug.Log ("!!" + over);
					fsm.post(over);
				});
				TaskManager.Run (task);
			};
			
			state.addEvent (over, nextState);
			return state;
		}

*/
		
	}
}