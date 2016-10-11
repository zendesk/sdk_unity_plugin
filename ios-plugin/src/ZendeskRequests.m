//
//  ZendeskRequests.m
//  ZEN
//

#import "ZendeskCore.h"

#pragma mark - ZDKRequests

void _zendeskRequestsConfigureZDKRequests(char* requestSubject, char* tags[], int tagsLength, char* additionalInfo) {

    [ZDKRequests configure:^(ZDKAccount *account, ZDKRequestCreationConfig *requestCreationConfig) {

         if (requestSubject) {
             requestCreationConfig.subject = GetStringParam(requestSubject);
         }

         if (tags && tagsLength > 0) {
             NSMutableArray *nsTags = [NSMutableArray new];

             for (int i = 0; i < tagsLength; i++) {
                 [nsTags addObject:GetStringParam(tags[i])];
             }

             requestCreationConfig.tags = nsTags;
         }

         if (additionalInfo) {
             requestCreationConfig.additionalRequestInfo = GetStringParam(additionalInfo);
         }
    }];
}

void _zendeskRequestsShowRequestCreation() {
    [ZDKRequests presentRequestCreationWithViewController:UnityGetGLViewController()];
}
