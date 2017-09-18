using System;
using UnityEngine;
using System.Collections;
using ZendeskSDK;

// <summary>
// A simple example demonstrating Zendesk configuration and displaying the
// Help Center, Requests and Rate My App views.
// </summary>
public class ZendeskExample: MonoBehaviour
{

	// <summary>
    // This shows you how to initialize the SDK and set an identity. These actions are required
    // before you can interact with the SDK.
    // </summary>
	void Awake() {

        // Initializes the SDK using your subdomain, application ID, and oauth client ID. Related info:
        // Android: https://developer.zendesk.com/embeddables/docs/android/initialize_sdk
        // iOS: https://developer.zendesk.com/embeddables/docs/ios/initialize_sdk
		ZendeskSDK.ZDKConfig.Initialize (gameObject, "https://{subdomain}.zendesk.com", "{applicationId}", "{oauthClientId}");

		// Set an anonymous identity. Releated info:
		// Android: https://developer.zendesk.com/embeddables/docs/android/set_an_identity
		// iOS: https://developer.zendesk.com/embeddables/docs/ios/set_an_identity
		ZendeskSDK.ZDKConfig.AuthenticateAnonymousIdentity();

		// If you use JWT identities, then comment out the AuthenticateAnonymousIdentity line above, and uncomment this line.
		//ZendeskSDK.ZDKConfig.AuthenticateJwtUserIdentity ("MyTestID");
	}

	// <summary>
    // must include this method for any zendesk callbacks to work
    // </summary>
	void OnZendeskCallback(string results) {
		ZDKConfig.CallbackResponse (results);
	}

	void OnEnable() {
	
	}

	void Update() {

	}

    // <summary>
    // This shows you how to add a few buttons which can be used to launch different parts of the
    // SDK. You must have previously initialized the SDK and set an identity as shown in Awake()
    // </summary>
	void OnGUI() {
		GUI.matrix = Matrix4x4.Scale (new Vector3 (5, 5, 5));

		if (GUILayout.Button ("Help Center")) {
			ZendeskSDK.ZDKHelpCenter.ShowHelpCenter ();
		}

		if (GUILayout.Button ("Request Creation")) {
			ZendeskSDK.ZDKRequests.ShowRequestCreation ();
		}
	}

	void OnDisable() {
		//
	}
}
