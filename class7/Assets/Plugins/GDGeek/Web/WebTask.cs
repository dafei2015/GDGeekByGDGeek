using UnityEngine;
using System.Collections;

namespace GDGeek{
	/*
	public class WebTask : GDGeek.Task{
		

		protected bool over_ = false;

		public bool over{
			get{
				return over_;
			}
			set{
				over_ = value;
			}
		}


		public void doOver(){
			this.over_ = true;
		}
		protected string url_ = "http://ezdoing.com";

		public void setup(string url){
			url_ = url;
			
		}
		public WebTask(){
			this.init = delegate {
				this.over = false;  
				WebTaskFactory.GetInstance().link(this);
			};
			this.isOver = delegate{
				return over_;
			};

		}
		
		private WebPack pack_  = new WebPack("asdf");
		public WebPack pack{
			get{
				return pack_;
			}

		}


		public delegate void Reset();
		public Reset reset = delegate(){};
		

		public WWW www(){
			WWW www = this.pack.www(this.url_) as WWW;
			return www;
		}
		public void handle(WWW www){
			
			if(www.error != null) {
				handleError(www.error);
				Debug.Log(url_ +":"+www.error);
				return;
			}
			var text = "";
			if(www.responseHeaders.ContainsKey("CONTENT-ENCODING") && www.responseHeaders["CONTENT-ENCODING"] == "gzip")
			{
				Debug.Log("a zip");
				Debug.Log(www.text);
	#if UNITY_STANDALONE_WIN
				Debug.Log("not iphone");
				text = JsonData.GZip.DeCompress(www.bytes);  
	#else
				text = www.text;
	#endif
			}else{
				
				Debug.Log("no zip");
				text = www.text;
			} 
			
			
			Debug.Log(url_); 
			handleResult(text); 
			
		}
		
		
		protected void handleError(string text){
			
			Debug.LogError(text);
		}
		protected void handleResult(string text){
			Debug.Log(text);
		}
		
	}*/
}

