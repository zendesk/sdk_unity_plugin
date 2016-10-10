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

	/** initialize zendesk in the Awake() method of the GameObject a script of yours is attached to */
	void Awake() {

		ZendeskSDK.ZDKConfig.Initialize (gameObject, "https://{subdomain}.zendesk.com", "{applicationId}", "{oauthClientId}");
		ZendeskSDK.ZDKConfig.AuthenticateAnonymousIdentity();

		// If you use JWT identities, then comment out the AuthenticateAnonymousIdentity line above, and uncomment this line.
		//ZendeskSDK.ZDKConfig.AuthenticateJwtUserIdentity ("MyTestID");
	}

	/** must include this method for any zendesk callbacks to work */
	void OnZendeskCallback(string results) {
		ZDKConfig.CallbackResponse (results);
	}

	void OnEnable() {
	
	}

	void Update() {

	}

	void OnGUI() {
		GUI.matrix = Matrix4x4.Scale (new Vector3 (5, 5, 5));

		if (GUILayout.Button ("Help Center")) {
			ZendeskSDK.ZDKHelpCenter.ShowHelpCenter ();
		}

		if (GUILayout.Button ("Request Creation")) {
			ZendeskSDK.ZDKRequests.ShowRequestCreation ();
		}

		if (GUILayout.Button ("Rate My App")) {
			ZendeskSDK.ZDKRMA.ShowAlways ();
		}
	}

	void OnDisable() {
		//
	}
}
