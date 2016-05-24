using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	/// <summary>
	/// Color specification for ZenAppearance properties.
	/// Similar to NSColor for iOS.
	/// </summary>
	public class ZenColor {

		public enum COLORTYPE {
			WHITEONLY,
			REDGREENBLUE,
			BYNAME
		};

		private static string _logTag = "ZenColor";		
		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}

		public COLORTYPE Type;
		public float White;
		public float Red;
		public float Green;
		public float Blue;
		public float Alpha;
		public string Name;

		public ZenColor(float white, float alpha) {
			White = white;
			Alpha = alpha;
			Type = COLORTYPE.WHITEONLY;
		}

		public ZenColor(float red, float green, float blue, float alpha) {
			Red = red;
			Green = green;
			Blue = blue;
			Alpha = alpha;
			Type = COLORTYPE.REDGREENBLUE;
		}

		public ZenColor(string name) {
			Name = name;
			Type = COLORTYPE.BYNAME;
		}
	}
}		

