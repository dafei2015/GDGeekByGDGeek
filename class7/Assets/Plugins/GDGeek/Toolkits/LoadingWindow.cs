
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;


namespace GDGeek{
	public class LoadingWindow : MonoBehaviour {
		public CanvasGroup _cg = null;
	//	public EventSystem _evtSys = null; 
		public float _wait = 0.3f;
		public void Awake(){
			//this.gameObject.SetActive(false);
			//_evtSys.StopCoroutine(
		}
		private Task packTask(Task task){
			Task pack = new Task();
			float allTime = 0.0f;
			bool isOver = false;
			bool isLoading = false;
			bool isLoaded = false;
			pack.init = delegate{
				allTime = 0;
				isOver = false;
				isLoading = false;
				isLoaded = false;
				TaskManager.PushBack (task, delegate{
					isOver = true;
					
				});
				TaskManager.Run (task);
			};
			pack.update = delegate(float d) {
				allTime += d;
				if(allTime>_wait && !isLoading){

					isLoading = true;
					Task show = this.show ();
					TaskManager.PushBack (show, delegate{
						//Debug.LogError("???");
						isLoaded = true;
					});
					TaskManager.Run (show);
				}
			};
			
			
			pack.isOver = delegate {
				if (isOver) {
					if(!isLoading){//Debug.LogError("error");
						return true;
					}
					if(isLoaded){//Debug.LogError("erro2r");
						return true;
					}
					
				}
				return false;
				
			};
			return pack;
		}
		public Task run(Task task){


			TaskList tl = new TaskList ();


			tl.push (packTask(task));
			tl.push (hide ());

			TaskManager.PushFront (tl, delegate {
				_cg.alpha = 0f;
				this.gameObject.SetActive(true);
			});
			return tl;
		}
		private Task show(){
			TweenTask task = new TweenTask (
				delegate{
					this.gameObject.SetActive(true);
					TweenGroupAlpha alpha = TweenGroupAlpha.Begin(this.gameObject,0.3f, 1.0f);
					return alpha;
				}
			);
			TaskManager.PushBack (task, delegate {
				_cg.alpha = 1.0f;
			});
			return task;
		}
		private Task hide(){
			TweenTask task = new TweenTask (
				delegate{
				TweenGroupAlpha alpha = TweenGroupAlpha.Begin(this.gameObject,0.15f, 0.0f);
				return alpha;
			}
			);
		
	

			TaskManager.PushBack (task, delegate {
				if(_cg != null) _cg.alpha = 0.0f;
				//Debug.LogError("????");
			//	if(_evtSys != null) _evtSys.enabled = true;
				if(this != null && this.gameObject != null) this.gameObject.SetActive(false);
			});
			return task;
		}
	}

}
