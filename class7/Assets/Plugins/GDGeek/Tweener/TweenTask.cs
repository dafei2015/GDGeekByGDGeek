using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class TweenTask : Task{

		public delegate Tween Maker();

		public TweenTask(Maker maker){
			Tween gw = null;
			this.init =  delegate {
				gw = maker();
			};
			this.isOver = delegate {
				if(gw && gw.enabled){
					return false;
				}else{
					return true;
				}
			};
		}


	}

}