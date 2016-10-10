/*
 *
 *  ZDKConfig+Private.h
 *  ZendeskSDK
 *
 *  Created by Zendesk on 29/10/2014.
 *
 *  Copyright (c) 2014 Zendesk. All rights reserved.
 *
 *  By downloading or using the Zendesk Mobile SDK, You agree to the Zendesk Terms
 *  of Service https://www.zendesk.com/company/terms and Application Developer and API License
 *  Agreement https://www.zendesk.com/company/application-developer-and-api-license-agreement and
 *  acknowledge that such terms govern Your use of and access to the Mobile SDK.
 *
 */

#import "ZendeskSDK.h"



@interface ZDKConfig ()
@property (nonatomic, copy) NSMutableArray *userAgentHeaderSuffixes;

// user agent headers must be added after "init" is called as they get wiped in the "init" method
- (BOOL)addUserAgentHeaderSuffixWithKey:(NSString *)key value:(NSString *)value;
- (void)clearUserAgentSuffixes;

@end
