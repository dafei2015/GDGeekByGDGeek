using UnityEngine;
using System.Collections;
namespace GDGeek{

	public class Task{
		public Task(){}
		public TaskInit init = delegate (){};
		public TaskShutdown shutdown = delegate(){};
		public TaskUpdate update = delegate(float d){};
		public TaskIsOver isOver = delegate(){return true;};
		
	};
}