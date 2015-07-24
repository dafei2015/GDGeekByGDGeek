

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

	public class TweenAlpha : Tween
	{
	    public float from = 1.0f;
	    public float to = 1.0f;
	   // public bool updateTable = false;
	    
	   // Transform mTrans;
	    //  UITable mTable;
	    private MaskableGraphic mGraphic = null;
	  //  private Text mText = null;
	    public MaskableGraphic cachedGraphic { get { if (mGraphic == null) mGraphic = this.gameObject.GetComponent<MaskableGraphic>(); return mGraphic; } }

	    public float alpha { get { 
	        
	            return cachedGraphic.color.a; 
	        
	        } set { 
	            Color color = cachedGraphic.color;
	            color.a = value;
	            cachedGraphic.color = color; 
	        
	        } }
	    
	    override protected void OnUpdate (float factor, bool isFinished)
	    {   
	        alpha = from * (1f - factor) + to * factor;
	        
	    }
	    
	    /// <summary>
	    /// Start the tweening operation.
	    /// </summary>
	    
	    static public TweenAlpha Begin (GameObject go, float duration, float alpha)
	    {
	        TweenAlpha comp = Tween.Begin<TweenAlpha>(go, duration);
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
