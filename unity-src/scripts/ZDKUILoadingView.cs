using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	// <summary>
	// Loading indicator.
	// </summary>
	public class ZDKUILoadingView {

		public static IOSAppearance _appearance = new IOSAppearance("ZDKUILoadingView");

		private static string _logTag = "ZDKUILoadingView";
		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}

		public static void SetBackgroundColor(ZenColor color) {
			_appearance.SetColor("backgroundColor", color);
		}
	}
}		