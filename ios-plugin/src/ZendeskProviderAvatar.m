//
//  ZendeskProviderAvatar.m
//  ZEN
//

#import "ZendeskCore.h"

#pragma mark - ZDKAvatarProvider

void _zendeskAvatarProviderGetAvatar(char * gameObjectName, char * callbackId, char * avatarUrl) {
    ZDKAvatarProvider *provider = [ZDKAvatarProvider new];
    ZDKCustomCallback(UIImage*,
                      NSData *imageData = UIImagePNGRepresentation(result);,
                      imageData ? ZDKBase64StringFromData(imageData, (int)imageData.length) : [NSNull null],
                      "didAvatarProviderGetAvatar")
    [provider getAvatarForUrl:GetStringParam(avatarUrl)
                 withCallback:callback];
}
