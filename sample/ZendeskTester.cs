using System;
using UnityEngine;
using System.Collections;
using ZendeskSDK;

public class ZendeskTester: MonoBehaviour
{
	private Texture2D avatarTexture;
	private bool pushEnabled = false;

	private int textureSize = 60;
	private Vector2 scrollPosition = Vector2.zero;

	/** initialize zendesk in the Awake() method of the GameObject a script of yours is attached to */
	void Awake() {
		avatarTexture = new Texture2D(textureSize, textureSize);
		ZendeskSDK.ZDKLogger.Enable (true);
		ZendeskSDK.ZDKConfig.Initialize (gameObject); // DontDestroyOnLoad automatically called on your supplied gameObject
	}

	/** must include this method for android to behave properly */
	void OnApplicationPause(bool pauseStatus) {
		ZendeskSDK.ZDKConfig.OnApplicationPause (pauseStatus);
	}

	/** must include this method for any zendesk callbacks to work */
	void OnZendeskCallback(string results) {
		Debug.Log("OnZendeskCallback - " + MakeResultString(results));
		ZDKConfig.CallbackResponse (results);
	}

	void OnEnable() {

	}

	void Update() {

	}

	void OnGUI() {

		// 240 is a magical number, derived from a scale factor of 2 looking well on a 480*800 device and 4.5 on a 1080*1920 device
		float scaleFactor = Screen.width / 240f;
		float screenWidth = Screen.width / scaleFactor;
		float screenHeight = Screen.height / scaleFactor;
		float spacerSize = 10f;

		GUI.matrix = Matrix4x4.Scale (new Vector3 (scaleFactor, scaleFactor, scaleFactor));

		float buttonAreaOriginX = 0f;
		float buttonAreaOriginY = textureSize + 10f;
		float buttonAreaWidth = screenWidth;
		float buttonAreaHeight = screenHeight;

		GUI.skin.button.margin = new RectOffset (10, 10, 10, 10);

		GUI.DrawTexture(new Rect(10.0f, 10.0f, (float)textureSize, (float)textureSize), avatarTexture, ScaleMode.ScaleToFit, true);
		GUILayout.Space ((float)textureSize + spacerSize);

		GUILayout.BeginVertical();
		GUILayout.BeginArea(new Rect(buttonAreaOriginX, buttonAreaOriginY, buttonAreaWidth, buttonAreaHeight - textureSize));
		scrollPosition = GUILayout.BeginScrollView (scrollPosition);

		GUILayoutOption buttonWidth = GUILayout.Width (screenWidth - (spacerSize * 2));

		if (GUILayout.Button ("Initialize SDK", buttonWidth)) {
			//ZendeskSDK.ZDKConfig.AuthenticateAnonymousIdentity("","","");
			ZendeskSDK.ZDKConfig.AuthenticateJwtUserIdentity ("MyTestID");
		}

        if (GUILayout.Button ("Set Custom Fields", buttonWidth)) {
			Hashtable customFields = new Hashtable ();
			string customFieldId = "customFieldId";
			customFields[customFieldId] = "customFieldValue";

			ZendeskSDK.ZDKConfig.SetCustomFields (customFields);
		}

		if (GUILayout.Button ("Set Contact Configuration", buttonWidth)) {
            string[] tags = new string[2];
        	tags[0] = "unityTag1";
        	tags[1] = "unityTag2";

            ZendeskSDK.ZDKConfig.ContactConfig contactConfig = new ZDKConfig.ContactConfig()
                	.WithTags(tags)
                	.WithAdditionalInfo("AdditionalRequestInfo TEST")
                	.WithRequestSubject("Unity Custom Support Request");

            ZendeskSDK.ZDKConfig.SetContactConfiguration (contactConfig);
        }


		if (GUILayout.Button (pushEnabled ? "Disable Push" : "Enable Push", buttonWidth)) {
			if (!pushEnabled) {
				ZendeskSDK.ZDKPush.Enable((result, error) => {
					if (error != null) {
						Debug.Log("ERROR: ZDKPush.Enable - " + error.Description);
					}
					else {
						pushEnabled = true;
						Debug.Log("ZDKPush.Enable Successful Callback - " + MakeResultString(result));
					}
				});
			} else {
				ZendeskSDK.ZDKPush.Disable((result, error) => {
					if (error != null) {
						Debug.Log("ERROR: ZDKPush.Disable - " + error.Description);
					}
					else {
						pushEnabled = false;
						Debug.Log("ZDKPush.Disable Successful Callback - " + MakeResultString(result));
					}
				});
			}
		}

		if (GUILayout.Button ("Show Help Center", buttonWidth)) {
			ZendeskSDK.ZDKHelpCenter.ShowHelpCenter (
				ZendeskSDK.ZDKHelpCenter.ShowOptions.ListCategories()
				.SetShowContactUsButton(true));
		}

		if (GUILayout.Button ("Show Help Center Section", buttonWidth)) {
			ZendeskSDK.ZDKHelpCenter.ShowHelpCenter (
				ZendeskSDK.ZDKHelpCenter.ShowOptions.ListArticles(200658665)
				.SetShowContactUsButton(false));
		}

		if (GUILayout.Button ("Show Request Creation", buttonWidth)) {
			ZendeskSDK.ZDKRequests.ShowRequestCreation ();
		}

		if (GUILayout.Button ("Show Requests List", buttonWidth)) {
			ZendeskSDK.ZDKRequests.ShowRequestList ();
		}

		if (GUILayout.Button ("Show Rate My App", buttonWidth)) {
			ZendeskSDK.ZDKRMA.ShowAlways ();
		}

		if (GUILayout.Button ("Show Rate My App Config", buttonWidth)) {
			string[] additionalTags = new string[2];
			additionalTags[0] = "Additional Config Tag 0";
			additionalTags[1] = "Additional Config Tag 1";
			ZDKRMAAction[] dialogActions = new ZDKRMAAction[3];
			dialogActions[0] = ZDKRMAAction.ZDKRMARateApp;
			dialogActions[1] = ZDKRMAAction.ZDKRMASendFeedback;
			dialogActions[2] = ZDKRMAAction.ZDKRMADontAskAgain;

			ZDKRMAConfigObject config = new ZDKRMAConfigObject();
			config.AdditionalTags = additionalTags;
			config.AdditionalRequestInfo = "AdditionalRequestInfo TEST";
			config.DialogActions = dialogActions;
			config.SuccessImageName = null;
			config.ErrorImageName = null;
			ZendeskSDK.ZDKRMA.Show (config);
		}

		if (GUILayout.Button ("Run Provider Tests", buttonWidth)) {
			RunProviderTests ();
		}

		if (GUILayout.Button ("Run Appearance Tests", buttonWidth)) {
			RunAppearanceTests ();
		}

		if (GUILayout.Button ("Run SDK Tests", buttonWidth)) {
			ZendeskSDK.ZDKConfig.SetUserLocale ("en-us");
			ZendeskSDK.ZDKConfig.SetCoppaEnabled (true);

			ZendeskSDK.ZDKLogger.Enable (true);
			ZendeskSDK.ZDKLogger.SetLogLevelIOS (LogLevel.Verbose);

			// ZDKDeviceInfo Tests
			Debug.Log (string.Format ("Device Type: {0}", ZendeskSDK.ZDKDeviceInfo.DeviceTypeiOS ()));
			Debug.Log (string.Format ("Total Device Memory: {0}", ZendeskSDK.ZDKDeviceInfo.TotalDeviceMemoryiOS ()));
			Debug.Log (string.Format ("Free Disk Space: {0}", ZendeskSDK.ZDKDeviceInfo.FreeDiskspaceiOS ()));
			Debug.Log (string.Format ("Total Disk Space: {0}", ZendeskSDK.ZDKDeviceInfo.TotalDiskspaceiOS ()));
			Debug.Log (string.Format ("Battery Level: {0}", ZendeskSDK.ZDKDeviceInfo.BatteryLeveliOS ()));
			Debug.Log (string.Format ("Region: {0}", ZendeskSDK.ZDKDeviceInfo.RegioniOS ()));
			Debug.Log (string.Format ("Language: {0}", ZendeskSDK.ZDKDeviceInfo.LanguageiOS ()));
			Debug.Log (string.Format ("Device Info String: {0}", ZendeskSDK.ZDKDeviceInfo.DeviceInfoString ()));
			Hashtable deviceInfoDict = ZendeskSDK.ZDKDeviceInfo.DeviceInfoDictionary ();
			Debug.Log (string.Format ("Device Info Dictionary:"));

			foreach (string key in deviceInfoDict.Keys) {
				Debug.Log (string.Format ("{0}: {1}", key, deviceInfoDict [key]));
			}

			// ZDKStringUtil Tests
			string[] strings = new string[2];
			strings [0] = "one";
			strings [1] = "second";
			Debug.Log (string.Format ("CSVStringFromArray: {0}", ZendeskSDK.ZDKStringUtil.CsvStringFromArray (strings)));

			// ZDKLogger Tests
			ZendeskSDK.ZDKLogger.Enable (true);
		}

		GUI.EndScrollView();
		GUILayout.EndArea();
		GUILayout.EndVertical ();



	}

	void RunAppearanceTests() {
		ZenColor testColor1 = new ZenColor(0.9f, 1.0f); // Random Gray color

		ZendeskSDK.ZDKRequestListLoadingTableCell.SetBackgroundColor(testColor1);
		ZendeskSDK.ZDKRequestListLoadingTableCell.SetSeparatorInset(0, 0, 0, 0);

		ZendeskSDK.ZDKRequestListEmptyTableCell.SetMessageFont("system", 14);
		ZendeskSDK.ZDKRequestListEmptyTableCell.SetMessageColor(testColor1);
		ZendeskSDK.ZDKRequestListEmptyTableCell.SetBackgroundColor(testColor1);
		ZendeskSDK.ZDKRequestListEmptyTableCell.SetSeparatorInset(0, 0, 0, 0);

		ZendeskSDK.ZDKUILoadingView.SetBackgroundColor(testColor1);

		ZendeskSDK.ZDKUIImageScrollView.SetBackgroundColor(testColor1);

		ZendeskSDK.ZDKSupportView.SetNoResultsContactButtonFont("system", 14);
		ZendeskSDK.ZDKSupportView.SetBackgroundColor(testColor1);
		ZendeskSDK.ZDKSupportView.SetTableBackgroundColor(testColor1);
		ZendeskSDK.ZDKSupportView.SetSeparatorColor(testColor1);
		ZendeskSDK.ZDKSupportView.SetNoResultsFoundLabelColor(testColor1);
		ZendeskSDK.ZDKSupportView.SetNoResultsFoundLabelBackground(testColor1);
		ZendeskSDK.ZDKSupportView.SetNoResultsContactButtonBackground(testColor1);
		ZendeskSDK.ZDKSupportView.SetNoResultsContactButtonBorderColor(testColor1);
		ZendeskSDK.ZDKSupportView.SetNoResultsContactButtonTitleColorNormal(testColor1);
		ZendeskSDK.ZDKSupportView.SetNoResultsContactButtonTitleColorHighlighted(testColor1);
		ZendeskSDK.ZDKSupportView.SetNoResultsContactButtonTitleColorDisabled(testColor1);
		ZendeskSDK.ZDKSupportView.SetNoResultsContactButtonEdgeInsets(0, 0, 0, 0);
		ZendeskSDK.ZDKSupportView.SetNoResultsContactButtonBorderWidth(5);
		ZendeskSDK.ZDKSupportView.SetNoResultsContactButtonCornerRadius(15);
		ZendeskSDK.ZDKSupportView.SetAutomaticallyHideNavBarOnLandscape(1);
		ZendeskSDK.ZDKSupportView.SetSearchBarStyle(UIBARSTYLE.UIBARSTYLEDEFAULT);
		ZendeskSDK.ZDKSupportView.SetSpinnerUIActivityIndicatorViewStyle(UIACTIVITYINDICATORVIEWSTYLE.UIACTIVITYINDICATORVIEWSTYLEGRAY);
		ZendeskSDK.ZDKSupportView.SetSpinnerColor(testColor1);

		ZendeskSDK.ZDKSupportTableViewCell.SetTitleLabelFont("system", 14);
		ZendeskSDK.ZDKSupportTableViewCell.SetBackgroundColor(testColor1);
		ZendeskSDK.ZDKSupportTableViewCell.SetTitleLabelColor(testColor1);
		ZendeskSDK.ZDKSupportTableViewCell.SetTitleLabelBackground(testColor1);
		ZendeskSDK.ZDKSupportTableViewCell.SetSeparatorInset(0, 0, 0, 0);

		ZendeskSDK.ZDKSupportAttachmentCell.SetTitleLabelFont("system", 14);
		ZendeskSDK.ZDKSupportAttachmentCell.SetBackgroundColor(testColor1);
		ZendeskSDK.ZDKSupportAttachmentCell.SetFileSizeLabelColor(testColor1);
		ZendeskSDK.ZDKSupportAttachmentCell.SetFileSizeLabelBackground(testColor1);
		ZendeskSDK.ZDKSupportAttachmentCell.SetTitleLabelColor(testColor1);
		ZendeskSDK.ZDKSupportAttachmentCell.SetTitleLabelBackground(testColor1);
		ZendeskSDK.ZDKSupportAttachmentCell.SetSeparatorInset(0, 0, 0, 0);

		ZendeskSDK.ZDKSupportArticleTableViewCell.SetArticleParentsLabelFont("system", 14);
		ZendeskSDK.ZDKSupportArticleTableViewCell.SetTitleLabelFont("system", 14);
		ZendeskSDK.ZDKSupportArticleTableViewCell.SetBackgroundColor(testColor1);
		ZendeskSDK.ZDKSupportArticleTableViewCell.SetArticleParentsLabelColor(testColor1);
		ZendeskSDK.ZDKSupportArticleTableViewCell.SetArticleParentsLabelBackground(testColor1);
		ZendeskSDK.ZDKSupportArticleTableViewCell.SetTitleLabelColor(testColor1);
		ZendeskSDK.ZDKSupportArticleTableViewCell.SetTitleLabelBackground(testColor1);
		ZendeskSDK.ZDKSupportArticleTableViewCell.SetSeparatorInset(0, 0, 0, 0);

		ZendeskSDK.ZDKRMAFeedbackView.SetSubheaderFont("system", 14);
		ZendeskSDK.ZDKRMAFeedbackView.SetHeaderFont("system", 14);
		ZendeskSDK.ZDKRMAFeedbackView.SetTextEntryFont("system", 14);
		ZendeskSDK.ZDKRMAFeedbackView.SetButtonFont("system", 14);
		ZendeskSDK.ZDKRMAFeedbackView.SetButtonColor(testColor1);
		ZendeskSDK.ZDKRMAFeedbackView.SetButtonSelectedColor(testColor1);
		ZendeskSDK.ZDKRMAFeedbackView.SetButtonBackgroundColor(testColor1);
		ZendeskSDK.ZDKRMAFeedbackView.SetSeparatorLineColor(testColor1);
		ZendeskSDK.ZDKRMAFeedbackView.SetBackgroundColor(testColor1);
		ZendeskSDK.ZDKRMAFeedbackView.SetHeaderColor(testColor1);
		ZendeskSDK.ZDKRMAFeedbackView.SetSubHeaderColor(testColor1);
		ZendeskSDK.ZDKRMAFeedbackView.SetTextEntryColor(testColor1);
		ZendeskSDK.ZDKRMAFeedbackView.SetTextEntryBackgroundColor(testColor1);
		ZendeskSDK.ZDKRMAFeedbackView.SetPlaceHolderColor(testColor1);
		ZendeskSDK.ZDKRMAFeedbackView.SetSpinnerUIActivityIndicatorViewStyle (UIACTIVITYINDICATORVIEWSTYLE.UIACTIVITYINDICATORVIEWSTYLEGRAY);
		ZendeskSDK.ZDKRMAFeedbackView.SetSpinnerColor(testColor1);

		ZendeskSDK.ZDKRMADialogView.SetHeaderFont("system", 14);
		ZendeskSDK.ZDKRMADialogView.SetButtonFont("system", 14);
		ZendeskSDK.ZDKRMADialogView.SetHeaderBackgroundColor(testColor1);
		ZendeskSDK.ZDKRMADialogView.SetHeaderColor(testColor1);
		ZendeskSDK.ZDKRMADialogView.SetSeparatorLineColor(testColor1);
		ZendeskSDK.ZDKRMADialogView.SetButtonBackgroundColor(testColor1);
		ZendeskSDK.ZDKRMADialogView.SetButtonSelectedBackgroundColor(testColor1);
		ZendeskSDK.ZDKRMADialogView.SetButtonColor(testColor1);
		ZendeskSDK.ZDKRMADialogView.SetBackgroundColor(testColor1);

		ZendeskSDK.ZDKRequestCommentTableCell.SetBackgroundColor(testColor1);
		ZendeskSDK.ZDKRequestCommentTableCell.SetSeparatorInset(0, 0, 0, 0);

		ZendeskSDK.ZDKRequestListTableCell.SetDescriptionFont("system", 14);
		ZendeskSDK.ZDKRequestListTableCell.SetCreatedAtFont("system", 14);
		ZendeskSDK.ZDKRequestListTableCell.SetUnreadColor(testColor1);
		ZendeskSDK.ZDKRequestListTableCell.SetDescriptionColor(testColor1);
		ZendeskSDK.ZDKRequestListTableCell.SetCreatedAtColor(testColor1);
		ZendeskSDK.ZDKRequestListTableCell.SetCellBackgroundColor(testColor1);
		ZendeskSDK.ZDKRequestListTableCell.SetBackgroundColor(testColor1);
		ZendeskSDK.ZDKRequestListTableCell.SetVerticalMargin(0);
		ZendeskSDK.ZDKRequestListTableCell.SetDescriptionTimestampMargin(0);
		ZendeskSDK.ZDKRequestListTableCell.SetLeftInset(0);
		ZendeskSDK.ZDKRequestListTableCell.SetSeparatorInset(0, 0, 0, 0);

		ZendeskSDK.ZDKRequestListTable.SetCellSeparatorColor(testColor1);
		ZendeskSDK.ZDKRequestListTable.SetTableBackgroundColor(testColor1);
		ZendeskSDK.ZDKRequestListTable.SetBackgroundColor(testColor1);
		ZendeskSDK.ZDKRequestListTable.SetSectionIndexColor(testColor1);
		ZendeskSDK.ZDKRequestListTable.SetSectionIndexBackgroundColor(testColor1);
		ZendeskSDK.ZDKRequestListTable.SetSectionIndexTrackingBackgroundColor(testColor1);
		ZendeskSDK.ZDKRequestListTable.SetSeparatorColor(testColor1);
		ZendeskSDK.ZDKRequestListTable.SetSeparatorInset(0, 0, 0, 0);

		ZendeskSDK.ZDKRequestCommentAttachmentTableCell.SetBackgroundColor(testColor1);
		ZendeskSDK.ZDKRequestCommentAttachmentTableCell.SetSeparatorInset(0, 0, 0, 0);

		ZendeskSDK.ZDKRequestCommentAttachmentLoadingTableCell.SetBackgroundColor(testColor1);
		ZendeskSDK.ZDKRequestCommentAttachmentLoadingTableCell.SetSeparatorInset(0, 0, 0, 0);

		ZendeskSDK.ZDKEndUserCommentTableCell.SetBackgroundColor(testColor1);
		ZendeskSDK.ZDKEndUserCommentTableCell.SetBodyFont("system", 14);
		ZendeskSDK.ZDKEndUserCommentTableCell.SetTimestampFont("system", 14);
		ZendeskSDK.ZDKEndUserCommentTableCell.SetBodyColor(testColor1);
		ZendeskSDK.ZDKEndUserCommentTableCell.SetTimestampColor(testColor1);
		ZendeskSDK.ZDKEndUserCommentTableCell.SetCellBackground(testColor1);
		ZendeskSDK.ZDKEndUserCommentTableCell.SetSeparatorInset(0, 0, 0, 0);

		ZendeskSDK.ZDKCreateRequestView.SetTextEntryFont("system", 14);
		ZendeskSDK.ZDKCreateRequestView.SetPlaceholderTextColor(testColor1);
		ZendeskSDK.ZDKCreateRequestView.SetTextEntryColor(testColor1);
		ZendeskSDK.ZDKCreateRequestView.SetTextEntryBackgroundColor(testColor1);
		ZendeskSDK.ZDKCreateRequestView.SetBackgroundColor(testColor1);
		ZendeskSDK.ZDKCreateRequestView.SetAttachmentButtonBorderColor(testColor1);
		ZendeskSDK.ZDKCreateRequestView.SetAttachmentButtonBackground(testColor1);
		ZendeskSDK.ZDKCreateRequestView.SetAttachmentButtonCornerRadius(0);
		ZendeskSDK.ZDKCreateRequestView.SetAttachmentButtonBorderWidth(0);
		ZendeskSDK.ZDKCreateRequestView.SetAutomaticallyHideNavBarOnLandscape(1);
		ZendeskSDK.ZDKCreateRequestView.SetAttachmentActionSheetStyle (UIACTIONSHEETSTYLE.UIACTIONSHEETSTYLEDEFAULT);
		ZendeskSDK.ZDKCreateRequestView.SetSpinnerUIActivityIndicatorViewStyle(UIACTIVITYINDICATORVIEWSTYLE.UIACTIVITYINDICATORVIEWSTYLEGRAY);
		ZendeskSDK.ZDKCreateRequestView.SetSpinnerColor(testColor1);
		ZendeskSDK.ZDKCreateRequestView.SetAttachmentButtonImage ("testimagename", "png");

		ZendeskSDK.ZDKCommentsListLoadingTableCell.SetBackgroundColor(testColor1);
		ZendeskSDK.ZDKCommentsListLoadingTableCell.SetLeftInset(0);
		ZendeskSDK.ZDKCommentsListLoadingTableCell.SetCellBackground(testColor1);
		ZendeskSDK.ZDKCommentsListLoadingTableCell.SetSeparatorInset(0, 0, 0, 0);

		ZendeskSDK.ZDKCommentInputView.SetAttachmentButtonBackgroundColor(testColor1);
		ZendeskSDK.ZDKCommentInputView.SetBackgroundColor(testColor1);
		ZendeskSDK.ZDKCommentInputView.SetTopBorderColor(testColor1);
		ZendeskSDK.ZDKCommentInputView.SetTextEntryColor(testColor1);
		ZendeskSDK.ZDKCommentInputView.SetTextEntryBackgroundColor(testColor1);
		ZendeskSDK.ZDKCommentInputView.SetTextEntryBorderColor(testColor1);
		ZendeskSDK.ZDKCommentInputView.SetSendButtonColor(testColor1);
		ZendeskSDK.ZDKCommentInputView.SetAreaBackgroundColor(testColor1);
		ZendeskSDK.ZDKCommentInputView.SetTextEntryFont("system", 14);
		ZendeskSDK.ZDKCommentInputView.SetSendButtonFont("system", 14);

		ZendeskSDK.ZDKAttachmentView.SetBackgroundColor(testColor1);
		ZendeskSDK.ZDKAttachmentView.SetCloseButtonBackgroundColor(testColor1);

		ZendeskSDK.ZDKAgentCommentTableCell.SetBackgroundColor(testColor1);
		ZendeskSDK.ZDKAgentCommentTableCell.SetAvatarSize (40);
		ZendeskSDK.ZDKAgentCommentTableCell.SetAgentNameFont("system", 14);
		ZendeskSDK.ZDKAgentCommentTableCell.SetBodyFont("system", 14);
		ZendeskSDK.ZDKAgentCommentTableCell.SetTimestampFont("system", 14);
		ZendeskSDK.ZDKAgentCommentTableCell.SetAgentNameColor(testColor1);
		ZendeskSDK.ZDKAgentCommentTableCell.SetBodyColor(testColor1);
		ZendeskSDK.ZDKAgentCommentTableCell.SetTimestampColor(testColor1);
		ZendeskSDK.ZDKAgentCommentTableCell.SetCellBackground(testColor1);
		ZendeskSDK.ZDKAgentCommentTableCell.SetSeparatorInset(0, 0, 0, 0);
	}

	void RunProviderTests() {

		// ZDKRequestProvider Tests

		string[] tags = new string[0];
		string[] attachments = new string[0];
		ZDKCreateRequest req = new ZDKCreateRequest("test@zendesk.com", "Test Subject", "Test Description", tags);
		ZendeskSDK.ZDKRequestProvider.CreateRequest(req, (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKRequestProvider.CreateRequest - " + error.Description);
			}
			else {
				Debug.Log("ZDKRequestProvider.CreateRequest Successful Callback - " + MakeResultString(result));
			}
		});

		req = new ZDKCreateRequest("test@zendesk.com", "Test Subject", "Test Description", tags, attachments);
		ZendeskSDK.ZDKRequestProvider.CreateRequest(req, (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKRequestProvider.CreateRequest - " + error.Description);
			}
			else {
				Debug.Log("ZDKRequestProvider.CreateRequest Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKRequestProvider.GetAllRequests((result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKRequestProvider.GetAllRequests - " + error.Description);
			}
			else {
				Debug.Log("ZDKRequestProvider.GetAllRequests Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKRequestProvider.GetAllRequests("Test Status", (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKRequestProvider.GetAllRequests - " + error.Description);
			}
			else {
				Debug.Log("ZDKRequestProvider.GetAllRequests Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKRequestProvider.GetComments("Test RequestID", (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKRequestProvider.GetComments - " + error.Description);
			}
			else {
				Debug.Log("ZDKRequestProvider.GetComments Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKRequestProvider.AddComment("Test Comment", "Test RequestID", (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKRequestProvider.AddComment - " + error.Description);
			}
			else {
				Debug.Log("ZDKRequestProvider.AddComment Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKRequestProvider.AddComment("Test Comment", "Test RequestID", attachments, (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKRequestProvider.AddComment2 - " + error.Description);
			}
			else {
				Debug.Log("ZDKRequestProvider.AddComment2 Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKRequestProvider.GetRequest("1", (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKRequestProvider.GetRequest - " + error.Description);
			}
			else {
				Debug.Log("ZDKRequestProvider.GetRequest Successful Callback - " + MakeResultString(result));
			}
		});

		// ZDKHelpCenterProvider Tests

		string[] labels = new string[0];
		ZendeskSDK.ZDKHelpCenterProvider.GetCategories ((result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKHelpCenterProvider.GetCategories - " + error.Description);
			}
			else {
				Debug.Log("ZDKHelpCenterProvider.GetCategories Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKHelpCenterProvider.GetSections("{id}", (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKHelpCenterProvider.GetSections - " + error.Description);
			}
			else {
				Debug.Log("ZDKHelpCenterProvider.GetSections Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKHelpCenterProvider.GetArticles("{id}", (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKHelpCenterProvider.SearchForArticles - " + error.Description);
			}
			else {
				Debug.Log("ZDKHelpCenterProvider.SearchForArticles Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKHelpCenterProvider.SearchArticles("Test query", (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKHelpCenterProvider.SearchForArticles - " + error.Description);
			}
			else {
				Debug.Log("ZDKHelpCenterProvider.SearchForArticles Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKHelpCenterProvider.SearchArticles("Test query", labels, (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKHelpCenterProvider.SearchForArticles - " + error.Description);
			}
			else {
				Debug.Log("ZDKHelpCenterProvider.SearchForArticles Successful Callback - " + MakeResultString(result));
			}
		});

		ZDKHelpCenterSearch helpCenterSearch = new ZDKHelpCenterSearch();
		helpCenterSearch.SectionId = 123456789;
		ZendeskSDK.ZDKHelpCenterProvider.SearchArticles(helpCenterSearch, (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKHelpCenterProvider.SearchArticles - " + error.Description);
			}
			else {
				Debug.Log("ZDKHelpCenterProvider.SearchArticles Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKHelpCenterProvider.GetAttachments("{id}", ZDKHelpCenterProvider.AttachmentType.Block, (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKHelpCenterProvider.GetAttachment - " + error.Description);
			}
			else {
				Debug.Log("ZDKHelpCenterProvider.GetAttachment Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKHelpCenterProvider.GetArticles(labels, (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKHelpCenterProvider.GetArticles2 - " + error.Description);
			}
			else {
				Debug.Log("ZDKHelpCenterProvider.GetArticles2 Successful Callback - " + MakeResultString(result));
			}
		});

		ZDKListArticleQuery getArticlesQuery = new ZDKListArticleQuery ();
		getArticlesQuery.LabelNames = labels;
		getArticlesQuery.SortOrder = SortOrder.Descending;
		getArticlesQuery.SortBy = SortBy.Title;
		ZendeskSDK.ZDKHelpCenterProvider.GetArticles(getArticlesQuery, (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKHelpCenterProvider.GetArticles3 - " + error.Description);
			}
			else {
				Debug.Log("ZDKHelpCenterProvider.GetArticles3 Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKHelpCenterProvider.GetFlatArticles((result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKHelpCenterProvider.GetFlatArticles - " + error.Description);
			}
			else {
				Debug.Log("ZDKHelpCenterProvider.GetFlatArticles Successful Callback - " + MakeResultString(result));
			}
		});
		ZendeskSDK.ZDKHelpCenterProvider.GetFlatArticles(getArticlesQuery, (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKHelpCenterProvider.GetFlatArticles2 - " + error.Description);
			}
			else {
				Debug.Log("ZDKHelpCenterProvider.GetFlatArticles2 Successful Callback - " + MakeResultString(result));
			}
		});

		ZDKHelpCenterDeflection deflectQuery = new ZDKHelpCenterDeflection ();
		deflectQuery.Query = "help";
		ZendeskSDK.ZDKHelpCenterProvider.GetSuggestedArticles(deflectQuery, (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKHelpCenterProvider.GetSuggestedArticles - " + error.Description);
			}
			else {
				Debug.Log("ZDKHelpCenterProvider.GetSuggestedArticles Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKHelpCenterProvider.GetArticle("{id}", (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKHelpCenterProvider.GetArticle - " + error.Description);
			}
			else {
				Debug.Log("ZDKHelpCenterProvider.GetArticle Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKHelpCenterProvider.GetSection("{id}", (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKHelpCenterProvider.GetSection - " + error.Description);
			}
			else {
				Debug.Log("ZDKHelpCenterProvider.GetSection Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKHelpCenterProvider.GetCategory("{id}", (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKHelpCenterProvider.GetCategory - " + error.Description);
			}
			else {
				Debug.Log("ZDKHelpCenterProvider.GetCategory Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKHelpCenterProvider.UpvoteArticle("{id}", (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKHelpCenterProvider.UpvoteArticle - " + error.Description);
			}
			else {
				Debug.Log("ZDKHelpCenterProvider.UpvoteArticle Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKHelpCenterProvider.DownvoteArticle("{id}", (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKHelpCenterProvider.DownvoteArticle - " + error.Description);
			}
			else {
				Debug.Log("ZDKHelpCenterProvider.DownvoteArticle Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKHelpCenterProvider.DeleteVote("{id}", (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKHelpCenterProvider.DeleteVote - " + error.Description);
			}
			else {
				Debug.Log("ZDKHelpCenterProvider.DeleteVote Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKHelpCenterProvider.SubmitRecordArticleView("{id}", null, (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKHelpCenterProvider.SubmitRecordArticleView - " + error.Description);
			}
			else {
				Debug.Log("ZDKHelpCenterProvider.SubmitRecordArticleView Successful Callback - " + MakeResultString(result));
			}
		});

		// ZDKAvatarProvider Tests

		ZendeskSDK.ZDKAvatarProvider.GetAvatar(
			"https://example.com/images/image.png",
			(result, error) => {
				if (error != null) {
					Debug.Log("ERROR: ZDKAvatarProvider.GetAvatar - " + error.Description);
				}
				else {
					Debug.Log("ZDKAvatarProvider.GetAvatar Successful Callback");
					avatarTexture.LoadImage(result);
				}
			});

		// ZDKSettingsProvider Tests

		ZendeskSDK.ZDKSettingsProvider.GetSdkSettings((result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKSettingsProvider.GetSdkSettings - " + error.Description);
			}
			else {
				Debug.Log("ZDKSettingsProvider.GetSdkSettings Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKSettingsProvider.GetSdkSettings("fr_FR", (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKSettingsProvider.GetSdkSettings2 - " + error.Description);
			}
			else {
				Debug.Log("ZDKSettingsProvider.GetSdkSettings2 Successful Callback - " + MakeResultString(result));
			}
		});

		// ZDKUploadProvider Tests

		ZendeskSDK.ZDKUploadProvider.UploadAttachment("TestAttachment", "TestAttachment.txt", "text", (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKUploadProvider.UploadAttachment - " + error.Description);
			}
			else {
				Debug.Log("ZDKUploadProvider.UploadAttachment Successful Callback: " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKUploadProvider.DeleteUpload("TestUploadToken", (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKUploadProvider.DeleteUpload - " + error.Description);
			}
			else {
				Debug.Log("ZDKUploadProvider.DeleteUpload Successful Callback: " + MakeResultString(result));
			}
		});

		runCreateRequestWithAttachmentUploadTest();
		runUploadImageAndAttachToCreateRequestTest ();

		// ZDKUserProvider Tests

		ZendeskSDK.ZDKUserProvider.GetUser((result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKUserProvider.GetUser - " + error.Description);
			}
			else {
				Debug.Log("ZDKUserProvider.GetUser Successful Callback: " + MakeResultString(result));
			}
		});

		Hashtable userFields = new Hashtable ();
		userFields ["key1"] = "value1";
		userFields ["key2"] = "value2";
		ZendeskSDK.ZDKUserProvider.SetUserFields(userFields, (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKUserProvider.SetUserFields - " + error.Description);
			}
			else {
				Debug.Log("ZDKUserProvider.SetUserFields Successful Callback: " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKUserProvider.GetUserFields((result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKUserProvider.GetUserFields - " + error.Description);
			}
			else {
				Debug.Log("ZDKUserProvider.GetUserFields Successful Callback: " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKUserProvider.AddTags(new string[] {"someTag", "anotherTag"}, (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKUserProvider.AddTags - " + error.Description);
			}
			else {
				Debug.Log("ZDKUserProvider.AddTags Successful Callback: " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKUserProvider.DeleteTags(new string[] {"someTag"}, (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKUserProvider.DeleteTags - " + error.Description);
			}
			else {
				Debug.Log("ZDKUserProvider.DeleteTags Successful Callback: " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKUserProvider.GetUserFields((result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKUserProvider.GetUserFields - " + error.Description);
			}
			else {
				Debug.Log("ZDKUserProvider.GetUserFields Successful Callback: " + MakeResultString(result));
			}
		});
	}

	void runUploadImageAndAttachToCreateRequestTest() {
		string url = "http://example.com/images/upload.png";
		WWW www = new WWW (url);
		StartCoroutine (WaitForRequest (www));
	}

	void runCreateRequestWithAttachmentUploadTest() {
		ZendeskSDK.ZDKUploadProvider.UploadAttachment("TestAttachment", "TestAttachment.txt", "text", (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKUploadProvider.UploadAttachment - " + error.Description);
			}
			else {
				Debug.Log("UploadAttachmentCallbackRan: " + MakeResultString(result));

				string[] tags = new string[0];
				string[] attachments = new string[1];
				#if UNITY_ANDROID
				attachments[0] = (string) result["token"];
				#else
				attachments[0] = (string) result["uploadToken"];
				#endif

				ZDKCreateRequest req = new ZDKCreateRequest("test@zendesk.com",
					"Test Attachments1",
					"Test Description1",
					tags,
					attachments);
				ZendeskSDK.ZDKRequestProvider.CreateRequest(req, (result2, error2) => {
					if (error2 != null) {
						Debug.Log("ERROR: ZDKRequestProvider.CreateRequest - " + error2.Description);
					}
					else {
						Debug.Log("CreateRequest Successful Callback - " + MakeResultString(result2));
					}
				});
			}
		});
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;

		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + www.text);
			avatarTexture = www.texture;
			byte[] imageData = www.texture.EncodeToPNG();
			string stringifiedImageData = Convert.ToBase64String(imageData);

			ZendeskSDK.ZDKUploadProvider.UploadAttachment(stringifiedImageData, "TestAttachment.png", "image/png", (result, error) => {
				if (error != null) {
					Debug.Log("ERROR: ZDKUploadProvider.UploadAttachment - " + error.Description);
				}
				else {
					Debug.Log("UploadAttachmentCallbackRan: " + MakeResultString(result));

					string[] tags = new string[0];
					string[] attachments = new string[1];

					#if UNITY_ANDROID
					attachments[0] = (string) result["token"];
					#else
					attachments[0] = (string) result["uploadToken"];
					#endif

					ZDKCreateRequest req = new ZDKCreateRequest("test@example.com",
						"Test Attachments2",
						"Test Description2",
						tags,
						attachments);
					ZendeskSDK.ZDKRequestProvider.CreateRequest(req, (result2, error2) => {
						if (error2 != null) {
							Debug.Log("ERROR: ZDKRequestProvider.CreateRequest - " + error2.Description);
						}
						else {
							Debug.Log("CreateRequest Successful Callback - " + MakeResultString(result2));
						}
					});
				}
			});
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}
	}

	string MakeResultString(object obj) {
		if (obj == null) {
			return "null";
		}

		if (obj.GetType().Name == "Hashtable") {
			Hashtable dict = (Hashtable) obj;
			string result = "{";
			foreach(string key in dict.Keys) {
				result += String.Format("{0}: {1}, ", key, MakeResultString(dict[key]));
			}
			result += "}";
			return result;
		}
		else if (obj.GetType().Name == "ArrayList") {
			ArrayList list = (ArrayList) obj;
			string result = "[";
			foreach(object obj2 in list) {
				result += String.Format("{0}, ", MakeResultString(obj2));
			}
			result += "]";
			return result;
		}
		else {
			return String.Format("{0}", obj);
		}
	}

	void OnDisable() {
	}
}
