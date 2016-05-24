using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	// <summary>
	// A simple table view with limited rows for displaying the options associated with ZDKRMA.
	// The table height determins the height of the individual rows and table header. The header and row
	// height is calculated dividing the number of rows plus the header
	// </summary>
	public class ZDKRMADialogView {

		public static IOSAppearance _appearance = new IOSAppearance("ZDKRMADialogView");

		private static string _logTag = "ZDKRMADialogView";
		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}

		public static void SetHeaderFont(string fontName, float size) {
			_appearance.SetFont("headerFont", fontName, size);
		}

		public static void SetButtonFont(string fontName, float size) {
			_appearance.SetFont("buttonFont", fontName, size);
		}
		
		public static void SetHeaderBackgroundColor(ZenColor color) {
			_appearance.SetColor("headerBackgroundColor", color);
		}

		public static void SetHeaderColor(ZenColor color) {
			_appearance.SetColor("headerColor", color);
		}

		public static void SetSeparatorLineColor(ZenColor color) {
			_appearance.SetColor("separatorLineColor", color);
		}

		public static void SetButtonBackgroundColor(ZenColor color) {
			_appearance.SetColor("buttonBackgroundColor", color);
		}

		public static void SetButtonSelectedBackgroundColor(ZenColor color) {
			_appearance.SetColor("buttonSelectedBackgroundColor", color);
		}

		public static void SetButtonColor(ZenColor color) {
			_appearance.SetColor("buttonColor", color);
		}

		public static void SetBackgroundColor(ZenColor color) {
			_appearance.SetColor("backgroundColor", color);
		}
	}
}		