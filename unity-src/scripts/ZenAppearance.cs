using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	public enum UIBARSTYLE {
		UIBARSTYLEDEFAULT,
		UIBARSTYLEBLACK
	};
	
	public enum UIACTIONSHEETSTYLE {
		UIACTIONSHEETSTYLEAUTOMATIC = -1,
		UIACTIONSHEETSTYLEDEFAULT = 0,
		UIACTIONSHEETSTYLEBLACKTRANSLUCENT = 1,
		UIACTIONSHEETSTYLEBLACKOPAQUE = 2
	};

	public enum UIACTIVITYINDICATORVIEWSTYLE {
		UIACTIVITYINDICATORVIEWSTYLEWHITELARGE,
		UIACTIVITYINDICATORVIEWSTYLEWHITE,
		UIACTIVITYINDICATORVIEWSTYLEGRAY
	};

	/// <summary>
	/// Appearance specifications for customizing the Zendesk
	/// interface on iOS.
	/// </summary>
	public class IOSAppearance {
		
		private static string _logTag = "Appearance";		
		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}

		private string _className;

		public IOSAppearance(string className) {
			_className = className;
		}
		
		#if UNITY_EDITOR || !UNITY_IPHONE

		public void SetFont(string propertyName, string fontName, float size) {
			Log("Unity : " + _className + ":Appearance:SetFont");
		}
		
		public void SetColor(string propertyName, ZenColor color) {
			Log("Unity : " + _className + ":Appearance:SetColorWithWhite");
		}

		public void SetEdgeInsets(string propertyName, float top, float left, float bottom, float right) {
			Log("Unity : " + _className + ":Appearance:SetEdgeInsets");
		}

		public void SetBorderWidth(string propertyName, float width) {
			Log("Unity : " + _className + ":Appearance:SetBorderWidth");
		}

		public void SetCornerRadius(string propertyName, float width) {
			Log("Unity : " + _className + ":Appearance:SetCornerRadius");
		}

		public void SetAutomaticallyHideNavBarOnLandscape(int enabled) {
			Log("Unity : " + _className + ":Appearance:SetAutomaticallyHideNavBarOnLandscape");
		}

		public void SetAvatarSize(float size) {
			Log("Unity : " + _className + ":Appearance:SetAvatarSize");
		}

		public void SetVerticalMargin(float size) {
			Log("Unity : " + _className + ":Appearance:SetVerticalMargin");
		}

		public void SetDescriptionTimestampMargin(float size) {
			Log("Unity : " + _className + ":Appearance:SetDescriptionTimestampMargin");
		}
		
		public void SetLeftInset(float size) {
			Log("Unity : " + _className + ":Appearance:SetLeftInset");
		}

		public void SetSearchBarStyle(UIBARSTYLE style) {
			Log("Unity : " + _className + ":Appearance:SetSearchBarStyle");
		}
		
		public void SetAttachmentActionSheetStyle(UIACTIONSHEETSTYLE style) {
			Log("Unity : " + _className + ":Appearance:SetAttachmentActionSheetStyle");
		}

		public void SetSpinnerUIActivityIndicatorViewStyle(UIACTIVITYINDICATORVIEWSTYLE style) {
			Log("Unity : " + _className + ":Appearance:SetSpinnerUIActivityIndicatorViewStyle");
		}

		public void SetAttachmentButtonImage(string imageName, string type) {
			Log("Unity : " + _className + ":Appearance:SetAttachmentButtonImage");
		}

		#elif UNITY_IPHONE
		
		[DllImport("__Internal")]
		private static extern void _zendeskSetFont(string className, string propertyName, string fontName, float size);
		[DllImport("__Internal")]
		private static extern void _zendeskSetColorWithWhite(string className, string propertyName, float white, float alpha);
		[DllImport("__Internal")]
		private static extern void _zendeskSetColorWithRed(string className, string propertyName, float red, float green, float blue, float alpha);
		[DllImport("__Internal")]
		private static extern void _zendeskSetColorWithName(string className, string propertyName, string colorName);
		[DllImport("__Internal")]
		private static extern void _zendeskSetEdgeInsets(string className, string propertyName, float top, float left, float bottom, float right);
		[DllImport("__Internal")]
		private static extern void _zendeskSetBorderWidth(string className, string propertyName, float width);
		[DllImport("__Internal")]
		private static extern void _zendeskSetCornerRadius(string className, string propertyName, float width);
		[DllImport("__Internal")]
		private static extern void _zendeskSetAutomaticallyHideNavBarOnLandscape(string className, int enabled);
		[DllImport("__Internal")]
		private static extern void _zendeskSetAvatarSize(string className, float size);
		[DllImport("__Internal")]
		private static extern void _zendeskSetVerticalMargin(string className, float size);
		[DllImport("__Internal")]
		private static extern void _zendeskSetDescriptionTimestampMargin(string className, float size);
		[DllImport("__Internal")]
		private static extern void _zendeskSetLeftInset(string className, float size);
		[DllImport("__Internal")]
		private static extern void _zendeskSetSearchBarStyle(string className, int style);
		[DllImport("__Internal")]
		private static extern void _zendeskSetAttachmentActionSheetStyle(string className, int style);
		[DllImport("__Internal")]
		private static extern void _zendeskSetSpinnerUIActivityIndicatorViewStyle(string className, int style);
		[DllImport("__Internal")]
		private static extern void _zendeskSetAttachmentButtonImage(string className, string imageName, string type);

		public void SetFont(string propertyName, string fontName, float size) {
			_zendeskSetFont(_className, propertyName, fontName, size);
		}

		public void SetColor(string propertyName, ZenColor color) {
			if (color.Type == ZenColor.COLORTYPE.WHITEONLY) {
				_zendeskSetColorWithWhite(_className, propertyName, color.White, color.Alpha);
			}
			else if (color.Type == ZenColor.COLORTYPE.REDGREENBLUE) {
				_zendeskSetColorWithRed(_className, propertyName, color.Red, color.Green, color.Blue, color.Alpha);
			}
			else if (color.Type == ZenColor.COLORTYPE.BYNAME) {
				_zendeskSetColorWithName(_className, propertyName, color.Name);
			}
		}

		public void SetEdgeInsets(string propertyName, float top, float left, float bottom, float right) {
			_zendeskSetEdgeInsets(_className, propertyName, top, left, bottom, right);
		}

		public void SetBorderWidth(string propertyName, float width) {
			_zendeskSetBorderWidth(_className, propertyName, width);
		}
		
		public void SetCornerRadius(string propertyName, float width) {
			_zendeskSetCornerRadius(_className, propertyName, width);
		}

		public void SetAutomaticallyHideNavBarOnLandscape(int enabled) {
			_zendeskSetAutomaticallyHideNavBarOnLandscape(_className, enabled);
		}

		public void SetAvatarSize(float size) {
			_zendeskSetAvatarSize(_className, size);
		}

		public void SetVerticalMargin(float size) {
			_zendeskSetVerticalMargin(_className, size);
		}
		
		public void SetDescriptionTimestampMargin(float size) {
			_zendeskSetDescriptionTimestampMargin(_className, size);
		}
		
		public void SetLeftInset(float size) {
			_zendeskSetLeftInset(_className, size);
		}

		public void SetSearchBarStyle(UIBARSTYLE style) {
			_zendeskSetSearchBarStyle(_className, (int) style);
		}
		
		public void SetAttachmentActionSheetStyle(UIACTIONSHEETSTYLE style) {
			_zendeskSetAttachmentActionSheetStyle(_className, (int) style);
		}

		public void SetSpinnerUIActivityIndicatorViewStyle(UIACTIVITYINDICATORVIEWSTYLE style) {
			_zendeskSetSpinnerUIActivityIndicatorViewStyle(_className, (int) style);
		}

		public void SetAttachmentButtonImage(string imageName, string type) {
			_zendeskSetAttachmentButtonImage(_className, imageName, type);
		}

		#endif
	}
}		

