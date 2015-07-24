using UnityEngine;
using System.Collections;

public class Filter {

	private int num_ = 0;
	private float[] filters_ = new float[15];
	
	public Filter(){
		for(int i = 0;i<filters_.Length; ++i){
			this.filters_[i] = 0.015f;
		}
		
	}
	
	public float interval(float d){
		this.filters_[num_] = d;
		num_++;
		if(num_ >= 15)
			num_ = 0;
		float all = 0;
		for(int i =0; i<15; ++i){
			all += this.filters_[i];
		}
		return (all/15);
		
	}
}
