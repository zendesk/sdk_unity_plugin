//
//  ZendeskBinding.m
//  ZEN
//

#import "ZendeskCore.h"

#pragma mark - ZDKRequestProvider

id<ZDKRequestUpdatesProtocol> updatesDelegate;

void _zendeskRequestProviderCreateRequest(char * gameObjectName, char * callbackId, char * subject, char * description, char * email, char *tagsArray[], int tagsLength, char *attachmentsArray[], int attachmentsLength) {
    NSMutableArray * tags = @[].mutableCopy;
    for (int i = 0; i < tagsLength; i++) {
        [tags addObject:GetStringParam(tagsArray[i])];
    }
    
    NSMutableArray * attachments = @[].mutableCopy;
    for (int i = 0; i < attachmentsLength; i++) {
        ZDKUploadResponse *uploadResponse = [[ZDKUploadResponse alloc] init];
        uploadResponse.uploadToken = GetStringParam(attachmentsArray[i]);
        [attachments addObject:uploadResponse];
    }
    
    ZDKCreateRequest *request = [ZDKCreateRequest new];
    request.tags = tags;
    request.attachments = attachments;
    request.subject = GetStringParam(subject);
    request.requestDescription = GetStringParam(description);

    ZDKRequestProvider *provider = [ZDKRequestProvider new];
    ZDKDefCallback(ZDKDispatcherResponse*, [ZendeskJSON ZDKDispatcherResponseToJSON:result], "didRequestProviderCreateRequest")
    [provider createRequest:request withCallback:callback];
}

void _zendeskRequestProviderGetAllRequests(char * gameObjectName, char * callbackId) {
    ZDKRequestProvider *provider = [ZDKRequestProvider new];
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKRequestsToJSON:result], "didRequestProviderGetAllRequests")
    [provider getAllRequestsWithCallback:callback];
}

void _zendeskRequestProviderGetRequestsByStatus(char * gameObjectName, char * callbackId, char * status) {
    ZDKRequestProvider *provider = [ZDKRequestProvider new];
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKRequestsToJSON:result], "didRequestProviderGetAllRequestsByStatus")
    [provider getRequestsByStatus:GetStringParam(status) withCallback:callback];
}

void _zendeskRequestProviderGetCommentsWithRequestId(char * gameObjectName, char * callbackId, char * requestId) {
    ZDKRequestProvider *provider = [ZDKRequestProvider new];
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKCommentsToJSON:result], "didRequestProviderGetCommentsWithRequestId")
    [provider getCommentsWithRequestId:GetStringParam(requestId) withCallback:callback];
}

void _zendeskRequestProviderGetRequestWithId(char * gameObjectName, char * callbackId, char * requestId) {
    ZDKRequestProvider *provider = [ZDKRequestProvider new];
    ZDKDefCallback(ZDKRequest*, [result toJSONString], "didRequestProviderGetRequestWithId")
    [provider getRequestById:GetStringParam(requestId) withCallback:callback];
}

void _zendeskRequestProviderAddComment(char * gameObjectName, char * callbackId, char * comment, char * requestId) {
    ZDKRequestProvider *provider = [ZDKRequestProvider new];
    ZDKDefCallback(ZDKComment*, [ZendeskJSON ZDKCommentToJSON:result], "didRequestProviderAddComment")
    [provider addComment:GetStringParam(comment)
              forRequestId:GetStringParam(requestId)
              withCallback:callback];
}

void _zendeskRequestProviderGetTicketFormWithIds(char * gameObjectName, char * callbackId, int64_t ticketFormsIds[], int formsCount) {
    ZDKRequestProvider *provider = [ZDKRequestProvider new];
    ZDKDefCallback(NSArray<ZDKTicketForm*>*, [result toJSONString], "didRequestProviderGetTicketFormWithIds")

    NSMutableArray *formIds = @[].mutableCopy;
    for (int i = 0 ; i < formsCount ; i ++) {
        [formIds addObject:[NSNumber numberWithLongLong:ticketFormsIds[i]]];
    }

    [provider getTicketFormWithIds:formIds callback:callback];
}

void _zendeskRequestProviderGetUpdatesForDevice(char * gameObjectName, char * callbackId) {
    ZDKRequestProvider *provider = [ZDKRequestProvider new];
    ZDKDefCallback(ZDKRequestUpdates*, [ZendeskJSON ZDKGetDeviceUpdatesToJSON:result], "didRequestProviderGetUpdatesForDevice")
    updatesDelegate = [provider getUpdatesForDevice:callback];
}

void _zendeskRequestProviderMarkRequestAsRead(char * requestId) {
    if (updatesDelegate != nil) {
        [updatesDelegate markRequestAsRead:GetStringParam(requestId)];
    }
}

void _zendeskRequestProviderAddCommentWithAttachments(char * gameObjectName, char * callbackId, char * comment, char * requestId, char *attachmentsArray[], int attachmentsLength) {
    NSMutableArray * attachments = @[].mutableCopy;
    for (int i = 0; i < attachmentsLength; i++) {
        ZDKUploadResponse *uploadResponse = [[ZDKUploadResponse alloc] init];
        uploadResponse.uploadToken = GetStringParam(attachmentsArray[i]);
        [attachments addObject:uploadResponse];
    }

    ZDKRequestProvider *provider = [ZDKRequestProvider new];
    ZDKDefCallback(ZDKComment*, [ZendeskJSON ZDKCommentToJSON:result], "didRequestProviderAddCommentWithAttachments")
    [provider addComment:GetStringParam(comment)
              forRequestId:GetStringParam(requestId)
              attachments:attachments
              withCallback:callback];
}
