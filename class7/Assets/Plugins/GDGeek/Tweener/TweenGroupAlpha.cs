
//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Tween the object's local scale.
/// </summary>

namespace GDGeek{
	public class TweenGroupAlpha : Tween
	{
		public float from = 1.0f;
		public float to = 1.0f;

		private CanvasGroup group_ = null;
		//  private Text mText = null;
		public CanvasGroup cachedGroup { get { if (group_ == null) group_ = this.gameObject.GetComponent<CanvasGroup>(); return group_; } }
		
		public float alpha { get { 
				
				return cachedGroup.alpha; 
				
			} set { 

				cachedGroup.alpha = value; 
				
			} }
		
		override protected void OnUpdate (float factor, bool isFinished)
		{   
			alpha = from * (1f - factor) + to * factor;
			
		}
		
		/// <summary>
		/// Start the tweening operation.
		/// </summary>
		
		static public TweenGroupAlpha Begin (GameObject go, float duration, float alpha)
		{
			TweenGroupAlpha comp = Tween.Begin<TweenGroupAlpha>(go, duration);
			comp.from = comp.alpha;
			comp.to = alpha;
			
			if (duration <= 0f)
			{
				comp.Sample(1f, true);
				comp.enabled = false;
			}
			return comp;
		}
	}
}