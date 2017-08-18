using System;
using UnityEngine;
using System.Collections;
using ZendeskSDK;

/// <summary>
/// Zendesk tester.
/// This class provides a springboard interface to allow you to test various parts of the SDK.
///
/// Note that you must:
///
/// 1) replace the values near if (GUILayout.Button ("Initialize SDK", buttonWidth)) {... with
/// your instance's values
///
/// </summary>
public class ZendeskTester: MonoBehaviour
{
	private Texture2D avatarTexture;
	private bool pushEnabled = false;
	private int textureSize = 60;
	private Vector2 scrollPosition = Vector2.zero;

	/// <summary>
	/// Awake this instance. This also enables the Zendesk logger for debugging.
	/// </summary>
	void Awake() {
		avatarTexture = new Texture2D(textureSize, textureSize);
		ZendeskSDK.ZDKLogger.Enable (true);
	}

	/** must include this method for any zendesk callbacks to work */
	void OnZendeskCallback(string results) {
		Debug.Log("OnZendeskCallback - " + MakeResultString(results));
		ZendeskSDK.ZDKConfig.CallbackResponse (results);
	}

	void OnEnable() {

	}

	void Update() {

	}

	/// <summary>
	/// Raises the GUI event.
	///
	/// Adds a number of buttons used to test the SDK.
	///
	/// </summary>
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
			ZendeskSDK.ZDKConfig.Initialize (gameObject, "https://{subdomain}.zendesk.com", "{applicationId}", "{oauthClientId}");
			ZendeskSDK.ZDKConfig.AuthenticateAnonymousIdentity();

			// Uncomment this line if you want to override the langauge that Help Center content is requested in
			// ZendeskSDK.ZDKConfig.SetUserLocale ("en-US");

			// If you use JWT identities, then comment out the AuthenticateAnonymousIdentity line above, and uncomment this line.
			//ZendeskSDK.ZDKConfig.AuthenticateJwtUserIdentity ("MyTestID");
		}

		if (GUILayout.Button ("Set Custom Fields", buttonWidth)) {
			Hashtable customFields = new Hashtable ();
			string customFieldId = "customFieldId";
			customFields[customFieldId] = "customFieldValue";

			ZendeskSDK.ZDKConfig.SetCustomFields (customFields);
		}

		if (GUILayout.Button (pushEnabled ? "Disable Push" : "Enable Push", buttonWidth)) {
			if (!pushEnabled) {
				ZendeskSDK.ZDKPush.EnableWithIdentifier("{deviceOrChannelId}", (result, error) => {
					if (error != null) {
						Debug.Log("ERROR: ZDKPush.Enable - " + error.Description);
					}
					else {
						pushEnabled = true;
						Debug.Log("ZDKPush.Enable Successful Callback - " + MakeResultString(result));
					}
				});
			} else {
				ZendeskSDK.ZDKPush.Disable("{deviceOrChannelId}", (result, error) => {
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
			// Shows all Help Center content
			ZendeskSDK.ZDKHelpCenter.ShowHelpCenter ();
		}


		if (GUILayout.Button ("Show Help Center With Options", buttonWidth)) {

			 // Shows Help Center content with additional options
			 ZendeskSDK.ZDKHelpCenter.HelpCenterOptions options = new ZendeskSDK.ZDKHelpCenter.HelpCenterOptions ();

			 // Optional: Specify any category IDs that you wish to restrict your content to
			 // options.IncludeCategoryIds = new [] { 203260428L, 203260368L };

			 // Optional: Specify any section IDs that you wish to restrict your content to
			 // options.IncludeSectionIds = new [] { 205095568L, 205095528L };

			 // Optional: Specify any label names that you wish to use to filter the content.
			 // options.IncludeLabelNames = new [] { "vip", "another_label" };

			 // Optional: Specify contact configuration
			 // ZDKHelpCenter.ContactConfiguration config = new ZDKHelpCenter.ContactConfiguration ();
			 // config.RequestSubject = "My printer is on fire!";
			 // config.Tags = new[] {"printer", "technical"};
			 // config.AdditionalInfo = " - Sent from Unity!";
			 // options.ContactConfiguration = config;

			 // Optional: Show / hide the contact us button
			 // options.ContactUsButtonVisibility = ZendeskSDK.ZDKHelpCenter.ContactUsButtonVisibility.ArticleListOnly;
			 // options.ArticleVoting = false;

				ZendeskSDK.ZDKHelpCenter.ShowHelpCenter (options);
		}

		if (GUILayout.Button ("Show Request Creation", buttonWidth)) {

			ZDKRequestCreationConfig config = new ZDKRequestCreationConfig ();
			config.RequestSubject = "My printer is still on fire";
			config.Tags = new [] { "printer" };
			config.AdditionalRequestInfo = " - Sent from Unity!";

			ZendeskSDK.ZDKRequests.ShowRequestCreationWithConfig (config);
		}

		if (GUILayout.Button ("Show Request List", buttonWidth)) {
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

		if (GUILayout.Button ("Get Ticket Form", buttonWidth)) {
			int [] x = new int[1];
			// x[0] = <your ticket form id>;

			ZendeskSDK.ZDKRequestProvider.GetTicketForms(x, (result, error) => {
				if (error != null) {
					Debug.Log("ERROR: ZDKRequestProvider.GetTicketForms - " + error.Description);
				}
				else {
					Debug.Log("ZDKRequestProvider.GetTicketForms Successful Callback - " + MakeResultString(result));
				}
			});
		}

		if (GUILayout.Button ("Run Provider Tests", buttonWidth)) {
			RunProviderTests ();
		}

		if (GUILayout.Button ("Run Appearance Tests", buttonWidth)) {
			RunAppearanceTests ();
		}

		GUI.EndScrollView();
		GUILayout.EndArea();
		GUILayout.EndVertical ();
	}

	void RunAppearanceTests() {

		#if UNITY_IPHONE

		runUploadImageAndAttachToCreateRequestTest ();

		IOSAppearance appearance = new IOSAppearance ();
		appearance.StartWithBaseTheme ();

		appearance.SetPrimaryTextColor(new ZenColor (1.0f, 1.0f, 0f));
		appearance.SetSecondaryTextColor (new ZenColor (1.0f, 0f, 0f));
		appearance.SetPrimaryBackgroundColor(new ZenColor(0f, 0f, 1.0f));
		appearance.SetSecondaryBackgroundColor (new ZenColor (0f, 1f, 0f));
		appearance.SetMetaTextColor (new ZenColor (0.5f, 0f, 0f));
		appearance.SetEmptyBackgroundColor (new ZenColor (0.5f, 0.5f, 0f));
		appearance.SetSeparatorColor (new ZenColor (0.5f, 0f, 0.5f));
		appearance.SetInputFieldColor (new ZenColor(0.5f, 0.7f, 0.2f));
		appearance.SetInputFieldBackgroundColor(new ZenColor(0.9f, 0.1f, 0.9f));

		appearance.ApplyTheme ();

		#else

		Debug.Log("This is only for iPhone");

		#endif
	}

	void RunProviderTests() {

		// ZDKRequestProvider Tests

		string[] tags = new string[0];
		string[] attachments = new string[0];
		ZDKCreateRequest req = new ZDKCreateRequest("test@example.com", "Test Subject", "Test Description", tags);
		ZendeskSDK.ZDKRequestProvider.CreateRequest(req, (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKRequestProvider.CreateRequest - " + error.Description);
			}
			else {
				Debug.Log("ZDKRequestProvider.CreateRequest Successful Callback - " + MakeResultString(result));
			}
		});

		req = new ZDKCreateRequest("test@example.com", "Test Subject", "Test Description", tags, attachments);
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

		ZendeskSDK.ZDKRequestProvider.GetComments("{requestId}", (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKRequestProvider.GetComments - " + error.Description);
			}
			else {
				Debug.Log("ZDKRequestProvider.GetComments Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKRequestProvider.AddComment("Test Comment", "{requestId}", (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKRequestProvider.AddComment - " + error.Description);
			}
			else {
				Debug.Log("ZDKRequestProvider.AddComment Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKRequestProvider.AddComment("Test Comment", "{requestId}", attachments, (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKRequestProvider.AddComment2 - " + error.Description);
			}
			else {
				Debug.Log("ZDKRequestProvider.AddComment2 Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKRequestProvider.GetRequest("{requestId}", (result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKRequestProvider.GetRequest - " + error.Description);
			}
			else {
				Debug.Log("ZDKRequestProvider.GetRequest Successful Callback - " + MakeResultString(result));
			}
		});

		ZendeskSDK.ZDKRequestProvider.GetUpdatesForDevice((result, error) => {
			if (error != null) {
				Debug.Log("ERROR: ZDKRequestProvider.GetUpdates - " + error.Description);
			}
			else {
				Debug.Log("ZDKRequestProvider.GetUpdates Successful Callback - " + MakeResultString(result));
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
		helpCenterSearch.SectionIds = new [] { "123L" };
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
				Debug.Log("ERROR: ZDKHelpCenterProvider.GetArticles - " + error.Description);
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
				Debug.Log("ERROR: ZDKHelpCenterProvider.GetArticles - " + error.Description);
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
				Debug.Log("ERROR: ZDKHelpCenterProvider.GetFlatArticles - " + error.Description);
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

		ZendeskSDK.ZDKHelpCenterProvider.SubmitRecordArticleView("{id}", "https://example.com", "Example", "fr-FR", (result, error) => {
		 	if (error != null) {
				Debug.Log("ERROR: ZDKHelpCenterProvider.SubmitRecordArticleView - " + error.Description); // TODO
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
		string url = "https://officesnapshots.com/wp-content/uploads/2014/09/ZendeskSF_Photo%C2%A9BruceDamonte_18.jpg";
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

				ZDKCreateRequest req = new ZDKCreateRequest("test@example.com",
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
				} else {
					Debug.Log("UploadAttachmentCallbackRan: " + MakeResultString(result));

					string[] tags = new string[0];
					string[] attachments = new string[1];

					#if UNITY_ANDROID
					attachments[0] = (string) result["token"];
					Debug.Log(attachments[0]);
					#else
					attachments[0] = (string) result["uploadToken"];
					#endif
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
