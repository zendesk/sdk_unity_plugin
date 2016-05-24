//
//  ZendeskProviderUser.m
//  ZEN
//

#import "ZendeskCore.h"

#pragma mark - ZDKUserProvider

void _zendeskUserProviderAddTags(char * gameObjectName, char * callbackId, char *tagsArray[], int tagsLength) {
    NSMutableArray * tags = @[].mutableCopy;
    for (int i = 0; i < tagsLength; i++) {
        [tags addObject:GetStringParam(tagsArray[i])];
    }
    
    ZDKUserProvider *provider = [ZDKUserProvider new];
    ZDKArrayCallback([ZendeskJSON serializeJSONObject:result], "didUserProviderAddTags")
    [provider addTags:tags callback:callback];
}

void _zendeskUserProviderDeleteTags(char * gameObjectName, char * callbackId, char *tagsArray[], int tagsLength) {
    NSMutableArray * tags = @[].mutableCopy;
    for (int i = 0; i < tagsLength; i++) {
        [tags addObject:GetStringParam(tagsArray[i])];
    }
    
    ZDKUserProvider *provider = [ZDKUserProvider new];
    ZDKArrayCallback([ZendeskJSON serializeJSONObject:result], "didUserProviderDeleteTags")
    [provider deleteTags:tags callback:callback];
}

void _zendeskUserProviderGetUser(char * gameObjectName, char * callbackId) {
    ZDKUserProvider *provider = [ZDKUserProvider new];
    ZDKDefCallback(ZDKUser*,[ZendeskJSON serializeJSONObject:result], "didUserProviderGetUser")
    [provider getUser:callback];
}

void _zendeskUserProviderGetUserFields(char * gameObjectName, char * callbackId) {
    ZDKUserProvider *provider = [ZDKUserProvider new];
    ZDKArrayCallback([ZendeskJSON serializeJSONObject:result], "didUserProviderGetUserFields")
    [provider getUserFields:callback];
}

void _zendeskUserProviderSetUserFields(char * gameObjectName, char * callbackId, char * userFields) {
    NSDictionary *fields = [NSJSONSerialization JSONObjectWithData:[GetStringParam(userFields) dataUsingEncoding:NSUTF8StringEncoding] options:kNilOptions error:NULL];
    ZDKUserProvider *provider = [ZDKUserProvider new];
    ZDKDefCallback(NSDictionary*, [result toJSONString], "didUserProviderSetUserFields")
    [provider setUserFields:fields callback:callback];
}
