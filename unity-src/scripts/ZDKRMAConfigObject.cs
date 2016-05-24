using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	public enum ZDKRMAAction {
		ZDKRMARateApp = 1, // Enumeration for rating an app.
		ZDKRMASendFeedback = 2, // Enumeration for sending feedback.
		ZDKRMADontAskAgain = 3, // Enumeration for choosing to never be prompted for feedback again.
		ZDKRMAUndisplayed = 4 // Enumeration for an undisplayed option.
	};

	/// <summary>
	/// ZDKRMAConfigObject encapsulates the configurable properties of ZDKRMA. A 
	/// ZDKRMAConfigObject is supplied to the ZDKRMA configure method:
	/// @see + configure: in ZDKRMA.h
	/// To override the default settings of ZDKRMA, call the configure method and 
	/// set new values as desired.
	/// </summary>
	public class ZDKRMAConfigObject {
		private static string _logTag = "ZDKRMAConfigObject";
		
		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}

		/// <summary>
		/// Additional tags to be set on the new request.
		/// Default value is nil.
		/// </summary>
		public string[] AdditionalTags;

		/// <summary>
		/// Additional info to be included in the new request, this will be added 
		/// above the device info and below the user request.
		/// Default value is nil.
		/// </summary>
		public string AdditionalRequestInfo;

		/// <summary>
		/// Subject to be included in the request. Android only
		/// Default value is nil.
		/// </summary>
		public string RequestSubject;

		/// <summary>
		/// An array that specifies the options in the dialog view.
		/// Default value is @[ZDKRMARateApp, ZDKRMASendFeedback, ZDKRMADontAskAgain]
		/// </summary>
		public ZDKRMAAction[] DialogActions;

		/// <summary>
		/// The name of the image shown on successful feedback submission. The image will
		/// be centred in the available area, which is 500 x 160. Images larger than this
		/// will be cropped.
		/// Default value is nil.
		/// </summary>
		public string SuccessImageName;

		/// <summary>
		/// The name of the image shown feedback submission fails. The image will
		/// be centred in the available area, which is 500 x 160. Images larger than this
		/// will be cropped.
		/// Default value is nil.
		/// </summary>
		public string ErrorImageName;

		/// <summary>
		/// Show the dialog no matter what if true. Default is false.
		/// </summary>
		public bool Always;

		public ZDKRMAConfigObject() {
			AdditionalTags = null;
			AdditionalRequestInfo = null;
			RequestSubject = null;
			DialogActions = null;
			SuccessImageName = null;
			ErrorImageName = null;
			Always = false;
		}
	}
}		