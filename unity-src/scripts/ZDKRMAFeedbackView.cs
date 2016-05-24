using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	// <summary>
	// A ZDKRMAFeedbackView contains a header, a text view and a set of buttons.
	// </summary>
	public class ZDKRMAFeedbackView {

		public static IOSAppearance _appearance = new IOSAppearance("ZDKRMAFeedbackView");

		private static string _logTag = "ZDKRMAFeedbackView";
		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}

		public static void SetSubheaderFont(string fontName, float size) {
			_appearance.SetFont("subheaderFont", fontName, size);
		}

		public static void SetHeaderFont(string fontName, float size) {
			_appearance.SetFont("headerFont", fontName, size);
		}

		public static void SetTextEntryFont(string fontName, float size) {
			_appearance.SetFont("textEntryFont", fontName, size);
		}

		public static void SetButtonFont(string fontName, float size) {
			_appearance.SetFont("buttonFont", fontName, size);
		}

		public static void SetButtonColor(ZenColor color) {
			_appearance.SetColor("buttonColor", color);
		}

		public static void SetButtonSelectedColor(ZenColor color) {
			_appearance.SetColor("buttonSelectedColor", color);
		}

		public static void SetButtonBackgroundColor(ZenColor color) {
			_appearance.SetColor("buttonBackgroundColor", color);
		}

		public static void SetSeparatorLineColor(ZenColor color) {
			_appearance.SetColor("separatorLineColor", color);
		}

		public static void SetBackgroundColor(ZenColor color) {
			_appearance.SetColor("backgroundColor", color);
		}

		public static void SetHeaderColor(ZenColor color) {
			_appearance.SetColor("headerColor", color);
		}

		public static void SetSubHeaderColor(ZenColor color) {
			_appearance.SetColor("subHeaderColor", color);
		}

		public static void SetTextEntryColor(ZenColor color) {
			_appearance.SetColor("textEntryColor", color);
		}

		public static void SetTextEntryBackgroundColor(ZenColor color) {
			_appearance.SetColor("textEntryBackgroundColor", color);
		}

		public static void SetPlaceHolderColor(ZenColor color) {
			_appearance.SetColor("placeHolderColor", color);
		}

		public static void SetSpinnerUIActivityIndicatorViewStyle(UIACTIVITYINDICATORVIEWSTYLE style) {
			_appearance.SetSpinnerUIActivityIndicatorViewStyle(style);
		}
		
		public static void SetSpinnerColor(ZenColor color) {
			_appearance.SetColor("spinner", color);
		}

		public static void SetViewBackgroundColor(ZenColor color){
			_appearance.SetColor ("viewBackgroundColor",color);
		}
	}
}		