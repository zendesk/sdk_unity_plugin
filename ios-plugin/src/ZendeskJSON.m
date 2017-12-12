

#import "ZendeskJSON.h"

@interface ZendeskJSON ()

@end

@implementation ZendeskJSON

+(NSString *)ZDKDispatcherResponseToJSON:(ZDKDispatcherResponse *) dispatcherResponse {
    NSDictionary *dict = [NSDictionary dictionaryWithObjectsAndKeys:
        [ZendeskJSON NSHTTPURLResponseToJSON:dispatcherResponse.response], @"response",
        [[NSString alloc] initWithData:dispatcherResponse.data encoding:NSUTF8StringEncoding], @"data",
        nil];
    return [ZendeskJSON serializeJSONObject:dict];
}

+(NSString *)NSArrayOfZDKUserFieldsToJSON:(NSArray *)items {
    NSMutableArray *results = [[NSMutableArray alloc] init];
    for (ZDKUserField *field in items) {
        [results addObject:[ZendeskJSON ZDKUserFieldToJSON:field]];
    }
    return [ZendeskJSON serializeJSONObject:[results copy]];
}

+(NSString *)NSArrayOfZDKCommentsToJSON:(NSArray *)items {
    NSMutableArray *results = [[NSMutableArray alloc] init];
    for (ZDKCommentWithUser *comment in items) {
        [results addObject:[ZendeskJSON ZDKCommentToJSON:comment]];
    }
    return [ZendeskJSON serializeJSONObject:[results copy]];
}

+(NSString *)NSArrayOfZDKHelpCenterArticlesToJSON:(NSArray *)items {
    NSMutableArray *results = [[NSMutableArray alloc] init];
    for (ZDKHelpCenterArticle *article in items) {
        NSDictionary *data = [article toJSONObject];
        [results addObject:data];
    }
    return [ZendeskJSON serializeJSONObject:[results copy]];
}

+(NSString *)NSArrayOfZDKHelpCenterFlatArticlesToJSON:(NSArray *)items {
    NSMutableArray *results = [[NSMutableArray alloc] init];
    for (ZDKHelpCenterFlatArticle *article in items) {
        NSDictionary *data = [article toJSONObject];
        [results addObject:data];
    }
    return [ZendeskJSON serializeJSONObject:[results copy]];
}

+(NSString *)NSArrayOfZDKHelpCenterSimpleArticlesToJSON:(NSArray *)items {
    NSMutableArray *results = [[NSMutableArray alloc] init];
    for (ZDKHelpCenterSimpleArticle *article in items) {
        NSDictionary *data = [article toJSONObject];
        [results addObject:data];
    }
    return [ZendeskJSON serializeJSONObject:[results copy]];
}

+(NSString *)NSArrayOfZDKHelpCenterCategoriesToJSON:(NSArray *)items {
    NSMutableArray *results = [[NSMutableArray alloc] init];
    for (ZDKHelpCenterCategory *category in items) {
        NSDictionary *data = [category toJSONObject];
        [results addObject:data];
    }
    return [ZendeskJSON serializeJSONObject:[results copy]];
}

+(NSString *)NSArrayOfZDKHelpCenterSectionsToJSON:(NSArray *)items {
    NSMutableArray *results = [[NSMutableArray alloc] init];
    for (ZDKHelpCenterSection *section in items) {
        NSDictionary *data = [section toJSONObject];
        [results addObject:data];
    }
    return [ZendeskJSON serializeJSONObject:[results copy]];
}

+(NSString *)NSArrayOfZDKRequestsToJSON:(NSArray *)items {
    NSMutableArray *results = [[NSMutableArray alloc] init];
    for (ZDKRequest *request in items) {
        NSDictionary *data = [request toJSONObject];
        [results addObject:data];
    }
    return [ZendeskJSON serializeJSONObject:[results copy]];
}

+(NSString *)NSErrorToJSON:(NSError *) error {
    NSDictionary *dict = [NSDictionary dictionaryWithObjectsAndKeys:
        [NSNumber numberWithInteger:error.code], @"code",
        error.domain, @"domain",
        //error.userInfo, @"userInfo", dict has sub-objects that need to be converted
        error.localizedDescription, @"localizedDescription",
        error.localizedFailureReason, @"localizedFailureReason",
        nil];
    return [ZendeskJSON serializeJSONObject:dict];
}

+(NSString *)NSHTTPURLResponseToJSON:(NSHTTPURLResponse *) response {
    NSDictionary *dict = [NSDictionary dictionaryWithObjectsAndKeys:
                          [NSNumber numberWithInteger:response.statusCode], @"statusCode",
                          [NSHTTPURLResponse localizedStringForStatusCode:response.statusCode], @"localizedStringForStatusCode",
                          nil];
    return [ZendeskJSON serializeJSONObject:dict];
}

+(NSString *)ZDKVoteResponseToJSON:(ZDKHelpCenterArticleVote *) response {
    NSDictionary *dict = [response toJSONObject];
    return [ZendeskJSON serializeJSONObject:dict];
}

+(NSString *)ZDKGetDeviceUpdatesToJSON:(ZDKRequestUpdates *) response {
    NSDictionary *dict = [NSDictionary dictionaryWithObjectsAndKeys:
        [NSNumber numberWithBool:response.hasUpdates], @"hasUpdates",
        response.updateCount, @"updateCount",
        response.requestsWithUpdates, @"requestsWithUpdates",
        nil];
    return [ZendeskJSON serializeJSONObject:dict];
}

+(NSString *)ZDKGenericResponseToJSON:(id) response {
    NSDictionary *dict = [NSDictionary dictionaryWithObjectsAndKeys:
                          @([response statusCode]), @"statusCode",
                          nil];
    return [ZendeskJSON serializeJSONObject:dict];
}

+(NSString *)ZDKPushRegistrationResponseToJSON:(ZDKPushRegistrationResponse *) response {
    NSDictionary *dict = [response toJSONObject];
    return [ZendeskJSON serializeJSONObject:dict];
}

+(NSString *)ZDKAttachmentToJSON:(ZDKAttachment *) attachment {
    //Thumbnails associated with the attachment. A thumbnail is an attachment with a nil thumbnails array.
    NSMutableArray *thumbnails = nil; 
    if (attachment.thumbnails) {
        thumbnails = [[NSMutableArray alloc] init];
        for (ZDKAttachment *thumbnail in attachment.thumbnails) {
            [thumbnails addObject:[ZendeskJSON ZDKAttachmentToJSON:thumbnail]];
        }
    }

    NSDictionary *dict = [NSDictionary dictionaryWithObjectsAndKeys:
        attachment.attachmentId, @"attachmentId",
        attachment.filename, @"filename",
        attachment.contentURLString, @"contentURLString",
        attachment.contentType, @"contentType",
        attachment.size, @"size",
        thumbnails, @"thumbnails",
        nil];
    return [ZendeskJSON serializeJSONObject:dict];
}

+(NSString *)ZDKCommentToJSON:(ZDKCommentWithUser *) commentWithUser {
    NSMutableArray *attachmentResults = [[NSMutableArray alloc] init];
    for (ZDKAttachment *attachment in commentWithUser.comment.attachments) {
        [attachmentResults addObject:[ZendeskJSON ZDKAttachmentToJSON:attachment]];
    }
    
    NSDictionary *dict = [NSDictionary dictionaryWithObjectsAndKeys:
                          commentWithUser.comment.commentId, @"commentId",
                          SafeNull(commentWithUser.comment.body), @"body",
                          commentWithUser.comment.authorId, @"authorId",
                          [NSNumber numberWithDouble:commentWithUser.comment.createdAt.timeIntervalSince1970], @"createdAt",
                          attachmentResults, @"attachments",
                          nil];
    
    return [ZendeskJSON serializeJSONObject:dict];
}

+(NSString *)ZDKUserFieldToJSON:(ZDKUserField *) userField {
    NSMutableDictionary *customFieldOptions = [[NSMutableDictionary alloc] init];
    for (ZDKCustomField *field in userField.customFieldOptions) {
        [customFieldOptions setObject:field.value forKey:field.fieldId];
    }
    
    NSDictionary *dict = [NSDictionary dictionaryWithObjectsAndKeys:
                          userField.userFieldId, @"userFieldId",
                          SafeNull(userField.title), @"title",
                          SafeNull(userField.typeOfField), @"typeOfField",
                          SafeNull(userField.fieldDescription), @"fieldDescription",
                          customFieldOptions, @"customFieldOptions",
                          nil];
    
    return [ZendeskJSON serializeJSONObject:dict];
}

+(NSString *)ZDKSettingsToJSON:(ZDKSettings *) settings {
    NSDictionary *helpCenterSettings = [NSDictionary dictionaryWithObjectsAndKeys:
        [NSNumber numberWithBool:settings.appSettings.helpCenterSettings.enabled], @"enabled",
        SafeNull(settings.appSettings.helpCenterSettings.locale), @"locale",
        nil];
    NSDictionary *contactUsSettings = [NSDictionary dictionaryWithObjectsAndKeys:
        SafeNull(settings.appSettings.contactUsSettings.tags), @"tags",
        nil];
    NSDictionary *conversationsSettings = [NSDictionary dictionaryWithObjectsAndKeys:
        [NSNumber numberWithBool:settings.appSettings.conversationsSettings.enabled], @"enabled",
        nil];

    NSDictionary *appSettings = [NSDictionary dictionaryWithObjectsAndKeys:
        settings.appSettings.authentication, @"authentication",
        helpCenterSettings, @"helpCenterSettings",
        contactUsSettings, @"contactUsSettings",
        conversationsSettings, @"conversationsSettings",
        nil];

    NSDictionary *attachmentSettings = [NSDictionary dictionaryWithObjectsAndKeys:
        [NSNumber numberWithBool:settings.accountSettings.attachmentSettings.enabled], @"enabled",
        settings.accountSettings.attachmentSettings.maxAttachmentSize, @"maxAttachmentSize",
        nil];
    NSDictionary *accountSettings = [NSDictionary dictionaryWithObjectsAndKeys:
        attachmentSettings, @"attachmentSettings",
        nil];

    NSDictionary *dict = [NSDictionary dictionaryWithObjectsAndKeys:
        appSettings, @"appSettings",
        accountSettings, @"accountSettings",
        nil];
    return [ZendeskJSON serializeJSONObject:dict];
}

+(NSString *)ZDKUploadResponseToJSON:(ZDKUploadResponse *) uploadResponse {
    NSDictionary *dict = [NSDictionary dictionaryWithObjectsAndKeys:
        SafeNull(uploadResponse.uploadToken), @"uploadToken",
        [ZendeskJSON ZDKAttachmentToJSON:uploadResponse.attachment], @"attachment",
        nil];
    return [ZendeskJSON serializeJSONObject:dict];
}

+(NSString *)serializeJSONObject:(NSObject *) jsonObject {
    return [jsonObject toJSONString];
}

+(NSNumber *)NSStringToNSNumber:(NSString *) numberAsString {
    NSNumberFormatter *formatter = [[NSNumberFormatter alloc] init];
    formatter.numberStyle = NSNumberFormatterDecimalStyle;
    return [formatter numberFromString:numberAsString];
}

@end
