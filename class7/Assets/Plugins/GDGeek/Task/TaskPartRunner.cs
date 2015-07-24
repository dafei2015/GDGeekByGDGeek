using UnityEngine;
using System.Collections;
namespace GDGeek{
	class TaskPartRunner : TaskRunner{
		private bool isLined_ = false;

		protected void Awake(){	
			if(TaskManager.GetInstance() != null){
				TaskManager.GetInstance().partRunner = this;
				isLined_ = true;
			}
			
		} 

		void Start(){
			if(!isLined_){
				TaskManager.GetInstance().partRunner = this;
				isLined_ = true;
			}
			
		}

		//protected override void Update() { 
		//	base.Update();
		//}

		protected void OnDestroy(){
			if(isLined_) 
			{
				TaskManager.GetInstance().partRunner = null; 
				isLined_ = false;
			}
			
		}
	}
}

