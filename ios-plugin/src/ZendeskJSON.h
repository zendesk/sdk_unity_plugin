
#import "ZendeskSDK.h"
#import "NSObject+ZDKBWJSONMatcher.h"

#define SafeNull(x) ((x) ? (x) : ([NSNull null]))

@interface ZendeskJSON: NSObject

+(NSString *)ZDKDispatcherResponseToJSON:(ZDKDispatcherResponse *) dispatcherResponse;
+(NSString *)NSArrayOfZDKCommentsToJSON:(NSArray *)items;
+(NSString *)NSArrayOfZDKUserFieldsToJSON:(NSArray *)items;
+(NSString *)NSArrayOfZDKHelpCenterArticlesToJSON:(NSArray *)items;
+(NSString *)NSArrayOfZDKHelpCenterCategoriesToJSON:(NSArray *)items;
+(NSString *)NSArrayOfZDKHelpCenterSectionsToJSON:(NSArray *)items;
+(NSString *)NSArrayOfZDKHelpCenterFlatArticlesToJSON:(NSArray *)items;
+(NSString *)NSArrayOfZDKHelpCenterSimpleArticlesToJSON:(NSArray *)items;
+(NSString *)NSArrayOfZDKRequestsToJSON:(NSArray *)items;
+(NSString *)NSErrorToJSON:(NSError *) error;
+(NSString *)NSHTTPURLResponseToJSON:(NSHTTPURLResponse *) response;
+(NSString *)ZDKAttachmentToJSON:(ZDKAttachment *) attachment;
+(NSString *)ZDKCommentToJSON:(ZDKCommentWithUser *) comment;
+(NSString *)ZDKSettingsToJSON:(ZDKSettings *) settings;
+(NSString *)ZDKUploadResponseToJSON:(ZDKUploadResponse *) uploadResponse;
+(NSString *)serializeJSONObject:(NSObject *) jsonObject;

+(NSString *)ZDKVoteResponseToJSON:(ZDKHelpCenterArticleVote *) response;
+(NSString *)ZDKPushRegistrationResponseToJSON:(ZDKPushRegistrationResponse *) response;
+(NSString *)ZDKGenericResponseToJSON:(id) response;

+(NSNumber *)NSStringToNSNumber:(NSString *) numberAsString;

@end
