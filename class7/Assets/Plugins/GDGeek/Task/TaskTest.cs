using UnityEngine;
using System.Collections;
using GDGeek;
public class TaskTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TaskList tl = new TaskList ();
		Task task1 = new Task ();
		task1.init = delegate() {
			Debug.Log("this is firs task!!!");
		};
		task1.isOver = delegate() {
			return true;
		};
		tl.push (task1);
		TaskWait wait = new TaskWait ();
		wait.setAllTime (2f);
		tl.push (wait);
		Task task2 = new Task ();
		task2.init = delegate() {
			Debug.Log("this is second task!!!");
		};
		tl.push (task2);

		TaskSet mt = new TaskSet ();
		Task task3 = new Task ();
		task3.init = delegate() {
			Debug.Log("this is third task!!!");
		};
		mt.push (task3);

		Task task4 = new Task ();
		task4.init = delegate() {
			Debug.Log("this is four task!!!");
		};
		mt.push (task4);
		TaskWait wait2 = new TaskWait ();
		wait2.setAllTime (5f);
		mt.push (wait2);

		Task task5 = new Task ();
		task5.init = delegate() {
			Debug.Log("this is five task!!!");
		};
		mt.push (task5);

		tl.push (mt);


		TaskManager.Run (tl);
	}

}

