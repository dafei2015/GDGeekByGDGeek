using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace GDGeek{
	public class TaskSet : Task {
		private List<Task> tasks_ = new List<Task>();
		private int overCount_ = 0;
		public TaskSet(){
			this.init = delegate() {
				overCount_ = 0;
				for(int i = 0; i < this.tasks_.Count; ++i){
					Task task = this.tasks_[i] as Task;
					TaskManager.Run(task);
				}
			}; 
			this.isOver = delegate(){
				return (this.overCount_ == this.tasks_.Count);
			};
			
		}
		
		public void push(Task task){
			this.tasks_.Add (task);
			TaskManager.PushBack(task, delegate(){overCount_++;});
		}
	}
}