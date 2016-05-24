#define PUSH_DEFINED_NOTHING // replaced with PUSH_DEFINED_UA if UA is to be imported
#define PUSH_ENABLED_REGULAR (USE_PUSH_REGULAR)
#define PUSH_ENABLED_UA (USE_PUSH_URBANAIRSHIP)
#define PUSH_ENABLED (PUSH_ENABLED_REGULAR || PUSH_ENABLED_UA)

#import <Foundation/Foundation.h>

#define APPLE_PUSH_UUID @"ZDKUnityPushUUIDKey"

FOUNDATION_EXPORT NSString * const UnityPluginVersionNumber;

FOUNDATION_EXPORT NSString * const UnityHeaderName;

#ifdef PUSH_DEFINED_UA
#import "UAPush.h"
@interface ZendeskUnityPlugin : NSObject <UAPushNotificationDelegate>
#else
@interface ZendeskUnityPlugin : NSObject
#endif

@end

extern void _zendeskPush__registrationComplete(NSString *pushId);
