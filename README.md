Zendesk Unity3D Plugin
======================

This is a Unity plugin that wraps the iOS and Android Zendesk Support SDKs. Review the [CHANGELOG](./CHANGELOG.md) for details about upgrading from previous versions.

This is an open source project, and is not directly supported by Zendesk. Check out the [CONTRIBUTING](./CONTRIBUTING.md) page to find out how you can make changes or report issues.

## Requirements
[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2Fzendesk%2Fsdk_unity_plugin.svg?type=shield)](https://app.fossa.io/projects/git%2Bgithub.com%2Fzendesk%2Fsdk_unity_plugin?ref=badge_shield)


- Unity 5.x

### OS requirements

The Android build script, `./android-plugin/build.gradle`, requires the `zip` command which is commonly
distributed on Linux and Unix based systems, including Mac OS.

`zip.exe` is not distributed on Windows machines and must be [installed](http://gnuwin32.sourceforge.net/packages/zip.htm) and added to the PATH.

Alternatively, the 7zip command line application, `7za.exe`, can be used to perform the same task. More details are in the `stripCssFromAndroidSdk` task in `./android-plugin/build.gradle`.

### iOS requirements

- Xcode 9.0+
- iOS 9 to 11
- Android requirements also have to be met, even if only building the iOS plugin.

### Android requirements

Most requirements will be downloaded automatically. You will have to ensure that some components are up to date in the Android SDK Manager.

- Android API 15 (4.0.3) and above.
- Android SDK Build-tools 24.0.0
- Latest version of Android Support Repository

## Basics

1. Import the Zendesk Unity SDK into your Unity project

    * Import the `sdk_unity_plugin` project into Android Studio.
    * Build the plugin with `./gradlew build`
    * Copy the output of build/unity-plugin/ into your Unity app.

    You may see some errors like this: `Could not create texture from Assets/Plugins/iOS/ZendeskSDK.bundle/{name}.png: File could not be read`.
    These are safe to ignore and will disappear when you build the project for iOS. You also may need to ensure that the `MessageUI`, `Security` and `MobileCoreServices` frameworks have been added to the project that Unity exports to Xcode. These frameworks can be added by selecting the correct target in Xcode and then selecting the aforementioned frameworks in the `Linked Frameworks and Libraries` under the `General` tab.

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

4. Android manifest

    * [MUST DO] One of the `<provider>` elements must be uncommented for the plugin to build correctly. See the documentation in `/Assets/Plugins/Android/AndroidManifest.xml`
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

### Request Updates API

In version 1.10.0.1 of the Support SDK, the [Request Updates API](https://developer.zendesk.com/embeddables/docs/android/show_open_requests#check-for-updates-on-your-requests) was added to allow querying for updates on requests without having to start the UI. Please note that the API is disabled when push notifications are enabled. Push integration should remove the need to query for request updates.

See `ZDKRequestProvider.cs` for the Request Updates methods: `GetUpdatesForDevice` and `MarkRequestAsRead`.

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

`/sdk_unity_plugin/android-plugin/src/main/assets/help_center_article_style.css`

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
3. Unity 3D fails to detect the Android SDK correctly when Android SDK Tools 25.3.1 is installed. Tracked on [code.google.com](https://code.google.com/p/android/issues/detail?id=235455).
4. The SDK can crash when deployed onto Android 4.0.3 devices. This occurs when your application is built with the `Internal (default)` build system. Building with gradle will fix this issue.


## License
[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2Fzendesk%2Fsdk_unity_plugin.svg?type=large)](https://app.fossa.io/projects/git%2Bgithub.com%2Fzendesk%2Fsdk_unity_plugin?ref=badge_large)