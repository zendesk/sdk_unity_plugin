/*
 *
 *  ZDKHelpCenterDelegate.m
 *  ZendeskSDK
 *
 *  Created by Zendesk on 01/07/2017.
 *
 *  Copyright © 2015 Zendesk. All rights reserved.
 *
 *  By downloading or using the Zendesk Mobile SDK, You agree to the Zendesk Master
 *  Subscription Agreement https://www.zendesk.com/company/customers-partners/#master-subscription-agreement and Application Developer and API License
 *  Agreement https://www.zendesk.com/company/customers-partners/#application-developer-api-license-agreement and
 *  acknowledge that such terms govern Your use of and access to the Mobile SDK.
 *
 */

#import "ZDKHelpCenterDelegate.h"
#import "ZendeskSDK.h"

@interface ZDKHelpCenterDelegate ()

@end

@implementation ZDKHelpCenterDelegate


- (ZDKNavBarConversationsUIType) navBarConversationsUIType {
    return ZDKNavBarConversationsUITypeLocalizedLabel;
}

- (ZDKContactUsVisibility) active {
    return self.whereActive;
}

- (UIImage *)conversationsBarButtonImage {
    return [UIImage imageNamed:@""];
}

@end
