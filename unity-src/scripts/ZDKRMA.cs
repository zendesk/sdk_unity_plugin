using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	/// <summary>
	/// ZDKRMA contains interfaces for configuring and displaying a Rate My App dialog.
	/// </summary>
	public class ZDKRMA : ZDKBaseComponent {
		
		private static ZDKRMA _instance;
		
		private static ZDKRMA instance() {
			if (_instance != null)
				return _instance;
			_instance = new ZDKRMA();
			return _instance;
		}

		override protected string GetIOsMethodPrefix() {
			return "_zendeskRMA";
		}

		/// <summary>
		/// To show the ZDKRMA dialog in a view, call this methed
		/// </summary>
		public static void Show() {
			instance().Do("showInView", false);
		}

		/// <summary>
		/// To show the ZDKRMA dialog in a view, call this methed.
		/// This method will always show a dialog irrespective of
		/// the settings in ZDKRMAConfigObject.
		/// </summary>
		public static void ShowAlways() {
			instance().Do("showInView", true);
		}

		/// <summary>
		/// To show the ZDKRMA dialog in a view, call this methed
		/// </summary>
		/// <param name="config">A ZDKRMAConfigObject to configure the Rate My App interface.</param>
		public static void Show(ZDKRMAConfigObject config) {
			#if UNITY_ANDROID
			//An array of booleans that represent which button to include in the dialog
			bool[] dialogBools = new bool[3];
			if (config.DialogActions != null) {
				for (int i = 0; i < config.DialogActions.Length; i++) {
					if (config.DialogActions [i] == ZDKRMAAction.ZDKRMARateApp) {
						dialogBools [0] = true;
					} else if (config.DialogActions [i] == ZDKRMAAction.ZDKRMASendFeedback) {
						dialogBools [1] = true;
					} else if (config.DialogActions [i] == ZDKRMAAction.ZDKRMADontAskAgain) {
						dialogBools [2] = true;
					}
				}
			}
			//If the config.DialogActions was null then the default is to add all the buttons
			else {
				for (int i = 0; i < dialogBools.Length; i++) {
					dialogBools[i] = true;
				}
			}
			
			if (config.AdditionalTags == null)
				config.AdditionalTags = new string[0];
			instance().Do("showInViewWithConfig", config.Always,
									              config.AdditionalTags,
									              config.AdditionalRequestInfo,
									              dialogBools,
									              config.RequestSubject);
			#else
			if (config.AdditionalTags == null)
				config.AdditionalTags = new string[0];
			if (config.DialogActions == null)
				config.DialogActions = new ZDKRMAAction[0];
			instance().Do("showInViewWithConfig", config.AdditionalTags,
									               config.AdditionalTags.Length,
									               config.AdditionalRequestInfo,
									               config.DialogActions,
									               config.DialogActions.Length,
									               config.SuccessImageName,
									               config.ErrorImageName,
			              						   config.Always);
			#endif
		}
		
		/// <summary>
		/// Log a visit. The first call to logVisit sets the initial visitCount and sets the 
		/// initialCheckDate. visitCount and initialCheckDate are passed to the shouldShowBlock. 
		/// The default shouldShowBlock checks that a threshold of 15 visits has been reached 
		/// and that 7 days have past since the initialCheckDate. iOS only
		///
		/// You should call LogVisit where you want to track user visits.
		/// If you use the default ZDKRMA setup you need to include a call to logVisit somewhere in your code.
		/// </summary>
		public static void LogVisitiOS() {
			instance().DoIOS("logVisit");
		}
		
		#if UNITY_IPHONE

		[DllImport("__Internal")]
		private static extern void _zendeskRMAShowInView(bool always);
		[DllImport("__Internal")]
		private static extern void _zendeskRMAShowInViewWithConfig(string[] additionalTags,
                                                                   int additionalTagsLength,
                                                                   string additionalRequestInfo,
                                                                   ZDKRMAAction[] dialogActions,
                                                                   int dialogActionsLength,
                                                                   string successImageName,
                                                                   string errorImageName,
		                                                           bool always);
		
		[DllImport("__Internal")]
		private static extern void _zendeskRMALogVisit();

		#endif
	}
}
