Zendesk Unity3D Plugin
==========================

This is a Unity plugin that wraps the iOS and Android Zendesk SDKs. It also allows optional inclusion of the Urban Airship Unity SDK to assist with push notification features.

## Third Party Packages

- mod_pbxproj.py - Apache License, Copyright 2012 Calvin Rien
- Urban Airship Unity SDK - Apache License, Copyright 2015 Urban Airship and Contributors

## Requirements

- OSX or Linux operating system (The plugin is not supported on Windows)
- Unity 5.0+
- Xcode 7.0+ (if building for iOS)
- Android SDK with the latest support repo and libraries installed. (Required to build the plugin, even if you are just using iOS)
- Note that you must have the "Android Support Library" installed. You can download this using the Android SDK Manager. 
If you don't see this as an available option, make sure you are using the standalone SDK Manager (not the Android Studio one),
and tick the "Obsolete" checkbox.

## Basics

1. Import the Zendesk Unity SDK into your Unity project

    * Update gradle.properties with your app's configuration and build preferences.
    * Build the plugin with `./gradlew build`
    * Copy the output of build/unity-plugin/ into your Unity app.

2. Viewing the samples

    * Copy the scripts from `sample` and attach either one to a `GameObject` in your scene.
    * Select a GameObject (MainCamera for example) from the Hierarchy window
    * On the Inspector window, push the 'Add Component' button
    * Select one of the sample scripts you copied.

3. Creating your own class that uses Zendesk

    * To use the Zendesk SDK in Unity, you must create a class that extends `MonoBehaviour` and attach it to a `GameObject` in your scene.
    * Include the following three methods in your class:

        c#
        /** initialize zendesk in the Awake() method of the GameObject a script of yours is attached to */
        void Awake() {
            ZendeskSDK.ZDKConfig.Initialize (gameObject); // DontDestroyOnLoad automatically called on your supplied gameObject
        }

        /** must include this method for android to behave properly */
        void OnApplicationPause(bool pauseStatus) {
            ZendeskSDK.ZDKConfig.OnApplicationPause (pauseStatus);
        }
    
        /** must include this method for any zendesk callbacks to work */
        void OnZendeskCallback(string results) {
            ZendeskSDK.ZDKConfig.CallbackResponse (results);
        }

4. Android conflicts

    * You may already have a file at `/Assets/Plugins/Android/AndroidManifest.xml`. If so, you will have to manually merge the items of that manifest with the one we supply in our plugin.  Specifically, your `<application>` tag must have the `android:theme="@style/ZendeskSdkTheme"` attribute, and your `UnityPlayerActivity` (or derived class) `<activity>` entry must have `<meta-data android:name="unityplayer.ForwardNativeEventsToDalvik" android:value="true" />` as a child tag.

## App Configuration and Zendesk App Interfaces

Example C#:

    c#
    ZendeskSDK.ZDKHelpCenter.ShowHelpCenter();
    ZendeskSDK.ZDKRequests.ShowRequestList();
    ZendeskSDK.ZDKRMA.ShowAlways();

App configuration and the Zendesk App Help Center, Requests, and Rate My App interfaces 
can be found in the /Assets/Zendesk folder and are named:

* ZDKConfig.cs
* ZDKHelpCenter.cs
* ZDKRequests.cs
* ZDKRMA.cs
* ZDKPush.cs
* ZDKLogger.cs
* ZDKDeviceInfo.cs

## Zendesk Data Providers

The Zendesk SDK provider interfaces can be found in the /Assets/Zendesk folder and are named:

* ZDKAvatarProvider.cs
* ZDKHelpCenterProvider.cs
* ZDKRequestProvider.cs
* ZDKSettingsProvider.cs
* ZDKUploadProvider.cs
* ZDKUserProvider.cs

Example C#:

    c#
    ZendeskSDK.ZDKRequestProvider.GetAllRequests((results, error) => {
        if (error != null) {
            Debug.Log("ERROR: ZDKRequestProvider.GetAllRequests - " + error.Description);
        } 
        else {
            Debug.Log("GetAllRequests returned results");
            foreach(Hashtable request in results) {
                Debug.Log(String.Format("RequestId: {0}", request["requestId"]));
            }
        }
    });
    

## Push notifications

Enabling and disabling push notifications for the current user is pretty straightforward.

    c#
    if (!pushEnabled) {
        ZendeskSDK.ZDKPush.Enable((result, error) => {
            if (error != null) {
                Debug.Log("ERROR: ZDKPush.Enable - " + error.Description);
            } else {
                pushEnabled = true;
                Debug.Log("ZDKPush.Enable Successful Callback - " + MakeResultString(result));
            }
        });
    } else {
        ZendeskSDK.ZDKPush.Disable((result, error) => {
            if (error != null) {
                Debug.Log("ERROR: ZDKPush.Disable - " + error.Description);
            } else {
                pushEnabled = false;
                Debug.Log("ZDKPush.Disable Successful Callback - " + MakeResultString(result));
            }
        });
    }

There is an an example of this in the `ZendeskTester.cs` script file. 

Notifications are a complex, OS-dependent feature. We provide straightforward implementations in the included iOS and Android plugins. 
Feel free to modify these implementations by editing the Java and Objective-C code in the android-plugin, ios-plugin, and ua-unity-android-plugin subdirectories.

## Interface customization

### iOS

Zendesk application customization can be specified through appearance customizations
specified on Views in the Zendesk Plugin SDK, similar to the iOS SDK.

Example C#:

```c#
ZendeskSDK.ZDKSupportView.SetBackgroundColor(new ZenColor(0.9f, 1.0f));
ZendeskSDK.ZDKRMAFeedbackView.SetHeaderFont("system", 14);
ZendeskSDK.ZDKCreateRequestView.SetAttachmentButtonCornerRadius(0);
ZendeskSDK.ZDKCreateRequestView.SetAttachmentButtonBorderWidth(0);
```

The appearance views can be found in the /Assets/Zendesk folder and are named:

* ZDKAttachmentView.cs
* ZDKCommentInputView.cs
* ZDKCreateRequestView.cs
* ZDKRequestCommentAttachmentLoadingTableCell.cs
* ZDKRequestCommentAttachmentTableCell.cs
* ZDKRequestCommentTableCell.cs
* ZDKRequestListTable.cs
* ZDKRequestListTableCell.cs
* ZDKRMADialogView.cs
* ZDKRMAFeedbackView.cs
* ZDKSupportArticleTableViewCell.cs
* ZDKSupportAttachmentCell.cs
* ZDKSupportTableViewCell.cs
* ZDKSupportView.sc
* ZDKUIImageScrollView.cs
* ZDKUILoadingView.cs

### Android

Zendesk application customization can be specified through styles in the styles.xml file, similar to the Android
SDK. You can use a set of defined styles to achieve the desired effect. Include your style changes in: 

`/Assets/Plugins/Android/appcompat/res/values/styles.xml`

To add the style to the activity of your choice, make the additions in:

`/Assets/Plugins/Android/AndroidManifest.xml`

To find defined styles and examples, see:

https://developer.zendesk.com/embeddables/docs/android/customization

## Help Center Appearance Customization 

Custom Help Center articles are styled with CSS that can be specified
in the following files once the plugin has been installed.

### iOS

`/Assets/Plugins/iOS/ZendeskSDK.bundle/help_center_article_style.css`

### Android

`/Assets/Plugins/Android/assets/help_center_article_style.css`

## String and Localization Customization

Custom strings and localizations can be specified in the following files
once the plugin has been installed.

### iOS

Strings are specified in plist files, one for each Locale. Each locale is a separate [Locale] folder.

`/Assets/Plugins/iOS/ZendeskSDKStrings.bundle/[Locale].lproj/Localizable.strings`

### Android

Strings are specified in xml files, one for each Locale. Each locale is a separate [Locale] folder. To change the 
default strings in your application, add replacements to the string you wish to modify. Make sure to include
placeholders in the replacement of any strings that contain them. Add the string replacements in:

`/Assets/Plugins/Android/zendesk-lib/res/values-[Locale]/strings.xml`


To find list of strings, see:

https://developer.zendesk.com/embeddables/docs/android/customization

## Known issues

1. External ID on anonymous authentication for iOS will not be picked up by the server. Anonymous authentication will still work, but the external ID will not be recognised.
2. Push notification deep-linking does not work. It will open the app, but won't deep-link to the ticket. 
3. When creating a ticket using Rate My App on Android, the description of the issue is used for the subject line of the ticket. 
4. On request creation on Android, rotating the screen during sending appears to cancel the ticket. The progress dialog will disappear and the ticket form will regain focus.
However, the ticket will still be successfully created, and will be present in the user's ticket list, though it will be missing any attachments added before the rotation.
5. Urban Airship, as per their documentation for both [Android](http://docs.urbanairship.com/platform/android.html#retrieving-your-channel-id) 
and [iOS](http://docs.urbanairship.com/platform/ios.html#retrieving-your-channel-id), can return a `null` channel ID until the device has been properly set up with the Urban Airship services. 
Restarting the app is usually enough to fix this. Unfortunately, it's quite inconsistent. 
In some cases (particularly when testing multiple builds and/or configurations on the same device), a more thorough clean is required. Reinstalling the plugin from scratch is usually enough to do this.   
6. The UI provided in these versions of the native SDKs and plugin do not work on iOS 10.