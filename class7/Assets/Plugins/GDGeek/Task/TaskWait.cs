using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class TaskWait : Task {

		private float allTime_ = 0f;
		private float time_ = 0f;

		public TaskWait(){
			this.init = initImpl;
			this.update = updateImpl;
			this.isOver = isOverImpl;
		}

		public void setAllTime(float allTime){
			allTime_ = allTime;
		}

		public void initImpl(){
			time_ = 0f;
		}

		public void updateImpl(float d){
			time_ += d;
		}

		public bool isOverImpl(){
			if (time_ >= allTime_) {
				return true;		
			}
			return false;
		}

	}
}
