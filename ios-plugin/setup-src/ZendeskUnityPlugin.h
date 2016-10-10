
#import <Foundation/Foundation.h>

FOUNDATION_EXPORT NSString * const UnityPluginVersionNumber;

FOUNDATION_EXPORT NSString * const UnityHeaderName;

@interface ZendeskUnityPlugin : NSObject

@end

extern void _zendeskPush__registrationComplete(NSString *pushId);
