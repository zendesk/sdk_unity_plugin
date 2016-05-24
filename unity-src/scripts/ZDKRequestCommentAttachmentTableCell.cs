using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {
	
	public class ZDKRequestCommentAttachmentTableCell {

		public static IOSAppearance _appearance = new IOSAppearance("ZDKRequestCommentAttachmentTableCell");

		private static string _logTag = "ZDKRequestCommentAttachmentTableCell";
		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}

		public static void SetBackgroundColor(ZenColor color) {
			_appearance.SetColor("backgroundColor", color);
		}

		public static void SetSeparatorInset(float top, float left, float bottom, float right) {
			_appearance.SetEdgeInsets("separatorInset", top, left, bottom, right);
		}
	}
}		