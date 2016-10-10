using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	/// <summary>
	/// Appearance specifications for customizing the Zendesk interface on iOS.
	///
	/// IOSAppearance appearance = new IOSAppearance ();
	///	appearance.StartWithBaseTheme ();
	/// appearance.SetPrimaryTextColor(new ZenColor 0.0f, 1.0f, 0.0f));
	///	appearance.ApplyTheme ();
	///
	/// See <a href="https://support.zendesk.com/hc/en-us/articles/220043467-Moving-from-appearance-selectors-to-ZDKTheme-properties-Support-SDK-for-iOS-v1-6-">Using ZDKTheme</a>
	///
	/// </summary>
	public class IOSAppearance {

		public IOSAppearance() {
			// Intentionally empty
		}

		#if UNITY_IOS
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

		/// <summary>
		/// This method must be called before you change any colors
		/// </summary>
		public void StartWithBaseTheme() {
			_zendeskThemeStartWithBaseTheme ();
		}

		/// <summary>
		/// This method must be called to apply any changes that you have made
		/// </summary>
		public void ApplyTheme() {
			_zendeskThemeApplyTheme();
		}

		/// <summary>
		/// Sets the color of the primary text.
		/// </summary>
		/// <param name="color">Color.</param>
		public void SetPrimaryTextColor(ZenColor color) {
			_zendeskThemeSetPrimaryTextColor(color.Red, color.Green, color.Blue, color.Alpha);
		}

		/// <summary>
		/// Sets the color of the secondary text.
		/// </summary>
		/// <param name="color">Color.</param>
		public void SetSecondaryTextColor(ZenColor color) {
			_zendeskThemeSetSecondaryTextColor(color.Red, color.Green, color.Blue, color.Alpha);
		}

		/// <summary>
		/// Sets the color of the primary background.
		/// </summary>
		/// <param name="color">Color.</param>
		public void SetPrimaryBackgroundColor(ZenColor color) {
			_zendeskThemeSetPrimaryBackgroundColor(color.Red, color.Green, color.Blue, color.Alpha);
		}

		/// <summary>
		/// Sets the color of the secondary background.
		/// </summary>
		/// <param name="color">Color.</param>
		public void SetSecondaryBackgroundColor(ZenColor color) {
			_zendeskThemeSetSecondaryBackgroundColor(color.Red, color.Green, color.Blue, color.Alpha);
		}

		/// <summary>
		/// Sets the empty color of the background.
		/// </summary>
		/// <param name="color">Color.</param>
		public void SetEmptyBackgroundColor(ZenColor color) {
			_zendeskThemeSetEmptyBackgroundColor(color.Red, color.Green, color.Blue, color.Alpha);
		}

		/// <summary>
		/// Sets the color of the meta text.
		/// </summary>
		/// <param name="color">Color.</param>
		public void SetMetaTextColor(ZenColor color) {
			_zendeskThemeSetMetaTextColor(color.Red, color.Green, color.Blue, color.Alpha);
		}

		/// <summary>
		/// Sets the color of the separator.
		/// </summary>
		/// <param name="color">Color.</param>
		public void SetSeparatorColor(ZenColor color) {
			_zendeskThemeSetSeparatorColor(color.Red, color.Green, color.Blue, color.Alpha);
		}

		/// <summary>
		/// Sets the color of the input field.
		/// </summary>
		/// <param name="color">Color.</param>
		public void SetInputFieldColor(ZenColor color) {
			_zendeskThemeSetInputFieldColor(color.Red, color.Green, color.Blue, color.Alpha);
		}

		/// <summary>
		/// Sets the color of the input field background.
		/// </summary>
		/// <param name="color">Color.</param>
		public void SetInputFieldBackgroundColor(ZenColor color) {
			_zendeskThemeSetInputFieldBackgroundColor(color.Red, color.Green, color.Blue, color.Alpha);
		}

		/// <summary>
		/// Sets the name of the font.
		/// </summary>
		/// <param name="fontName">Font name.</param>
		public void SetFontName(string fontName) {
			_zendeskThemeSetFontName(fontName);
		}

		/// <summary>
		/// Sets the name of the bold font.
		/// </summary>
		/// <param name="boldFontName">Bold font name.</param>
		public void SetBoldFontName(string boldFontName) {
			_zendeskThemeSetBoldFontName(boldFontName);
		}

		#endif
	}
}

