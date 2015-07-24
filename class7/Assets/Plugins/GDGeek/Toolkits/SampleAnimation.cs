using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace GDGeek{
	public class SampleAnimation : MonoBehaviour {
		public Image _image = null;
		public Sprite[] _sprites = null;
		public float _interval = 1.0f;
		private float allTime_ = 0;
		private int index_ = 0;
		// Update is called once per frame
		void Update () {
			allTime_ += Time.deltaTime;
			while (allTime_ > _interval) {
				allTime_-=	_interval;
				index_++;
				index_ %= _sprites.Length;
				_image.sprite = _sprites[index_];
			}
		}
	}
}
