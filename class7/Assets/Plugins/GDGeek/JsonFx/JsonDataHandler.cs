using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using Pathfinding.Serialization.JsonFx;





public class JsonDataHandler{
	
	//public static JsonData.AffixConverter affixConverter_ = null;
	protected static JsonReaderSettings rsettings_ = new JsonReaderSettings(); 
	protected static JsonWriterSettings wsettings_ = new JsonWriterSettings(); 
	
	public static T reader<T>(string json){
		
		//if(affixConverter_ == null){
		//	affixConverter_ = new JsonData.AffixConverter();
			//rsettings_.AddTypeConverter(affixConverter_);
		//	wsettings_.AddTypeConverter(affixConverter_);
		//}
		JsonReader reader = new JsonReader(json, rsettings_);
		return (T)reader.Deserialize(typeof(T)); 
	}
	
	public static string write<T>(T obj){
		
		
		
		JsonWriterSettings settings = new JsonWriterSettings();
		settings.PrettyPrint = false;
		//settings.AddTypeConverter (new JsonData.AffixConverter());
		
		StringBuilder output = new StringBuilder();
		JsonWriter writer = new JsonWriter(output, settings);	
		writer.Write(obj);
		return output.ToString();
	}

} 
