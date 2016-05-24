//
//  ZendeskRMA.m
//  ZEN
//

#import "ZendeskCore.h"

#pragma mark - ZDKRMA

void _zendeskRMAShowInView(BOOL always) {
    if (always)
        [ZDKRMA showAlwaysInView:UnityGetGLView()];
    else
        [ZDKRMA showInView:UnityGetGLView()];
}

void _zendeskRMAShowInViewWithConfig(char * additionalTags[],
                                     int additionalTagsLength,
                                     char * additionalRequestInfo,
                                     int dialogActions[],
                                     int dialogActionsLength,
                                     char * successImageName,
                                     char * errorImageName,
                                     BOOL always) {
      NSString *newAdditionalRequestInfo = GetStringParam(additionalRequestInfo);
      NSString *newSuccessImageName = GetStringParam(successImageName);
      NSString *newErrorImageName = GetStringParam(errorImageName);

      NSMutableArray * newAdditionalTags = @[].mutableCopy;
      for (int i = 0; i < additionalTagsLength; i++) {
            [newAdditionalTags addObject:GetStringParam(additionalTags[i])];
      }
      NSMutableArray * newDialogActions = @[].mutableCopy;
      for (int i = 0; i < dialogActionsLength; i++) {
            [newDialogActions addObject:[NSNumber numberWithInt:dialogActions[i]]];
      }

      [ZDKRMA configure:^(ZDKAccount *account, ZDKRMAConfigObject *config) {
            config.additionalTags = newAdditionalTags;
            config.additionalRequestInfo = newAdditionalRequestInfo;
            config.dialogActions = newDialogActions;
            config.successImageName = newSuccessImageName;
            config.errorImageName = newErrorImageName;
      }];
    
    if (always)
        [ZDKRMA showAlwaysInView:UnityGetGLView()];
    else
        [ZDKRMA showInView:UnityGetGLView()];
}

void _zendeskRMALogVisit() {
    [ZDKRMA logVisit];
}
