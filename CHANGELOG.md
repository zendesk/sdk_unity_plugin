# v.1.4.1.1
* Fix crashes for iOS7 and below when initializing the SDK.
* Fix issue of incorrectly mapping ticket email to ticket Subject, ticket Subject to ticket description, and ticket description to ticket email.

# v.1.4.0.1

* **Native SDKs updated: plugin now uses Zendesk Android SDK v1.4.2.2 and iOS SDK v.1.4.2.1. Please see the changelogs for these products to read about any under-the-hood improvements.**
    * https://developer.zendesk.com/embeddables/docs/android/release_notes
    * https://developer.zendesk.com/embeddables/docs/ios/release_notes
* ZDKConfig changes:
    * New method: `SetCustomFields` - This is used to add custom field data to tickets created.
    * New method: `SetContactConfiguration` - This is used to set custom contact configuration on all tickets created, whether via Help Center or not.
* ZDKRequests changes:
    * Method removed: `ShowRequestCreation(ZDKRequestCreationConfig config)` - Use `ZDKConfig.SetContactConfiguration` instead.
* ZDKHelpCenter changes:
    * Method removed: `SetRequestSubject(String reqSub)` - Use `ZDKConfig.SetContactConfiguration` instead.
*  Synchronise default request subject across both Android and iOS.
*  Fixes to push notifications on Android via UrbanAirship.
*  Android permissions added, for adding and viewing attachments in tickets:
    * READ_EXTERNAL_STORAGE
    * WRITE_EXTERNAL_STORAGE
*  Fixes to RateMyApp dialog on Android.
*  Fixes to push notifications on iOS.
*  `ZendeskTester` updated with more examples.

# v.1.3

* **Native SDKs updated: plugin now uses Zendesk Android SDK v1.4.1.1 and iOS SDK v1.4.1.4. Please see the changelogs for these products to read about any under-the-hood improvements.**
    * https://developer.zendesk.com/embeddables/docs/android/release_notes
    * https://developer.zendesk.com/embeddables/docs/ios/release_notes
* Integration instructions changed
    * SDK must now be built using configuration options
    * ZenExternal class no longer used
* New push notification features added including optional integration with Urban Airship
* Many new parameters for showing the Help Center
* Provider changes:
    * New provider: ZDKUserProvider
    * Many methods added to ZDKHelpCenterProvider and ZDKRequestProvider
    * ZDKRequestProvider.CreateRequest now takes a ZDKCreateRequest object instead of individual parameters
    * ZDKHelpCenterProvider.GetAttachment renamed to GetAttachments
    * ZDKSettingsProvider.GetSdkSettingsiOS removed, now only GetSdkSettings is needed
* ZDKConfig changes:
    * use ZDKConfig instead of ZDKConfig.Instance for configuration methods
    * Logging methods moved to ZDKLogger
    * ZDKConfig.SetDebugLoggingiOS removed, replaced with ZDKLogger.Enable and ZDKLogger.SetLogLevelIOS
    * new methods added: SetCoppaEnabled, SetUserLocale

# v.1.2

* Added ViewArticle method to the Help Center Provider.
* Added Alpha color and background color for Rate My App view.
* Update README to state that Unity 5.0 is required.

# v.1.1

* Android - Fix upload provider to work with image files
* Fix conflicts with Chartboost Unity Plugin

# v.1.0

* Initial release
