#import "ZendeskUnityPlugin.h"
#import <ZendeskSDK/ZendeskSDK.h>
#import "ZDKConfig+Private.h"
#import "ZendeskCore.h"

NSString * const UnityPluginVersionNumber = @"1.7.0.1";
NSString * const UnityHeaderName = @"Unity";

@implementation ZendeskUnityPlugin

+ (void)load {
    NSLog(@"Zendesk UnityPlugin class loaded");
    NSNotificationCenter *center = [NSNotificationCenter defaultCenter];
    [center addObserver:[ZendeskUnityPlugin class] selector:@selector(initializeSdks:) name:UIApplicationDidFinishLaunchingNotification object:nil];
}

+ (void)initializeSdks: (NSNotification *)notification {
    [[ZDKConfig instance] addUserAgentHeaderSuffixWithKey:UnityHeaderName value:UnityPluginVersionNumber];
}

-(id) init {
    self = [super init];
    if (self) {
        // nothing for now
    }
    return self;
}

@end
