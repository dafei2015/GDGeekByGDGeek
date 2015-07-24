using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace GDGeek{
	public class WebTimestamp : MonoBehaviour {
		
		// Use this for initialization
		private static WebTimestamp instance_ = null;
		private static double synchro_ = 0;
		private void Awake(){
			WebTimestamp.instance_ = this;
		}
		public static WebTimestamp GetInstance(){
			return WebTimestamp.instance_;
		}
		private double local{
			get{
				double epoch = (System.DateTime.Now.Ticks - 621355968000000000) / 10000000;
				return epoch;
			}
		}
		public double epoch{
			get{
				return local + synchro_;
			}
		}
		public void synchro(double stamp){
			synchro_ = stamp - local;
		}
	}
}

