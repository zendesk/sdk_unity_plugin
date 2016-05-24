using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	public class ZDKUIImageScrollView {

		public static IOSAppearance _appearance = new IOSAppearance("ZDKUIImageScrollView");

		private static string _logTag = "ZDKUIImageScrollView";
		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}

		public static void SetBackgroundColor(ZenColor color) {
			_appearance.SetColor("backgroundColor", color);
		}
	}
}		