//
//  ZendeskCore.h
//  ZEN
//

#import "ZendeskSDK.h"
#import "ZendeskModalNavigationController.h"
#import "UnityAppController.h"
#import "ZendeskJSON.h"


FOUNDATION_EXPORT NSString * const UnityPluginVersionNumber;
FOUNDATION_EXPORT NSString * const UnityHeaderName;

extern void UnitySendMessage(const char *className, const char *methodName, const char *param);

// Converts C style string to NSString
#define GetStringParam(_x_) (_x_ != NULL) ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""]

// Converts C style string to NSString as long as it isnt empty
#define GetStringParamOrNil(_x_) (_x_ != NULL && strlen(_x_)) ? [NSString stringWithUTF8String:_x_] : nil

char* ZDKMakeStringCopy(const char* string);

#define UnityCallbackMethodName ("OnZendeskCallback")

#define ZDKCallback(gameObjectName,callbackId,itemType,type,customCode,resultTransform,callbackName) NSString * gameObjectNameHolder = GetStringParam(gameObjectName); \
    NSString * callbackIdHolder = GetStringParam(callbackId); \
    void (^callback)(itemType, NSError*) = ^(itemType result, NSError *error) { \
        customCode \
        NSDictionary *data = [NSDictionary dictionaryWithObjectsAndKeys: \
            result ? (resultTransform) : [NSNull null], @"result", \
            error ? [ZendeskJSON NSErrorToJSON:error] : [NSNull null], @"error", \
            callbackIdHolder, @"callbackId", \
            @(callbackName), @"methodName", \
            (type), @"type", \
            nil]; \
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:data options:0 error:NULL]; \
        NSString *json = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding]; \
        UnitySendMessage(ZDKMakeStringCopy(gameObjectNameHolder.UTF8String), \
            ZDKMakeStringCopy(UnityCallbackMethodName), \
            json.UTF8String); \
    };

#define ZDKDefCallback(itemType,itemTransform,callbackName) ZDKCallback(gameObjectName,callbackId,itemType,[NSNull null], ,itemTransform,callbackName)
#define ZDKCustomCallback(itemType,customCode,itemTransform,callbackName) ZDKCallback(gameObjectName,callbackId,itemType,[NSNull null],customCode,itemTransform,callbackName)

#define ZDKArrayCallback(itemTransform,callbackName) ZDKCallback(gameObjectName,callbackId,NSArray*,@"list", ,itemTransform,callbackName)


NSString * ZDKBase64StringFromData(NSData * data, int length);

NSData * ZDKBase64DataFromString(NSString * string);
