using UnityEngine;
using System.Collections;

//任务委托的声明
namespace GDGeek{
	public delegate void TaskInit();
	public delegate void TaskShutdown();
	public delegate void TaskUpdate(float d);
	public delegate bool TaskIsOver();
	public delegate Task TaskFactory();


}