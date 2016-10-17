Zendesk Unity3D Plugin
======================

This is a Unity plugin that wraps the iOS and Android Zendesk SDKs.

## Requirements

- OSX or Linux operating system (The plugin is not supported on Windows)
- Unity 5.0+ for iOS, Unity 5.5+ for Android
- Xcode 7.0+ (if building for iOS)
- Android SDK with the latest support repository and libraries installed. (Required to build the plugin, even if you are just using iOS)
- Note that you must have the "Android Support Library" installed. You can download this using the Android SDK Manager. 

## Basics

1. Import the Zendesk Unity SDK into your Unity project

    * Import the `sdk_unity_plugin` project into Android Studio.
    * Build the plugin with `./gradlew build`
    * Copy the output of build/unity-plugin/ into your Unity app.

2. Viewing the samples

    * Copy the scripts from `sample` and attach either one to a `GameObject` in your scene.
    * Select a GameObject (MainCamera for example) from the Hierarchy window
    * On the Inspector window, push the 'Add Component' button
    * Select one of the sample scripts you copied.

3. Creating your own class that uses Zendesk

    * To use the Zendesk SDK in Unity, you must create a class that extends `MonoBehaviour` and attach it to a `GameObject` in your scene.
    * Include the following two methods in your class:

    ```c#
    // initialize Zendesk and set an identity. See ZendeskExample.cs for more information
    void Awake() {
        ZendeskSDK.ZDKConfig.Initialize (gameObject, "https://{subdomain}.zendesk.com", "{applicationId}", "{oauthClientId}"); // DontDestroyOnLoad automatically called on your supplied gameObject
        ZendeskSDK.ZDKConfig.AuthenticateAnonymousIdentity();
    }
    
    // must include this method for any zendesk callbacks to work
    void OnZendeskCallback(string results) {
        ZendeskSDK.ZDKConfig.CallbackResponse (results);
    }
    ```
    
4. Android conflicts

    * You may already have a file at `/Assets/Plugins/Android/AndroidManifest.xml`. If so, you will have to manually merge the items of that manifest with the one we supply in our plugin.  Specifically, your `<application>` tag must have the `android:theme="@style/UnityTheme"` attribute, and your `UnityPlayerActivity` (or derived class) `<activity>` entry must have `<meta-data android:name="unityplayer.ForwardNativeEventsToDalvik" android:value="true" />` as a child tag.

## App Configuration and Zendesk App Interfaces

Example C#:

    c#
    ZendeskSDK.ZDKHelpCenter.ShowHelpCenter();
    ZendeskSDK.ZDKHelpCenter.ShowHelpCenter(options);
    
    ZendeskSDK.ZDKRequests.ShowRequestCreation
    ZendeskSDK.ZDKRequests.ShowRequestCreationWithConfig(config)
    
    ZendeskSDK.ZDKRMA.Show();
    ZendeskSDK.ZDKRMA.ShowAlways();

App configuration and the Zendesk App Help Center, Requests, and Rate My App interfaces 
can be found in the /Assets/Zendesk folder and are named:

* ZDKConfig.cs
* ZDKHelpCenter.cs
* ZDKRequests.cs
* ZDKRMA.cs
* ZDKPush.cs
* ZDKLogger.cs

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
        ZendeskSDK.ZDKPush.EnableWithIdentifier("{device-or-channel-identifier}", (result, error) => {
            if (error != null) {
                Debug.Log("ERROR: ZDKPush.Enable - " + error.Description);
            }
            else {
                pushEnabled = true;
                Debug.Log("ZDKPush.Enable Successful Callback - " + MakeResultString(result));
            }
        });
    } else {
        ZendeskSDK.ZDKPush.Disable("device-or-channel-identifier", (result, error) => {
            if (error != null) {
                Debug.Log("ERROR: ZDKPush.Disable - " + error.Description);
            }
            else {
                pushEnabled = false;
                Debug.Log("ZDKPush.Disable Successful Callback - " + MakeResultString(result));
            }
        });
    }

There is an an example of this in the `ZendeskTester.cs` script file. 

Notifications are a complex, OS-dependent feature. We provide the interfaces for enabling and disabling push. To handle incoming push messages you will need to configure the Urban Airship Unity SDK or the GCM / APNS SDKs.

## Interface customization

### iOS

Zendesk application customization can be specified with IOSAppearance. ZenColor supports rgb and rbga values.

Example C#:

```c#
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
```


### Android

Zendesk application customization must be specified before the Android Unity plugin is created with `./gradlew build`.

Include your style changes in: 

`/sdk_unity_plugin/android-plugin/src/main/res/values/styles.xml`

The default `styles.xml` defines a theme called `UnityTheme`. This is then referenced by the `AndroidManifest.xml` file in your Unity project at `/Assets/Plugins/Android`.

To find defined styles and examples, see:

https://developer.zendesk.com/embeddables/docs/android/customize_the_look

## Help Center Appearance Customization 

Custom Help Center articles are styled with CSS that can be specified in the following files.

### iOS

`/Assets/Plugins/iOS/ZendeskSDK.bundle/help_center_article_style.css`

### Android

On Android, this file must be edited before you create the plugin with `./gradlew build`

`/sdk_unity_plugin/android-plugin/src/main/res/assets/help_center_article_style.css`

## String and Localization Customization

Custom strings and localizations can be specified in the following files. To change the default strings in your application, add replacements to the string you wish to modify. Make sure to include placeholders in the replacement of any strings that contain them.

### iOS

Strings are specified in plist files, one for each Locale. Each locale is a separate `[Locale]` folder.

`/Assets/Plugins/iOS/ZendeskSDKStrings.bundle/[Locale].lproj/Localizable.strings`

To find list of strings, see:

https://developer.zendesk.com/embeddables/docs/ios/localize_text

### Android

Strings are specified in xml files, one for each Locale. Each locale is a separate `[Locale]` folder. On Android, these files must be edited before you create the plugin with `./gradlew build`

`/Assets/Plugins/Android/zendesk-lib/res/values-[Locale]/strings.xml`

To find list of strings, see:

https://developer.zendesk.com/embeddables/docs/android/localize_text

## Known issues
 
1. When creating a ticket using Rate My App on Android, the description of the issue is used for the subject line of the ticket. 
2. On request creation on Android, rotating the screen during sending appears to cancel the ticket. The progress dialog will disappear and the ticket form will regain focus.
However, the ticket will still be successfully created, and will be present in the user's ticket list, though it will be missing any attachments added before the rotation.
