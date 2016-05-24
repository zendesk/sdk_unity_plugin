using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	/// <summary>
	/// A small view which displays attachments for a comment being created.
	/// Allowing end users to add and remove attachments to comments. 
	/// </summary>
	public class ZDKAttachmentView {

		public static IOSAppearance _appearance = new IOSAppearance("ZDKAttachmentView");

		private static string _logTag = "ZDKAttachmentView";
		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}

		public static void SetBackgroundColor(ZenColor color) {
			_appearance.SetColor("backgroundColor", color);
		}

		public static void SetCloseButtonBackgroundColor(ZenColor color) {
			_appearance.SetColor("closeButtonBackgroundColor", color);
		}
	}
}		