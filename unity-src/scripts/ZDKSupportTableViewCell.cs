using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {                
	
	public class ZDKSupportTableViewCell {

		public static IOSAppearance _appearance = new IOSAppearance("ZDKSupportTableViewCell");

		private static string _logTag = "ZDKSupportTableViewCell";
		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}

		public static void SetTitleLabelFont(string fontName, float size) {
			_appearance.SetFont("titleLabelFont", fontName, size);
		}

		public static void SetBackgroundColor(ZenColor color) {
			_appearance.SetColor("backgroundColor", color);
		}

		public static void SetTitleLabelColor(ZenColor color) {;
			_appearance.SetColor("titleLabelColor", color);
		}

		public static void SetTitleLabelBackground(ZenColor color) {
			_appearance.SetColor("titleLabelBackground", color);
		}

		public static void SetSeparatorInset(float top, float left, float bottom, float right) {
			_appearance.SetEdgeInsets("separatorInset", top, left, bottom, right);
		}
	}
}		