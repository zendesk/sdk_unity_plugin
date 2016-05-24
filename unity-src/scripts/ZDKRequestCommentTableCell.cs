using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {
	
	// <summary>
	// Super class for request comment cells with shared functionality
	// </summary>
	public class ZDKRequestCommentTableCell {
		
		public static IOSAppearance _appearance = new IOSAppearance("ZDKRequestCommentTableCell");
		
		private static string _logTag = "ZDKAgentCommentTableCell";
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

	// <summary>
	// Comment cell for rendering agent comments.
	// </summary>
	public class ZDKAgentCommentTableCell {

		public static IOSAppearance _appearance = new IOSAppearance("ZDKAgentCommentTableCell");

		private static string _logTag = "ZDKAgentCommentTableCell";
		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}

		public static void SetBackgroundColor(ZenColor color) {
			_appearance.SetColor("backgroundColor", color);
		}

		public static void SetAvatarSize(float size) {
			_appearance.SetAvatarSize(size);
		}

		public static void SetAgentNameFont(string fontName, float size) {
			_appearance.SetFont("agentNameFont", fontName, size);
		}

		public static void SetBodyFont(string fontName, float size) {
			_appearance.SetFont("bodyFont", fontName, size);
		}

		public static void SetTimestampFont(string fontName, float size) {
			_appearance.SetFont("timestampFont", fontName, size);
		}

		public static void SetAgentNameColor(ZenColor color) {
			_appearance.SetColor("agentNameColor", color);
		}

		public static void SetBodyColor(ZenColor color) {
			_appearance.SetColor("bodyColor", color);
		}

		public static void SetTimestampColor(ZenColor color) {
			_appearance.SetColor("timestampColor", color);
		}

		public static void SetCellBackground(ZenColor color) {
			_appearance.SetColor("cellBackground", color);
		}
		
		public static void SetSeparatorInset(float top, float left, float bottom, float right) {
			_appearance.SetEdgeInsets("separatorInset", top, left, bottom, right);
		}
	}

	// <summary>
	// Comment cell for rendering end user comments.
	// </summary>
	public class ZDKEndUserCommentTableCell {
		
		public static IOSAppearance _appearance = new IOSAppearance("ZDKEndUserCommentTableCell");
		
		private static string _logTag = "ZDKEndUserCommentTableCell";
		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}
		
		public static void SetBackgroundColor(ZenColor color) {
			_appearance.SetColor("backgroundColor", color);
		}

		public static void SetBodyFont(string fontName, float size) {
			_appearance.SetFont("bodyFont", fontName, size);
		}

		public static void SetTimestampFont(string fontName, float size) {
			_appearance.SetFont("timestampFont", fontName, size);
		}

		public static void SetBodyColor(ZenColor color) {
			_appearance.SetColor("bodyColor", color);
		}
		
		public static void SetTimestampColor(ZenColor color) {
			_appearance.SetColor("timestampColor", color);
		}
		
		public static void SetCellBackground(ZenColor color) {
			_appearance.SetColor("cellBackground", color);
		}
		
		public static void SetSeparatorInset(float top, float left, float bottom, float right) {
			_appearance.SetEdgeInsets("separatorInset", top, left, bottom, right);
		}
	}

	// <summary>
	// Loading state cell for the request list.
	// </summary>
	public class ZDKCommentsListLoadingTableCell {
		
		public static IOSAppearance _appearance = new IOSAppearance("ZDKCommentsListLoadingTableCell");
		
		private static string _logTag = "ZDKCommentsListLoadingTableCell";
		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}
		
		public static void SetBackgroundColor(ZenColor color) {
			_appearance.SetColor("backgroundColor", color);
		}

		public static void SetLeftInset(float size) {
			_appearance.SetLeftInset(size);
		}

		public static void SetCellBackground(ZenColor color) {
			_appearance.SetColor("cellBackground", color);
		}
		
		public static void SetSeparatorInset(float top, float left, float bottom, float right) {
			_appearance.SetEdgeInsets("separatorInset", top, left, bottom, right);
		}
	}
}		