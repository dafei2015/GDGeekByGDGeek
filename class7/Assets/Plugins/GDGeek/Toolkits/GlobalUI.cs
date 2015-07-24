using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class GlobalUI : MonoBehaviour {

		public LoadingWindow _loading = null;
		private static GlobalUI instance_ = null; 

		public static GlobalUI GetInstance(){
			return GlobalUI.instance_;
		}




		void Awake(){
			GlobalUI.instance_ = this;
		}
		public LoadingWindow loading{
			get{
				return _loading;
			}

		}
	}
}
