using UnityEngine;
using System.Collections;

namespace GDGeek{
	public class State {
		
        //定义一个委托可以根据名字获得该状态
		public delegate State GetCurrState(string name);
		private string name_ = "";
		protected string fatherName_ = "";
		public GetCurrState getCurrState = null;

		public string name{
			get{
				return name_;
			}
			set{
				name_ = value;
			}
		}

		public string fatherName{
			get{
				return fatherName_;
			}
			set{
				fatherName_ = value;
			}
		}

		public State(){
			
		}

        //状态开始时执行的动作
		public virtual void start(){
			
		}
        //状态结束时执行的动作
		public virtual void over(){
			
		}

		public virtual string postEvent(FSMEvent evt){
			return "";
		}
	}
}