using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace GDGeek{

	public class TaskList:Task{
		private Task begin_ = null;
		private Task end_ = null;
		private List<Task> other_ = new List<Task>(); 
		private bool isOver_ = false;
		private bool isCompleted_ = false;

		public TaskList(){
			this.init = this.initImpl;
			this.isOver = this.isOverImpl;
		}

		public void push(Task task){
			if(this.isCompleted_){
				Debug.LogError("list task is completed!");
			}
			if(this.begin_ == null && this.end_ == null)
			{
				this.begin_ = task;
				this.end_ = task;
			}else{
				Task end = this.end_;
				TaskShutdown oShutdown = end.shutdown;
				end.shutdown = delegate(){
					oShutdown();
					TaskManager.Run(task);
				};
				other_.Add(this.end_);
				this.end_ = task;
			}
		}

		public void initImpl(){
			if(this.isCompleted_ == false && this.end_!=null){
				TaskManager.PushBack(this.end_, delegate(){this.isOver_ = true;});
				this.isCompleted_ = true;
			}
			if(this.begin_ != null){ 
				this.isOver_ = false;
				TaskManager.Run(begin_); 
			}else{
				this.isOver_ = true;
			} 
		}
		
		
		public bool isOverImpl(){
			return this.isOver_;
		}
	};
}
