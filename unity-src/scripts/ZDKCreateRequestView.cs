using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	// <summary>
	// Request creation view.
	// </summary>
	public class ZDKCreateRequestView {

		public static IOSAppearance _appearance = new IOSAppearance("ZDKCreateRequestView");

		private static string _logTag = "ZDKCreateRequestView";
		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}

		public static void SetTextEntryFont(string fontName, float size) {
			_appearance.SetFont("textEntryFont", fontName, size);
		}
		
		public static void SetPlaceholderTextColor(ZenColor color) {
			_appearance.SetColor("placeholderTextColor", color);
		}

		public static void SetTextEntryColor(ZenColor color) {
			_appearance.SetColor("textEntryColor", color);
		}

		public static void SetTextEntryBackgroundColor(ZenColor color) {
			_appearance.SetColor("textEntryBackgroundColor", color);
		}

		public static void SetAttachmentButtonBorderColor(ZenColor color) {
			_appearance.SetColor("attachmentButtonBorderColor", color);
		}

		public static void SetAttachmentButtonBackground(ZenColor color) {
			_appearance.SetColor("attachmentButtonBackground", color);
		}

		public static void SetBackgroundColor(ZenColor color) {
			_appearance.SetColor("backgroundColor", color);
		}

		public static void SetAttachmentButtonCornerRadius(float radius) {
			_appearance.SetCornerRadius ("attachmentButtonCornerRadius", radius);
		}
		
		public static void SetAttachmentButtonBorderWidth(float radius) {
			_appearance.SetBorderWidth("attachmentButtonBorderWidth", radius);
		}

		public static void SetAutomaticallyHideNavBarOnLandscape(int enabled) {
			_appearance.SetAutomaticallyHideNavBarOnLandscape(enabled);
		}

		public static void SetAttachmentActionSheetStyle(UIACTIONSHEETSTYLE style) {
			_appearance.SetAttachmentActionSheetStyle(style);
		}

		public static void SetSpinnerUIActivityIndicatorViewStyle(UIACTIVITYINDICATORVIEWSTYLE style) {
			_appearance.SetSpinnerUIActivityIndicatorViewStyle(style);
		}
		
		public static void SetSpinnerColor(ZenColor color) {
			_appearance.SetColor("spinner", color);
		}

		public static void SetAttachmentButtonImage(string imageName, string type) {
			_appearance.SetAttachmentButtonImage(imageName, type);
		}
	}
}		