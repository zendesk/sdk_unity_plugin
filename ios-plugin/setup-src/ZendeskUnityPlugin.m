#import "ZendeskUnityPlugin.h"
#import <ZendeskSDK/ZendeskSDK.h>
#import <ZendeskSDK/ZDKConfig+Private.h>
#import "ZendeskCore.h"

#ifdef PUSH_DEFINED_UA
#import "UAPush.h"
#import "UAirship.h"
#import "UAConfig.h"
#endif

NSString * const UnityPluginVersionNumber = @"1.4.1.1";
NSString * const UnityHeaderName = @"Unity";

@implementation ZendeskUnityPlugin

+ (void)load {
    NSLog(@"Zendesk UnityPlugin class loaded");
    [[NSUserDefaults standardUserDefaults] setObject:nil forKey:APPLE_PUSH_UUID];
    NSNotificationCenter *center = [NSNotificationCenter defaultCenter];
    [center addObserver:[ZendeskUnityPlugin class] selector:@selector(initializeSdks:) name:UIApplicationDidFinishLaunchingNotification object:nil];
}

+ (void)initializeSdks: (NSNotification *)notification {
    // if you need to do any configuration of Zendesk properties before dealing with a push that launched the app, do it here!
    // ZDKAnonymousIdentity *identity = [ZDKAnonymousIdentity new];
    // identity.email = @"urbanairshiptest@example.com";
    // [ZDKConfig instance].userIdentity = identity;

    //Initalize the Zendesk SDK
    [[ZDKConfig instance] initializeWithAppId:ZENDESK_APP_ID zendeskUrl:ZENDESK_URL andClientId:ZENDESK_OAUTH];

    NSLog(@"Zendesk UnityPlugin: initialized zendesk SDK for url %@", ZENDESK_URL);

    if (PUSH_ENABLED) {
        [[NSNotificationCenter defaultCenter] addObserver:[ZendeskUnityPlugin class] selector:@selector(didRegisterForRemoteNotifications:) name:@"kUnityDidRegisterForRemoteNotificationsWithDeviceToken" object:nil];
        [[NSNotificationCenter defaultCenter] addObserver:[ZendeskUnityPlugin class] selector:@selector(didFailToRegisterForRemoteNotifications) name:@"kUnityDidFailToRegisterForRemoteNotificationsWithError" object:nil];
        [[NSNotificationCenter defaultCenter] addObserver:[ZendeskUnityPlugin class] selector:@selector(didReceiveRemoteNotification:) name:@"kUnityDidReceiveRemoteNotification" object:nil];
    }

    if (PUSH_ENABLED_UA) {
#ifdef PUSH_DEFINED_UA
        NSLog(@"Zendesk UnityPlugin: Urban Airship taking off");
        UAConfig *config = [UAConfig defaultConfig];
        [UAirship takeOff:config];
        [UAirship push].userNotificationTypes = (UIUserNotificationTypeAlert |
                                                 UIUserNotificationTypeBadge |
                                                 UIUserNotificationTypeSound);

        // User notifications will not be enabled until userPushNotificationsEnabled is
        // set YES on UAPush. Once enabled, the setting will be persisted and the user
        // will be prompted to allow notifications. Normally, you should wait for a more appropriate
        // time to enable push to increase the likelihood that the user will accept
        // notifications.
        [UAirship push].userPushNotificationsEnabled = YES;
#endif
    } else if (PUSH_ENABLED_REGULAR) {
        // Register for remote notfications
        if ([UIApplication instancesRespondToSelector:@selector(registerForRemoteNotifications)]) {

            UIUserNotificationType types = UIUserNotificationTypeAlert | UIUserNotificationTypeBadge | UIUserNotificationTypeSound;

            UIUserNotificationSettings *settings = [UIUserNotificationSettings settingsForTypes:types categories:nil];
            [[UIApplication sharedApplication] registerUserNotificationSettings:settings];
            [[UIApplication sharedApplication] registerForRemoteNotifications];

        } else if ([UIApplication instancesRespondToSelector:@selector(registerForRemoteNotificationTypes:)]) {

            UIRemoteNotificationType types = UIRemoteNotificationTypeAlert | UIRemoteNotificationTypeBadge | UIRemoteNotificationTypeSound;

            [[UIApplication sharedApplication] registerForRemoteNotificationTypes:types];
        }
    }

    [[ZDKConfig instance] addUserAgentHeaderSuffixWithKey:UnityHeaderName value:UnityPluginVersionNumber];
}

-(id) init {
    self = [super init];
    if (self) {
        // nothing for now
    }
    return self;
}

+(void) didRegisterForRemoteNotifications:(NSNotification*)notification {
    NSData *deviceToken = (NSData*)notification.userInfo;

    if (PUSH_ENABLED_UA) {
#ifdef PUSH_DEFINED_UA
        NSString *deviceTokenString = [UAirship push].channelID;
        NSLog(@"Zendesk UnityPlugin: Device registered for remote notifications with UA identifier: %@", deviceTokenString);
        [[NSUserDefaults standardUserDefaults] setObject:deviceTokenString forKey:APPLE_PUSH_UUID];
        [[UAirship push] appRegisteredForRemoteNotificationsWithDeviceToken:deviceToken];
        _zendeskPush__registrationComplete(deviceTokenString);
#endif
    } else if (PUSH_ENABLED_REGULAR) {
        NSString *deviceTokenString = [[[[deviceToken description]
                                         stringByReplacingOccurrencesOfString: @"<" withString: @""]
                                        stringByReplacingOccurrencesOfString: @">" withString: @""]
                                       stringByReplacingOccurrencesOfString: @" " withString: @""];
        NSLog(@"Zendesk UnityPlugin: Device registered for remote notifications with Apple identifier: %@", deviceTokenString);
        [[NSUserDefaults standardUserDefaults] setObject:deviceTokenString forKey:APPLE_PUSH_UUID];
        _zendeskPush__registrationComplete(deviceTokenString);
    }
}

+(void) didFailToRegisterForRemoteNotifications {
    NSLog(@"Zendesk UnityPlugin: Device failed to register for remote notifications");
    if (PUSH_ENABLED) {
        _zendeskPush__registrationComplete(nil);
    }
}

+(void) didReceiveRemoteNotification:(NSNotification *)note {
    NSLog(@"Zendesk UnityPlugin: Device received remote notification");

    NSDictionary *userInfo = note.userInfo;
    NSMutableDictionary *pushUtilUserInfo = [[NSMutableDictionary alloc] initWithDictionary:userInfo];

    // if you want ticket deep-linking, make sure that the zendesk_sdk_request_id key is set properly
    // either send it from your push service with the correct key name or set it here
    // [pushUtilUserInfo setObject:userInfo[@"tid"] forKey:@"zendesk_sdk_request_id"];

    [ZDKPushUtil handlePush:pushUtilUserInfo
             forApplication:[UIApplication sharedApplication]
          presentationStyle:UIModalPresentationFormSheet
                layoutGuide:ZDKLayoutRespectTop
                  withAppId:ZENDESK_APP_ID
                 zendeskUrl:ZENDESK_URL
                   clientId:ZENDESK_OAUTH];
}


@end
