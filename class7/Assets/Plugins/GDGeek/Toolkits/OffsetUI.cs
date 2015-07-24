
using UnityEngine;
using System.Collections;
namespace GDGeek{
	


	[ExecuteInEditMode]
	public class OffsetUI : MonoBehaviour {
		public Vector2 _original;
		public RectTransform _rect;
		public bool _enable = false;
		// Use this for initialization
		private void refersh(){
			if (_rect == null || _original.x == 0 || _original.y == 0) {
				return;		
			}
			if (_enable) {
					float rx = Screen.width / _original.x;
					float ry = Screen.height / _original.y;
					if (rx < ry) {
							float r = rx;
			
							float cha = (1f - r) * Screen.height;
							float x = (1f - r) / 2 * Screen.width;
							float y = cha / 2f;
			
							this.gameObject.transform.localScale = new Vector3 (r, r, 1);
							this.gameObject.transform.localPosition = new Vector3 (x, y, 0);
							_rect.sizeDelta = new Vector2 ((_original.x - Screen.width), cha / r);
					} else {
							float r = ry;
			
							float cha = (1f - r) * Screen.width;
							float y = (1 - r) / 2 * Screen.height;
							float x = cha / 2f;
			
							this.gameObject.transform.localScale = new Vector3 (r, r, 1);
							this.gameObject.transform.localPosition = new Vector3 (x, y, 0);
							_rect.sizeDelta = new Vector2 (cha / r, (_original.y - Screen.height));
					}		
			} else {
				this.gameObject.transform.localScale = new Vector3 (1, 1, 1);
				this.gameObject.transform.localPosition = new Vector3 (0, 0, 0);
				_rect.sizeDelta = new Vector2 (0, 0 );
			}

		}
		void Start () {
			refersh ();

		}
		void Update(){
			refersh ();
		}
	}
}
