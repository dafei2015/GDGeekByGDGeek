using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek{
	public class FSM {

		private Dictionary<string, State> states_ = new Dictionary<string, State>();
		private ArrayList currState_ = new ArrayList();

		public FSM(){
			State root = new State();
			root.name = "root";
			this.states_["root"]  = root;
			this.currState_.Add(root);
		}
		public void addState(string stateName, State state){
			this.addState (stateName, state, "");		
		}

        /// <summary>
        /// 状态的注册
        /// </summary>
        /// <param name="stateName"></param>
        /// <param name="state"></param>
        /// <param name="fatherName"></param>
		public void addState(string stateName, State state, string fatherName){	
			if(fatherName == ""){
				state.fatherName = "root";
			}
			else{
				state.fatherName = fatherName;
			}

            //根据名字获得该状态，如果当前状态列表中有的话直接返回没有返回为null
			state.getCurrState = delegate (string name){	
				for(int i = 0; i< this.currState_.Count; ++i){
					State s = this.currState_[i] as State;
                    Debug.Log(s.name +":"+name);
					if(s.name == name)
					{
						return s;
					}
					
				}	
				return null;
			};

			this.states_[stateName] = state;
		}

	    /// <summary>
	    /// 根据名称转换为对应的状态，然后将该状态放到当前可执行状态前端
	    /// </summary>
	    /// <param name="name"></param>
		public void translation(string name)
		{
			State target = this.states_[name] as State;//target state
			
			if (target == null)//if no target return!
			{
				return;
			}
			
			//如果该状态为状态列表中的最后一个状态，则初始化该状态，即先结束该状态，然后在开始该状态
			//if current, reset
			if(target == this.currState_[this.currState_.Count-1])
			{
				target.over();
				target.start();
				return;
			}
			
			
			
			State publicState = null;
			
			ArrayList stateList = new ArrayList();
			
			State tempState = target;
			string fatherName = tempState.fatherName;
			
			//do loop 
			while(tempState != null)
			{
				//reiterator current list
				for(var i = this.currState_.Count -1; i >= 0; i--){
					State state = this.currState_[i] as State;
					//if has public 
					if(state == tempState){
						publicState = state;	
						break;
					}
				}
				
				//end
				if(publicState != null){
					break;
				}
				
				//else push state_list
				stateList.Insert(0, tempState);
				//state_list.unshift(temp_state);
				
				if(fatherName != ""){
					tempState = this.states_[fatherName] as State;
					fatherName = tempState.fatherName;
				}else{
					tempState = null;
				}
				
			}
			//if no public return
			if (publicState == null){
				return;
			}
			
			ArrayList newCurrState = new ArrayList();
			bool under = true;
			//-- 析构状态
			for(int i2 = this.currState_.Count -1; i2>=0; --i2)
			{
				State state2 = this.currState_[i2] as State;
				if(state2 == publicState)
				{
					under = false;
				}
				if(under){
					state2.over();
				}
				else{
					newCurrState.Insert(0, state2);
				}
				
			}
			
			
			//-- 构建状态
			for(int i3 = 0; i3 < stateList.Count; ++i3){
				State state3 = stateList[i3] as State;
				state3.start();
				newCurrState.Add(state3);
			}
			this.currState_ = newCurrState;
		}


		/// <summary>
		/// 根据名字获取当前状态
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public State getCurrState(string name)
		{
			var self = this;
			for(var i =0; i< self.currState_.Count; ++i)
			{
				State state = self.currState_[i] as  State;
				if(state.name == name)
				{
					return state;
				}
			}
			
			return null;
			
		}
		
        /// <summary>
        /// 状态机的入口点
        /// </summary>
        /// <param name="state_name">状态的名称</param>
		public void init(string state_name){
			var self = this;      //代表该状态机本身
			self.translation(state_name);
		}
		
		/// <summary>
		/// 结束所有状态，遍历状态列表中的所有当前状态，执行结束动作over,然后将当前状态列表设为NULL
		/// </summary>
		public void shutdown(){
			for(int i = this.currState_.Count-1; i>=0; --i){
				State state =  this.currState_[i] as State;
				state.over();
			}
			this.currState_ = null;  
		}

        /// <summary>
        /// 向状态机发送消息，将消息转换为事件发送给状态处理
        /// </summary>
        /// <param name="msg"></param>
		public void post(string msg){
			FSMEvent evt = new FSMEvent();
			evt.msg = msg;
			this.postEvent(evt);
		}


		public void postEvent(FSMEvent evt){
			for(int i =0; i< this.currState_.Count; ++i){
				State state = this.currState_[i] as State;
				string stateName = state.postEvent(evt) as string;
				if(stateName != ""){
					this.translation(stateName);
					break;
				}
			}
		}
	}
}
