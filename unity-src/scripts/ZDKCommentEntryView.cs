using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	// <summary>
	// Comment entry text view and send button.
	// </summary>
	public class ZDKCommentEntryView {

		public static IOSAppearance _appearance = new IOSAppearance("ZDKCommentEntryView");

		private static string _logTag = "ZDKCommentEntryView";
		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}

		public static void SetBackgroundColor(ZenColor color) {
			_appearance.SetColor("backgroundColor", color);
		}

		public static void SetTopBorderColor(ZenColor color) {
			_appearance.SetColor("topBorderColor", color);
		}

		public static void SetTextEntryColor(ZenColor color) {
			_appearance.SetColor("textEntryColor", color);
		}

		public static void SetTextEntryBackgroundColor(ZenColor color) {
			_appearance.SetColor("textEntryBackgroundColor", color);
		}

		public static void SetTextEntryBorderColor(ZenColor color) {
			_appearance.SetColor("textEntryBorderColor", color);
		}

		public static void SetSendButtonColor(ZenColor color) {
			_appearance.SetColor("sendButtonColor", color);
		}

		public static void SetAreaBackgroundColor(ZenColor color) {
			_appearance.SetColor("areaBackgroundColor", color);
		}

		public static void SetTextEntryFont(string fontName, float size) {
			_appearance.SetFont("textEntryFont", fontName, size);
		}

		public static void SetSendButtonFont(string fontName, float size) {
			_appearance.SetFont("sendButtonFont", fontName, size);
		}
	}
}		