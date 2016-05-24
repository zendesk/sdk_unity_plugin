using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	// <summary>
	// Request cell for the request list table.
	// </summary>
	public class ZDKRequestListTableCell {

		public static IOSAppearance _appearance = new IOSAppearance("ZDKRequestListTableCell");

		private static string _logTag = "ZDKRequestListTableCell";
		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}

		public static void SetDescriptionFont(string fontName, float size) {
			_appearance.SetFont("descriptionFont", fontName, size);
		}

		public static void SetCreatedAtFont(string fontName, float size) {
			_appearance.SetFont("createdAtFont", fontName, size);
		}

		public static void SetUnreadColor(ZenColor color) {
			_appearance.SetColor("unreadColor", color);
		}
		
		public static void SetDescriptionColor(ZenColor color) {
			_appearance.SetColor("descriptionColor", color);
		}

		public static void SetCreatedAtColor(ZenColor color) {
			_appearance.SetColor("createdAtColor", color);
		}

		public static void SetCellBackgroundColor(ZenColor color) {
			_appearance.SetColor("cellBackgroundColor", color);
		}

		public static void SetBackgroundColor(ZenColor color) {
			_appearance.SetColor("backgroundColor", color);
		}

		public static void SetVerticalMargin(float size) {
			_appearance.SetVerticalMargin(size);
		}
		
		public static void SetDescriptionTimestampMargin(float size) {
			_appearance.SetDescriptionTimestampMargin(size);
		}
		
		public static void SetLeftInset(float size) {
			_appearance.SetLeftInset(size);
		}
		
		public static void SetSeparatorInset(float top, float left, float bottom, float right) {
			_appearance.SetEdgeInsets("separatorInset", top, left, bottom, right);
		}
	}

	// <summary>
	// Empty state cell for the request list.
	// </summary>
	public class ZDKRequestListEmptyTableCell {
		
		public static IOSAppearance _appearance = new IOSAppearance("ZDKRequestListEmptyTableCell");
		
		private static string _logTag = "ZDKRequestListEmptyTableCell";
		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}

		public static void SetMessageFont(string fontName, float size) {
			_appearance.SetFont("messageFont", fontName, size);
		}
		
		public static void SetMessageColor(ZenColor color) {
			_appearance.SetColor("messageColor", color);
		}

		public static void SetBackgroundColor(ZenColor color) {
			_appearance.SetColor("backgroundColor", color);
		}
		
		public static void SetSeparatorInset(float top, float left, float bottom, float right) {
			_appearance.SetEdgeInsets("separatorInset", top, left, bottom, right);
		}
	}

	// <summary>
	// Loading state cell for the request list.
	// </summary>
	public class ZDKRequestListLoadingTableCell {
		
		public static IOSAppearance _appearance = new IOSAppearance("ZDKRequestListLoadingTableCell");
		
		private static string _logTag = "ZDKRequestListLoadingTableCell";
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