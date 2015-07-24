using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class WebTaskFactory : MonoBehaviour{
		public delegate void Callback ();
		public delegate void Handler (string s);

		private WebData data_ = new WebData(); 
		public WebData data{
			get{
				return data_;
			}
			set{
				data_ = value;
			}
		}
		
		
		private static WebTaskFactory instance_ = null;
		public  void Awake(){
			WebTaskFactory.instance_ = this;

		}

		
		public static WebTaskFactory GetInstance(){
			return WebTaskFactory.instance_;
		}
	
		private IEnumerator linkIt(WWW www, Handler succeed, Handler error, Callback doOver){

			yield return www;
			doHandle(www, succeed, error);
			doOver();
			
		}
		public Task create(WebPack pack, Handler succeed, Handler error){
			WWW www = pack.www() as WWW;
			Task task = new Task ();
			bool over = false;
			task.init = delegate {
				over = false;  
				StartCoroutine(WebTaskFactory.GetInstance().linkIt(www, succeed, error, delegate{
					over = true;
				}));
			};

			task.isOver = delegate{
				return over;
			};
			return task;

		}


		public void doHandle(WWW www, Handler succeed, Handler error){
			
			if(www.error != null) {
				error(www.error);
				Debug.Log(":"+www.error);
				return;
			}
			var text = "";
			if(www.responseHeaders.ContainsKey("CONTENT-ENCODING") && www.responseHeaders["CONTENT-ENCODING"] == "gzip")
			{
				Debug.Log("a zip");
				Debug.Log(www.text);
				#if UNITY_STANDALONE_WIN
				Debug.Log("not iphone");
				//text = JsonData.GZip.DeCompress(www.bytes);  
				#else
				text = www.text;
				#endif
			}else{
				
				Debug.Log("no zip" + www.text);
				text = www.text;
			} 
			
			
			//Debug.Log(url_); 
			succeed(text); 
			
		}
	
	}
}