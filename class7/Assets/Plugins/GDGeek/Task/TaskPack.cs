using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class TaskPack : Task {
	    private Task task_ = null;
	    private bool isOver_ = false;
	    public delegate Task CreateTask();
	    public TaskPack(CreateTask createTask){
	     
	        this.init = delegate
	        {
	            isOver_ = false;
	            task_ = createTask();
				if(task_ == null){
					isOver_ = true;
				}else{
		            TaskManager.PushBack(task_, delegate {
		                isOver_ = true;
		            });
		            TaskManager.Run(task_);
				}
	        };
	        this.isOver = delegate {
	            return isOver_;
	        };
	       
	    }

	}
}
