using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	/// <summary>
	/// Appearance specifications for customizing the Zendesk
	/// interface on iOS.
	/// </summary>
	public class IOSAppearance {

		public IOSAppearance() {
			// Intentionally empty
		}

		#if UNITY_IPHONE
		[DllImport("__Internal")]
		private static extern void _zendeskThemeStartWithBaseTheme();
		[DllImport("__Internal")]
		private static extern void _zendeskThemeApplyTheme();
		[DllImport("__Internal")]
		private static extern void _zendeskThemeSetPrimaryTextColor(float red, float green, float blue, float alpha);
		[DllImport("__Internal")]
		private static extern void _zendeskThemeSetSecondaryTextColor(float red, float green, float blue, float alpha);
		[DllImport("__Internal")]
		private static extern void _zendeskThemeSetPrimaryBackgroundColor(float red, float green, float blue, float alpha);
		[DllImport("__Internal")]
		private static extern void _zendeskThemeSetSecondaryBackgroundColor(float red, float green, float blue, float alpha);
		[DllImport("__Internal")]
		private static extern void _zendeskThemeSetEmptyBackgroundColor(float red, float green, float blue, float alpha);
		[DllImport("__Internal")]
		private static extern void _zendeskThemeSetMetaTextColor(float red, float green, float blue, float alpha);
		[DllImport("__Internal")]
		private static extern void _zendeskThemeSetSeparatorColor(float red, float green, float blue, float alpha);
		[DllImport("__Internal")]
		private static extern void _zendeskThemeSetInputFieldColor(float red, float green, float blue, float alpha);
		[DllImport("__Internal")]
		private static extern void _zendeskThemeSetInputFieldBackgroundColor(float red, float green, float blue, float alpha);
		[DllImport("__Internal")]
		private static extern void _zendeskThemeSetFontName(string fontName);
		[DllImport("__Internal")]
		private static extern void _zendeskThemeSetBoldFontName(string boldFontName);

		public void StartWithBaseTheme() {
			_zendeskThemeStartWithBaseTheme ();
		}

		public void ApplyTheme() {
			_zendeskThemeApplyTheme();
		}

		public void SetPrimaryTextColor(ZenColor color) {
			_zendeskThemeSetPrimaryTextColor(color.Red, color.Green, color.Blue, color.Alpha);
		}

		public void SetSecondaryTextColor(ZenColor color) {
			_zendeskThemeSetSecondaryTextColor(color.Red, color.Green, color.Blue, color.Alpha);
		}

		public void SetPrimaryBackgroundColor(ZenColor color) {
			_zendeskThemeSetPrimaryBackgroundColor(color.Red, color.Green, color.Blue, color.Alpha);
		}

		public void SetSecondaryBackgroundColor(ZenColor color) {
			_zendeskThemeSetSecondaryBackgroundColor(color.Red, color.Green, color.Blue, color.Alpha);
		}

		public void SetEmptyBackgroundColor(ZenColor color) {
			_zendeskThemeSetEmptyBackgroundColor(color.Red, color.Green, color.Blue, color.Alpha);
		}

		public void SetMetaTextColor(ZenColor color) {
			_zendeskThemeSetMetaTextColor(color.Red, color.Green, color.Blue, color.Alpha);
		}

		public void SetSeparatorColor(ZenColor color) {
			_zendeskThemeSetSeparatorColor(color.Red, color.Green, color.Blue, color.Alpha);
		}

		public void SetInputFieldColor(ZenColor color) {
			_zendeskThemeSetInputFieldColor(color.Red, color.Green, color.Blue, color.Alpha);
		}

		public void SetInputFieldBackgroundColor(ZenColor color) {
			_zendeskThemeSetInputFieldBackgroundColor(color.Red, color.Green, color.Blue, color.Alpha);
		}

		public void SetFontName(string fontName) {
			_zendeskThemeSetFontName(fontName);
		}

		public void SetBoldFontName(string boldFontName) {
			_zendeskThemeSetBoldFontName(boldFontName);
		}

		#endif
	}
}		

