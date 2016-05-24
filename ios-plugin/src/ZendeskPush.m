//
//  ZendeskPush.m
//  ZEN
//

#import "ZendeskCore.h"
#import "ZendeskUnityPlugin.h"

#pragma mark - ZDKPush

BOOL _zendeskPush__isRegistrationCompleted = NO;
NSString * _zendeskPush__pushId = nil;
NSMutableArray * _zendeskPush_waitingCallbacks = nil;
NSMutableArray * _zendeskPush_waitingForEnable = nil;

void _zendeskPushEnable(char * gameObjectName, char * callbackId) {
    ZDKDefCallback(ZDKPushRegistrationResponse*, [ZendeskJSON ZDKPushRegistrationResponseToJSON:result], "didPushEnable")
    
    if (!PUSH_ENABLED) {
        if (callback)
            callback(nil, [NSError errorWithDomain:@"NSZendeskError"
                                              code:1000
                                          userInfo:@{
                                                     NSLocalizedDescriptionKey: @"Zendesk Unity SDK was not built with push notification support. Please rebuild the Zendesk Unityt SDK.",
                                                     NSLocalizedFailureReasonErrorKey: @"Zendesk Unity SDK was not built with push notification support. Please rebuild the Zendesk Unityt SDK."
                                                     }]);
        return;
    }
    
    if (_zendeskPush__isRegistrationCompleted) {
        if (_zendeskPush__pushId) {
            if (PUSH_ENABLED_UA) {
                [[ZDKConfig instance] enablePushWithUAChannelID:_zendeskPush__pushId callback:callback];
            } else {
                [[ZDKConfig instance] enablePushWithDeviceID:_zendeskPush__pushId callback:callback];
            }
        } else {
            if (callback)
                callback(nil, [NSError errorWithDomain:@"NSZendeskError"
                                                  code:1000
                                              userInfo:@{
                                                         NSLocalizedDescriptionKey: @"Failed to register app for push notifications.",
                                                         NSLocalizedFailureReasonErrorKey: @"Failed to register app for push notifications."
                                                         }]);
        }
    } else {
        if (_zendeskPush_waitingCallbacks == nil) {
            _zendeskPush_waitingCallbacks = @[].mutableCopy;
            _zendeskPush_waitingForEnable = @[].mutableCopy;
        }
        [_zendeskPush_waitingCallbacks addObject:callback];
        [_zendeskPush_waitingForEnable addObject:callback];
    }
}

void _zendeskPushDisable(char * gameObjectName, char * callbackId) {
    NSString * gameObjectNameHolder = (gameObjectName != ((void*)0)) ? [NSString stringWithUTF8String:gameObjectName] : [NSString stringWithUTF8String:""]; NSString * callbackIdHolder = (callbackId != ((void*)0)) ? [NSString stringWithUTF8String:callbackId] : [NSString stringWithUTF8String:""];
    
    void (^callback)(NSNumber*, NSError*)
    = ^(NSNumber* result, NSError *error) {
        
        NSString *stringResult = [NSString stringWithFormat:@"{\"result\": %@}", result];
        NSDictionary *data = @{@"result": stringResult ? stringResult : [NSNull null],
                               @"error": error ? [ZendeskJSON NSErrorToJSON:error] : [NSNull null],
                               @"callbackId": callbackIdHolder,
                               @"methodName": @"didPushDisable",
                               @"type": [NSNull null]};
        
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:data options:0 error:nil];
        NSString *json = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        
        UnitySendMessage(ZDKMakeStringCopy(gameObjectNameHolder.UTF8String), ZDKMakeStringCopy(("OnZendeskCallback")), json.UTF8String);
    };
    
    
    if (!PUSH_ENABLED) {
        if (callback) {
            callback(nil, [NSError errorWithDomain:@"NSZendeskError"
                                              code:1000
                                          userInfo:@{
                                                     NSLocalizedDescriptionKey: @"Zendesk Unity SDK was not built with push notification support. Please rebuild the Zendesk Unityt SDK.",
                                                     NSLocalizedFailureReasonErrorKey: @"Zendesk Unity SDK was not built with push notification support. Please rebuild the Zendesk Unityt SDK."
                                                     }]);
        }
        return;
    }
    
    if (_zendeskPush__isRegistrationCompleted) {
        if (_zendeskPush__pushId) {
            [[ZDKConfig instance] disablePush:_zendeskPush__pushId callback: callback];
        } else {
            if (callback)
                callback(nil, [NSError errorWithDomain:@"NSZendeskError"
                                                  code:1000
                                              userInfo:@{
                                                         NSLocalizedDescriptionKey: @"Failed to register app for push notifications.",
                                                         NSLocalizedFailureReasonErrorKey: @"Failed to register app for push notifications."
                                                         }]);
        }
    } else {
        if (_zendeskPush_waitingCallbacks == nil) {
            _zendeskPush_waitingCallbacks = @[].mutableCopy;
            _zendeskPush_waitingForEnable = @[].mutableCopy;
        }
        [_zendeskPush_waitingCallbacks addObject:callback];
    }
}


void _zendeskPush__registrationComplete(NSString *pushId) {
    if (_zendeskPush_waitingCallbacks == nil) {
        _zendeskPush_waitingCallbacks = @[].mutableCopy;
        _zendeskPush_waitingForEnable = @[].mutableCopy;
    }
    
    _zendeskPush__isRegistrationCompleted = YES;
    _zendeskPush__pushId = [pushId copy];
    BOOL success = pushId != nil;
    
    for (int i = 0; i < [_zendeskPush_waitingCallbacks count]; i++) {
        id callback = [_zendeskPush_waitingCallbacks objectAtIndex: i];
        BOOL enableCallback = [_zendeskPush_waitingForEnable containsObject: callback];
        if (success) {
            if (enableCallback) {
                if (PUSH_ENABLED_UA) {
                    [[ZDKConfig instance] enablePushWithUAChannelID:pushId callback:callback];
                } else {
                    [[ZDKConfig instance] enablePushWithDeviceID:pushId callback:callback];
                }
            } else
                [[ZDKConfig instance] disablePush:pushId callback:callback];
        } else {
            ((ZDKPushRegistrationCallback)[_zendeskPush_waitingCallbacks objectAtIndex: i])(nil, [NSError errorWithDomain:@"NSZendeskError"
                                                                                                                     code:1000
                                                                                                                 userInfo:@{
                                                                                                                            NSLocalizedDescriptionKey: @"Failed to register app for push notifications.",
                                                                                                                            NSLocalizedFailureReasonErrorKey: @"Failed to register app for push notifications."
                                                                                                                            }]);
        }
    }
    [_zendeskPush_waitingCallbacks removeAllObjects];
    [_zendeskPush_waitingForEnable removeAllObjects];
}
