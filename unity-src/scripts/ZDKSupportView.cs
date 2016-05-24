using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	// <summary>
	// A view that displays help center content.
	// </summary>
	public class ZDKSupportView {

		public static IOSAppearance _appearance = new IOSAppearance("ZDKSupportView");

		private static string _logTag = "ZDKSupportView";
		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}

		public static void SetNoResultsContactButtonFont(string fontName, float size) {
			_appearance.SetFont("noResultsContactButtonFont", fontName, size);
		}

		public static void SetBackgroundColor(ZenColor color) {
			_appearance.SetColor("backgroundColor", color);
		}

		public static void SetTableBackgroundColor(ZenColor color) {
			_appearance.SetColor("tableBackgroundColor", color);
		}

		public static void SetSeparatorColor(ZenColor color) {
			_appearance.SetColor("separatorColor", color);
		}

		public static void SetNoResultsFoundLabelColor(ZenColor color) {
			_appearance.SetColor("noResultsFoundLabelColor", color);
		}

		public static void SetNoResultsFoundLabelBackground(ZenColor color) {
			_appearance.SetColor("noResultsFoundLabelBackground", color);
		}

		public static void SetNoResultsContactButtonBackground(ZenColor color) {
			_appearance.SetColor("noResultsContactButtonBackground", color);
		}

		public static void SetNoResultsContactButtonBorderColor(ZenColor color) {
			_appearance.SetColor("noResultsContactButtonBorderColor", color);
		}

		public static void SetNoResultsContactButtonTitleColorNormal(ZenColor color) {
			_appearance.SetColor("noResultsContactButtonTitleColorNormal", color);
		}

		public static void SetNoResultsContactButtonTitleColorHighlighted(ZenColor color) {
			_appearance.SetColor("noResultsContactButtonTitleColorHighlighted", color);
		}

		public static void SetNoResultsContactButtonTitleColorDisabled(ZenColor color) {
			_appearance.SetColor("noResultsContactButtonTitleColorDisabled", color);
		}

		public static void SetNoResultsContactButtonEdgeInsets(float top, float left, float bottom, float right) {
			_appearance.SetEdgeInsets ("noResultsContactButtonEdgeInsets", top, left, bottom, right);
		}

		public static void SetNoResultsContactButtonBorderWidth(float width) {
			_appearance.SetBorderWidth("noResultsContactButtonBorderWidth", width);
		}
		
		public static void SetNoResultsContactButtonCornerRadius(float radius) {;
			_appearance.SetCornerRadius("noResultsContactButtonCornerRadius", radius);
		}

		public static void SetAutomaticallyHideNavBarOnLandscape(int enabled) {
			_appearance.SetAutomaticallyHideNavBarOnLandscape(enabled);
		}

		public static void SetSearchBarStyle(UIBARSTYLE style) {
			_appearance.SetSearchBarStyle(style);
		}

		public static void SetSpinnerUIActivityIndicatorViewStyle(UIACTIVITYINDICATORVIEWSTYLE style) {
			_appearance.SetSpinnerUIActivityIndicatorViewStyle(style);
		}
		
		public static void SetSpinnerColor(ZenColor color) {
			_appearance.SetColor("spinner", color);
		}
	}
}		