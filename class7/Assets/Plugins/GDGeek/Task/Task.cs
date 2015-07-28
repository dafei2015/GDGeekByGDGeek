using UnityEngine;
using System.Collections;
namespace GDGeek{

    /// <summary>
    /// 委托的初始化
    /// </summary>
	public class Task{
		public Task(){}
		public TaskInit init = delegate (){};
		public TaskShutdown shutdown = delegate(){};
		public TaskUpdate update = delegate(float d){};
		public TaskIsOver isOver = delegate(){return true;};
		
	};
}