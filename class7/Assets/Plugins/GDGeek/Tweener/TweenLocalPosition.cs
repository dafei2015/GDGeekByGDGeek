//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Tween the object's position.
/// </summary>
namespace GDGeek{
	public class TweenLocalPosition : Tween
	{
		public Vector3 from;
		public Vector3 to;

		Transform mTrans;

		public Transform cachedTransform { get { if (mTrans == null) mTrans = transform; return mTrans; } }
		public Vector3 position { get { return cachedTransform.localPosition; } set { cachedTransform.localPosition = value; } }

		override protected void OnUpdate (float factor, bool isFinished) { cachedTransform.localPosition = from * (1f - factor) + to * factor; }

		/// <summary>
		/// Start the tweening operation.
		/// </summary>

		static public TweenLocalPosition Begin (GameObject go, float duration, Vector3 pos)
		{
			TweenLocalPosition comp = Tween.Begin<TweenLocalPosition>(go, duration);
			comp.from = comp.position;
			comp.to = pos;

			if (duration <= 0f)
			{
				comp.Sample(1f, true);
				comp.enabled = false;
			}
			return comp;
		}
	}
}