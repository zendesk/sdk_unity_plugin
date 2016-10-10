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

		public float Red;
		public float Green;
		public float Blue;
		public float Alpha;

		public ZenColor(float red, float green, float blue) : this(red, green, blue, 1.0f) {}

		public ZenColor(float red, float green, float blue, float alpha) {
			Red = red;
			Green = green;
			Blue = blue;
			Alpha = alpha;
		}
	}
}