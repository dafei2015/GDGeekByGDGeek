using UnityEngine;
using System.Collections;
using Pathfinding.Serialization.JsonFx;
namespace GDGeek{
	public abstract class DataInfo{
		
		[JsonMember]
		public bool succeed = false;
		
		[JsonMember]
		public string message = "";
		
		[JsonMember]
		public double epoch = 0;
		
	}

	public interface DataInfoLoader<T>{
		T load (string json);

	}

	public class WebLoaderTask<T> : Task where T:DataInfo {
		public delegate void Succeed (T info);
		public delegate void Error (string msg);
		public event Succeed onSucceed;
		public event Error onError;
		private WebPack pack_ = null;
		public WebPack pack{
			get{
				return pack_;
			}
		}

		public WebLoaderTask(string url, DataInfoLoader<T> loader){
			pack_ = new WebPack (url);
			pack_.addField("a", "b");
			bool isOver = false;
			this.init = delegate {
				isOver = false;
				Task web = WebTaskFactory.GetInstance().create(pack, delegate(string json) {
					if(onSucceed != null){
						T info = loader.load(json);
						if(WebTimestamp.GetInstance() != null){
							WebTimestamp.GetInstance().synchro(info.epoch);
						}
						onSucceed(info);
					}
				},delegate(string msg) {
					if(onError != null){
						onError(msg);
						
						//Debug.LogError("asdfasdf");
					}
				});
				TaskManager.PushBack (web, delegate{
					isOver = true;
				});
				this.isOver = delegate {
					return isOver;
				};
				TaskManager.Run (web);
			};


		}
		

	}
}
