//
//  ZendeskProviderUpload.m
//  ZEN
//

#import "ZendeskCore.h"

#pragma mark - ZDKUploadProvider

void _zendeskUploadProviderUploadAttachment(char * gameObjectName, char * callbackId, char * attachment, char * filename, char * contentType) {
    ZDKUploadProvider *provider = [ZDKUploadProvider new];
    NSString * contentTypeHolder = GetStringParam(contentType);

    NSData * attachmentData;
    if ([contentTypeHolder rangeOfString:@"text" options:NSCaseInsensitiveSearch].location != NSNotFound ||
        [contentTypeHolder rangeOfString:@"txt" options:NSCaseInsensitiveSearch].location != NSNotFound) {
        attachmentData = [GetStringParam(attachment) dataUsingEncoding:NSUTF8StringEncoding];
    }
    else if ([contentTypeHolder rangeOfString:@"image" options:NSCaseInsensitiveSearch].location != NSNotFound ||
             [contentTypeHolder rangeOfString:@"img" options:NSCaseInsensitiveSearch].location != NSNotFound) {
        attachmentData = ZDKBase64DataFromString(GetStringParam(attachment));
    }
    else {
      NSLog(@"Warning: Upload type not recognized (%@), handling attachment as string", contentTypeHolder);
      attachmentData = [GetStringParam(attachment) dataUsingEncoding:NSUTF8StringEncoding];
    }
    
    ZDKDefCallback(ZDKUploadResponse*, [ZendeskJSON ZDKUploadResponseToJSON:result], "didUploadProviderUploadAttachment")
    [provider uploadAttachment:attachmentData
                  withFilename:GetStringParam(filename)
                andContentType:GetStringParam(contentType)
                      callback:callback];
}

void _zendeskUploadProviderDeleteUpload(char * gameObjectName, char * callbackId, char * uploadToken) {
    ZDKUploadProvider *provider = [ZDKUploadProvider new];
    ZDKDefCallback(NSString*, result, "didUploadProviderDeleteUpload")
    [provider deleteUpload:GetStringParam(uploadToken)
               andCallback:callback];
}
