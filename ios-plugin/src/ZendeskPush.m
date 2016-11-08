//
//  ZendeskPush.m
//  ZEN
//

#import "ZendeskCore.h"

#pragma mark - ZDKPush

NSMutableArray * _zendeskPush_waitingCallbacks = nil;
NSMutableArray * _zendeskPush_waitingForEnable = nil;

void _zendeskPushEnablePushWithIdentifier(char * gameObjectName, char * callbackId, char * deviceId) {
    ZDKDefCallback(ZDKPushRegistrationResponse*, [ZendeskJSON ZDKPushRegistrationResponseToJSON:result], "didPushEnable")
    [[ZDKConfig instance] enablePushWithDeviceID:GetStringParam(deviceId) callback:callback];
}

void _zendeskPushEnablePushWithUAChannelId(char * gameObjectName, char * callbackId, char * channelId) {
    ZDKDefCallback(ZDKPushRegistrationResponse*, [ZendeskJSON ZDKPushRegistrationResponseToJSON:result], "didPushEnable")
    [[ZDKConfig instance] enablePushWithUAChannelID:GetStringParam(channelId) callback:callback];
}

void _zendeskPushDisablePush(char * gameObjectName, char * callbackId, char* deviceOrChannelId) {


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


    [[ZDKConfig instance] disablePush:GetStringParam(deviceOrChannelId) callback: callback];
}
