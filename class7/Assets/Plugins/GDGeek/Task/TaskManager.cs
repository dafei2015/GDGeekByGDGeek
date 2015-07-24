using UnityEngine;
using System.Collections;

namespace GDGeek{

	public class TaskManager : MonoBehaviour {

		//public TaskFactories _factories = null;
		public TaskRunner _runner = null;
		
		private TaskRunner partRunner_  = null;
		private static TaskManager instance_ = null; 
		//private static Hashtable reserve_ = new Hashtable();
		
		public TaskRunner partRunner{
			set{this.partRunner_ = value as TaskRunner;}
		}
		
		void Awake(){

			TaskManager.instance_ = this;
			if (_runner == null) {
				_runner = this.gameObject.GetComponent<TaskRunner>();			
			}
			if (_runner == null) {
				_runner = this.gameObject.AddComponent<TaskRunner>();	
			}
		}
		
		public static TaskManager GetInstance(){
			return TaskManager.instance_;
		}
		
		public TaskRunner globalRunner{
			get{return _runner;}
		}
		
		public TaskRunner runner{
			get{
					if(partRunner_ != null){
						return partRunner_;
					}
					return _runner;
				}

		}
		
		public static void AddIsOver(Task task, TaskIsOver func){
			TaskIsOver oIsOver = task.isOver;
			task.isOver = delegate(){
				return (oIsOver() || func());
			};
		}
		public static void AddUpdate(Task task, TaskUpdate func){
			TaskUpdate update = task.update;
			task.update = delegate(float d){
				update(d);
				func(d);
			};
		}

		public static void PushBack(Task task, TaskShutdown func){
			TaskShutdown oShutdown = task.shutdown;
			task.shutdown = delegate (){
				oShutdown();
				func();
			};
		}
		
	
		public static void Run(Task task){
			if(TaskManager.GetInstance() != null){
				TaskManager.GetInstance().runner.addTask(task);
			}
		}

		public static void PushFront(Task task, TaskInit func){
			TaskInit oInit = task.init;
			task.init = delegate(){
				func();
				oInit();
			};
		}
	}
}
