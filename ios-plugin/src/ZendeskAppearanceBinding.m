//
//  ZendeskApperanceBinding.m
//  ZEN
//

#import "ZendeskCore.h"

#pragma mark - Appearance

void _zendeskSetColor(NSString *className, NSString *propertyName, UIColor *color) {
    if ([className isEqualToString:@"ZDKSupportView"]) {
        if ([propertyName isEqualToString:@"backgroundColor"]) {
            [[ZDKSupportView appearance] setBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"tableBackgroundColor"]) {
            [[ZDKSupportView appearance] setTableBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"separatorColor"]) {
            [[ZDKSupportView appearance] setSeparatorColor:color];
        }
        else if ([propertyName isEqualToString:@"noResultsFoundLabelColor"]) {
            [[ZDKSupportView appearance] setNoResultsFoundLabelColor:color];
        }
        else if ([propertyName isEqualToString:@"noResultsFoundLabelBackground"]) {
            [[ZDKSupportView appearance] setNoResultsFoundLabelBackground:color];
        }
        else if ([propertyName isEqualToString:@"noResultsContactButtonBackground"]) {
            [[ZDKSupportView appearance] setNoResultsContactButtonBackground:color];
        }
        else if ([propertyName isEqualToString:@"noResultsContactButtonBorderColor"]) {
            [[ZDKSupportView appearance] setNoResultsContactButtonBorderColor:color];
        }
        else if ([propertyName isEqualToString:@"noResultsContactButtonTitleColorNormal"]) {
            [[ZDKSupportView appearance] setNoResultsContactButtonTitleColorNormal:color];
        }
        else if ([propertyName isEqualToString:@"noResultsContactButtonTitleColorHighlighted"]) {
            [[ZDKSupportView appearance] setNoResultsContactButtonTitleColorHighlighted:color];
        }
        else if ([propertyName isEqualToString:@"noResultsContactButtonTitleColorDisabled"]) {
            [[ZDKSupportView appearance] setNoResultsContactButtonTitleColorDisabled:color];
        }
        else if ([propertyName isEqualToString:@"spinner"]) {
            UIActivityIndicatorView * spinner = [[UIActivityIndicatorView alloc] initWithFrame:CGRectMake(0, 0, 20, 20)];
            [spinner setColor:color];
            [[ZDKSupportView appearance] setSpinner:(id<ZDKSpinnerDelegate>)spinner];
        }
        else {
            NSLog(@"ERROR: _zendeskSetColor:%@ - Unrecognized propertyName %@", className, propertyName);
        }
    }
    else if ([className isEqualToString:@"ZDKRequestListLoadingTableCell"]) {
        if ([propertyName isEqualToString:@"backgroundColor"]) {
            [[ZDRequestListLoadingTableCell appearance] setBackgroundColor:color];
        }
        else {
            NSLog(@"ERROR: _zendeskSetColor:%@ - Unrecognized propertyName %@", className, propertyName);
        }
    }
    else if ([className isEqualToString:@"ZDKAgentCommentTableCell"]) {
        if ([propertyName isEqualToString:@"cellBackground"]) {
            [[ZDKAgentCommentTableCell appearance] setCellBackground:color];
        }
        else if ([propertyName isEqualToString:@"timestampColor"]) {
            [[ZDKAgentCommentTableCell appearance] setTimestampColor:color];
        }
        else if ([propertyName isEqualToString:@"bodyColor"]) {
            [[ZDKAgentCommentTableCell appearance] setBodyColor:color];
        }
        else if ([propertyName isEqualToString:@"agentNameColor"]) {
            [[ZDKAgentCommentTableCell appearance] setAgentNameColor:color];
        }
        else if ([propertyName isEqualToString:@"backgroundColor"]) {
            [[ZDKAgentCommentTableCell appearance] setBackgroundColor:color];
        }
        else {
            NSLog(@"ERROR: _zendeskSetColor:%@ - Unrecognized propertyName %@", className, propertyName);
        }
    }
    else if ([className isEqualToString:@"ZDKAttachmentView"]) {
        if ([propertyName isEqualToString:@"closeButtonBackgroundColor"]) {
            [[ZDKAttachmentView appearance] setCloseButtonBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"backgroundColor"]) {
            [[ZDKAttachmentView appearance] setBackgroundColor:color];
        }
        else {
            NSLog(@"ERROR: _zendeskSetColor:%@ - Unrecognized propertyName %@", className, propertyName);
        }
    }
    else if ([className isEqualToString:@"ZDKCommentEntryView"]) {
        if ([propertyName isEqualToString:@"backgroundColor"]) {
            [[ZDKCommentInputView appearance] setBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"topBorderColor"]) {
            [[ZDKCommentInputView appearance] setTopBorderColor:color];
        }
        else if ([propertyName isEqualToString:@"textEntryColor"]) {
            [[ZDKCommentInputView appearance] setTextEntryColor:color];
        }
        else if ([propertyName isEqualToString:@"textEntryBackgroundColor"]) {
            [[ZDKCommentInputView appearance] setTextEntryBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"textEntryBorderColor"]) {
            [[ZDKCommentInputView appearance] setTextEntryBorderColor:color];
        }
        else if ([propertyName isEqualToString:@"sendButtonColor"]) {
            [[ZDKCommentInputView appearance] setSendButtonColor:color];
        }
        else if ([propertyName isEqualToString:@"areaBackgroundColor"]) {
            [[ZDKCommentInputView appearance] setAreaBackgroundColor:color];
        }
        else {
            NSLog(@"ERROR: _zendeskSetColor:%@ - Unrecognized propertyName %@", className, propertyName);
        }
    }
    else if ([className isEqualToString:@"ZDKCommentInputView"]) {
        if ([propertyName isEqualToString:@"backgroundColor"]) {
            [[ZDKCommentInputView appearance] setBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"attachmentButtonBackgroundColor"]) {
            [[ZDKCommentInputView appearance] setAttachmentButtonBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"topBorderColor"]) {
            [[ZDKCommentInputView appearance] setTopBorderColor:color];
        }
        else if ([propertyName isEqualToString:@"textEntryColor"]) {
            [[ZDKCommentInputView appearance] setTextEntryColor:color];
        }
        else if ([propertyName isEqualToString:@"textEntryBackgroundColor"]) {
            [[ZDKCommentInputView appearance] setTextEntryBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"textEntryBorderColor"]) {
            [[ZDKCommentInputView appearance] setTextEntryBorderColor:color];
        }
        else if ([propertyName isEqualToString:@"sendButtonColor"]) {
            [[ZDKCommentInputView appearance] setSendButtonColor:color];
        }
        else if ([propertyName isEqualToString:@"areaBackgroundColor"]) {
            [[ZDKCommentInputView appearance] setAreaBackgroundColor:color];
        }
        else {
            NSLog(@"ERROR: _zendeskSetColor:%@ - Unrecognized propertyName %@", className, propertyName);
        }
    }
    else if ([className isEqualToString:@"ZDKCommentsListLoadingTableCell"]) {
        if ([propertyName isEqualToString:@"backgroundColor"]) {
            [[ZDKCommentsListLoadingTableCell appearance] setBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"cellBackground"]) {
            [[ZDKCommentsListLoadingTableCell appearance] setCellBackground:color];
        }
        else {
            NSLog(@"ERROR: _zendeskSetColor:%@ - Unrecognized propertyName %@", className, propertyName);
        }
    }
    else if ([className isEqualToString:@"ZDKRequestListEmptyTableCell"]) {
        if ([propertyName isEqualToString:@"backgroundColor"]) {
            [[ZDRequestListEmptyTableCell appearance] setBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"messageColor"]) {
            [[ZDRequestListEmptyTableCell appearance] setMessageColor:color];
        }
        else {
            NSLog(@"ERROR: _zendeskSetColor:%@ - Unrecognized propertyName %@", className, propertyName);
        }
    }
    else if ([className isEqualToString:@"ZDKUILoadingView"]) {
        if ([propertyName isEqualToString:@"backgroundColor"]) {
            [[ZDKUILoadingView appearance] setBackgroundColor:color];
        }
        else {
            NSLog(@"ERROR: _zendeskSetColor:%@ - Unrecognized propertyName %@", className, propertyName);
        }
    }
    else if ([className isEqualToString:@"ZDKUIImageScrollView"]) {
        if ([propertyName isEqualToString:@"backgroundColor"]) {
            [[ZDKUIImageScrollView appearance] setBackgroundColor:color];
        }
        else {
            NSLog(@"ERROR: _zendeskSetColor:%@ - Unrecognized propertyName %@", className, propertyName);
        }
    }
    else if ([className isEqualToString:@"ZDKSupportTableViewCell"]) {
        if ([propertyName isEqualToString:@"backgroundColor"]) {
            [[ZDKSupportTableViewCell appearance] setBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"titleLabelColor"]) {
            [[ZDKSupportTableViewCell appearance] setTitleLabelColor:color];
        }
        else if ([propertyName isEqualToString:@"titleLabelBackground"]) {
            [[ZDKSupportTableViewCell appearance] setTitleLabelBackground:color];
        }
        else {
            NSLog(@"ERROR: _zendeskSetColor:%@ - Unrecognized propertyName %@", className, propertyName);
        }
    }
    else if ([className isEqualToString:@"ZDKSupportAttachmentCell"]) {
        if ([propertyName isEqualToString:@"backgroundColor"]) {
            [[ZDKSupportAttachmentCell appearance] setBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"titleLabelColor"]) {
            [[ZDKSupportAttachmentCell appearance] setTitleLabelColor:color];
        }
        else if ([propertyName isEqualToString:@"titleLabelBackground"]) {
            [[ZDKSupportAttachmentCell appearance] setTitleLabelBackground:color];
        }
        else if ([propertyName isEqualToString:@"fileSizeLabelColor"]) {
            [[ZDKSupportAttachmentCell appearance] setFileSizeLabelColor:color];
        }
        else if ([propertyName isEqualToString:@"fileSizeLabelBackground"]) {
            [[ZDKSupportAttachmentCell appearance] setFileSizeLabelBackground:color];
        }
        else {
            NSLog(@"ERROR: _zendeskSetColor:%@ - Unrecognized propertyName %@", className, propertyName);
        }
    }
    else if ([className isEqualToString:@"ZDKSupportArticleTableViewCell"]) {
        if ([propertyName isEqualToString:@"backgroundColor"]) {
            [[ZDKSupportArticleTableViewCell appearance] setBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"titleLabelColor"]) {
            [[ZDKSupportArticleTableViewCell appearance] setTitleLabelColor:color];
        }
        else if ([propertyName isEqualToString:@"titleLabelBackground"]) {
            [[ZDKSupportArticleTableViewCell appearance] setTitleLabelBackground:color];
        }
        else if ([propertyName isEqualToString:@"articleParentsLabelColor"]) {
            [[ZDKSupportArticleTableViewCell appearance] setArticleParentsLabelColor:color];
        }
        else if ([propertyName isEqualToString:@"articleParentsLabelBackground"]) {
            // Note the typo in ZendeskSDK's method name 'Parnets'.
            [[ZDKSupportArticleTableViewCell appearance] setArticleParnetsLabelBackground:color];
        }
        else {
            NSLog(@"ERROR: _zendeskSetColor:%@ - Unrecognized propertyName %@", className, propertyName);
        }
    }
    else if ([className isEqualToString:@"ZDKRMAFeedbackView"]) {
        if ([propertyName isEqualToString:@"backgroundColor"]) {
            [[ZDKRMAFeedbackView appearance] setBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"buttonColor"]) {
            [[ZDKRMAFeedbackView appearance] setButtonColor:color];
        }
        else if ([propertyName isEqualToString:@"buttonSelectedColor"]) {
            [[ZDKRMAFeedbackView appearance] setButtonSelectedColor:color];
        }
        else if ([propertyName isEqualToString:@"buttonBackgroundColor"]) {
            [[ZDKRMAFeedbackView appearance] setButtonBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"separatorLineColor"]) {
            [[ZDKRMAFeedbackView appearance] setSeparatorLineColor:color];
        }
        else if ([propertyName isEqualToString:@"headerColor"]) {
            [[ZDKRMAFeedbackView appearance] setHeaderColor:color];
        }
        else if ([propertyName isEqualToString:@"subHeaderColor"]) {
            [[ZDKRMAFeedbackView appearance] setSubHeaderColor:color];
        }
        else if ([propertyName isEqualToString:@"textEntryColor"]) {
            [[ZDKRMAFeedbackView appearance] setTextEntryColor:color];
        }
        else if ([propertyName isEqualToString:@"textEntryBackgroundColor"]) {
            [[ZDKRMAFeedbackView appearance] setTextEntryBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"placeHolderColor"]) {
            [[ZDKRMAFeedbackView appearance] setPlaceHolderColor:color];
        }
        else if([propertyName isEqualToString:@"viewBackgroundColor"]){
            [[ZDKRMAFeedbackView appearance] setViewBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"spinner"]) {
            UIActivityIndicatorView * spinner = [[UIActivityIndicatorView alloc] initWithFrame:CGRectMake(0, 0, 20, 20)];
            [spinner setColor:color];
            [[ZDKRMAFeedbackView appearance] setSpinner:(id<ZDKSpinnerDelegate>)spinner];
        }
        else {
            NSLog(@"ERROR: _zendeskSetColor:%@ - Unrecognized propertyName %@", className, propertyName);
        }
    }
    else if ([className isEqualToString:@"ZDKRMADialogView"]) {
        if ([propertyName isEqualToString:@"backgroundColor"]) {
            [[ZDKRMADialogView appearance] setBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"headerBackgroundColor"]) {
            [[ZDKRMADialogView appearance] setHeaderBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"headerColor"]) {
            [[ZDKRMADialogView appearance] setHeaderColor:color];
        }
        else if ([propertyName isEqualToString:@"separatorLineColor"]) {
            [[ZDKRMADialogView appearance] setSeparatorLineColor:color];
        }
        else if ([propertyName isEqualToString:@"buttonBackgroundColor"]) {
            [[ZDKRMADialogView appearance] setButtonBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"buttonSelectedBackgroundColor"]) {
            [[ZDKRMADialogView appearance] setButtonSelectedBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"buttonColor"]) {
            [[ZDKRMADialogView appearance] setButtonColor:color];
        }
        else {
            NSLog(@"ERROR: _zendeskSetColor:%@ - Unrecognized propertyName %@", className, propertyName);
        }
    }
    else if ([className isEqualToString:@"ZDKRequestCommentTableCell"]) {
        if ([propertyName isEqualToString:@"backgroundColor"]) {
            [[ZDKRequestCommentTableCell appearance] setBackgroundColor:color];
        }
        else {
            NSLog(@"ERROR: _zendeskSetColor:%@ - Unrecognized propertyName %@", className, propertyName);
        }
    }
    else if ([className isEqualToString:@"ZDKRequestListTableCell"]) {
        if ([propertyName isEqualToString:@"backgroundColor"]) {
            [[ZDKRequestListTableCell appearance] setBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"unreadColor"]) {
            [[ZDKRequestListTableCell appearance] setUnreadColor:color];
        }
        else if ([propertyName isEqualToString:@"descriptionColor"]) {
            [[ZDKRequestListTableCell appearance] setDescriptionColor:color];
        }
        else if ([propertyName isEqualToString:@"createdAtColor"]) {
            [[ZDKRequestListTableCell appearance] setCreatedAtColor:color];
        }
        else if ([propertyName isEqualToString:@"cellBackgroundColor"]) {
            [[ZDKRequestListTableCell appearance] setCellBackgroundColor:color];
        }
        else {
            NSLog(@"ERROR: _zendeskSetColor:%@ - Unrecognized propertyName %@", className, propertyName);
        }
    }
    else if ([className isEqualToString:@"ZDKRequestListTable"]) {
        if ([propertyName isEqualToString:@"backgroundColor"]) {
            [[ZDKRequestListTable appearance] setBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"cellSeparatorColor"]) {
            [[ZDKRequestListTable appearance] setCellSeparatorColor:color];
        }
        else if ([propertyName isEqualToString:@"tableBackgroundColor"]) {
            [[ZDKRequestListTable appearance] setTableBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"sectionIndexColor"]) {
            [[ZDKRequestListTable appearance] setSectionIndexColor:color];
        }
        else if ([propertyName isEqualToString:@"sectionIndexBackgroundColor"]) {
            [[ZDKRequestListTable appearance] setSectionIndexBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"sectionIndexTrackingBackgroundColor"]) {
            [[ZDKRequestListTable appearance] setSectionIndexTrackingBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"separatorColor"]) {
            [[ZDKRequestListTable appearance] setSeparatorColor:color];
        }
        else {
            NSLog(@"ERROR: _zendeskSetColor:%@ - Unrecognized propertyName %@", className, propertyName);
        }
    }
    else if ([className isEqualToString:@"ZDKRequestCommentAttachmentTableCell"]) {
        if ([propertyName isEqualToString:@"backgroundColor"]) {
            [[ZDKRequestCommentAttachmentTableCell appearance] setBackgroundColor:color];
        }
        else {
            NSLog(@"ERROR: _zendeskSetColor:%@ - Unrecognized propertyName %@", className, propertyName);
        }
    }
    else if ([className isEqualToString:@"ZDKRequestCommentAttachmentLoadingTableCell"]) {
        if ([propertyName isEqualToString:@"backgroundColor"]) {
            [[ZDKRequestCommentAttachmentLoadingTableCell appearance] setBackgroundColor:color];
        }
        else {
            NSLog(@"ERROR: _zendeskSetColor:%@ - Unrecognized propertyName %@", className, propertyName);
        }
    }
    else if ([className isEqualToString:@"ZDKCreateRequestView"]) {
        if ([propertyName isEqualToString:@"backgroundColor"]) {
            [[ZDKCreateRequestView appearance] setBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"placeholderTextColor"]) {
            [[ZDKCreateRequestView appearance] setPlaceholderTextColor:color];
        }
        else if ([propertyName isEqualToString:@"textEntryColor"]) {
            [[ZDKCreateRequestView appearance] setTextEntryColor:color];
        }
        else if ([propertyName isEqualToString:@"textEntryBackgroundColor"]) {
            [[ZDKCreateRequestView appearance] setTextEntryBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"attachmentButtonBorderColor"]) {
            [[ZDKCreateRequestView appearance] setAttachmentButtonBorderColor:color];
        }
        else if ([propertyName isEqualToString:@"attachmentButtonBackground"]) {
            [[ZDKCreateRequestView appearance] setAttachmentButtonBackground:color];
        }
        else if ([propertyName isEqualToString:@"spinner"]) {
            UIActivityIndicatorView * spinner = [[UIActivityIndicatorView alloc] initWithFrame:CGRectMake(0, 0, 20, 20)];
            [spinner setColor:color];
            [[ZDKCreateRequestView appearance] setSpinner:(id<ZDKSpinnerDelegate>)spinner];
        }
        else {
            NSLog(@"ERROR: _zendeskSetColor:%@ - Unrecognized propertyName %@", className, propertyName);
        }
    }
    else if ([className isEqualToString:@"ZDKEndUserCommentTableCell"]) {
        if ([propertyName isEqualToString:@"backgroundColor"]) {
            [[ZDKEndUserCommentTableCell appearance] setBackgroundColor:color];
        }
        else if ([propertyName isEqualToString:@"bodyColor"]) {
            [[ZDKEndUserCommentTableCell appearance] setBodyColor:color];
        }
        else if ([propertyName isEqualToString:@"timestampColor"]) {
            [[ZDKEndUserCommentTableCell appearance] setTimestampColor:color];
        }
        else if ([propertyName isEqualToString:@"cellBackground"]) {
            [[ZDKEndUserCommentTableCell appearance] setCellBackground:color];
        }
        else {
            NSLog(@"ERROR: _zendeskSetColor:%@ - Unrecognized propertyName %@", className, propertyName);
        }
    }
    else {
        NSLog(@"ERROR: _zendeskSetColor - Unrecognized className %@ and property %@", className, propertyName);
    }
}

void _zendeskSetColorWithWhite(char *className, char *propertyName, float white, float alpha) {
    UIColor *color = [UIColor colorWithWhite:white alpha:alpha];
    _zendeskSetColor(GetStringParam(className), GetStringParam(propertyName), color);
}

void _zendeskSetColorWithRed(char *className, char *propertyName, float red, float green, float blue, float alpha) {
    UIColor *color = [UIColor colorWithRed:red green:green blue:blue alpha:alpha];
    _zendeskSetColor(GetStringParam(className), GetStringParam(propertyName), color);
}

void _zendeskSetColorWithName(char *className, char *propertyName, char *colorName) {
    
    UIColor *color; // TODO: create UIColor with the given colorName, probably want to do an enum mapping
    
    _zendeskSetColor(GetStringParam(className), GetStringParam(propertyName), color);
}

void _zendeskSetFont(char *className, char *propertyName, char *fontName, float size) {
    UIFont *font = [UIFont fontWithName:GetStringParam(fontName) size:size];
    NSString *class = GetStringParam(className);
    NSString *property = GetStringParam(propertyName);
    if ([class isEqualToString:@"ZDKSupportView"]) {
        if ([property isEqualToString:@"noResultsFoundLabelFont"]) {
            [[ZDKSupportView appearance] setNoResultsFoundLabelFont:font];
        }
        else if ([property isEqualToString:@"noResultsContactButtonFont"]) {
            [[ZDKSupportView appearance] setNoResultsContactButtonFont:font];
        }
        else {
            NSLog(@"ERROR: _zendeskSetFont:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKAgentCommentTableCell"]) {
        if ([property isEqualToString:@"agentNameFont"]) {
            [[ZDKAgentCommentTableCell appearance] setAgentNameFont:font];
        }
        else if ([property isEqualToString:@"bodyFont"]) {
            [[ZDKAgentCommentTableCell appearance] setBodyFont:font];
        }
        else if ([property isEqualToString:@"timestampFont"]) {
            [[ZDKAgentCommentTableCell appearance] setTimestampFont:font];
        }
        else {
            NSLog(@"ERROR: _zendeskSetFont:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKCommentEntryView"]) {
        if ([property isEqualToString:@"textEntryFont"]) {
            [[ZDKCommentInputView appearance] setTextEntryFont:font];
        }
        else if ([property isEqualToString:@"sendButtonFont"]) {
            [[ZDKCommentInputView appearance] setSendButtonFont:font];
        }
        else {
            NSLog(@"ERROR: _zendeskSetFont:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKCommentInputView"]) {
        if ([property isEqualToString:@"textEntryFont"]) {
            [[ZDKCommentInputView appearance] setTextEntryFont:font];
        }
        else if ([property isEqualToString:@"sendButtonFont"]) {
            [[ZDKCommentInputView appearance] setSendButtonFont:font];
        }
        else {
            NSLog(@"ERROR: _zendeskSetFont:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKRequestListEmptyTableCell"]) {
        if ([property isEqualToString:@"messageFont"]) {
            [[ZDRequestListEmptyTableCell appearance] setMessageFont:font];
        }
        else {
            NSLog(@"ERROR: _zendeskSetFont:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKSupportTableViewCell"]) {
        if ([property isEqualToString:@"titleLabelFont"]) {
            [[ZDKSupportTableViewCell appearance] setTitleLabelFont:font];
        }
        else {
            NSLog(@"ERROR: _zendeskSetFont:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKSupportAttachmentCell"]) {
        if ([property isEqualToString:@"titleLabelFont"]) {
            [[ZDKSupportAttachmentCell appearance] setTitleLabelFont:font];
        }
        else {
            NSLog(@"ERROR: _zendeskSetFont:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKSupportArticleTableViewCell"]) {
        if ([property isEqualToString:@"articleParentsLabelFont"]) {
            [[ZDKSupportArticleTableViewCell appearance] setArticleParentsLabelFont:font];
        }
        else if ([property isEqualToString:@"titleLabelFont"]) {
            [[ZDKSupportArticleTableViewCell appearance] setTitleLabelFont:font];
        }
        else {
            NSLog(@"ERROR: _zendeskSetFont:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKRMAFeedbackView"]) {
        if ([property isEqualToString:@"subheaderFont"]) {
            [[ZDKRMAFeedbackView appearance] setSubheaderFont:font];
        }
        else if ([property isEqualToString:@"headerFont"]) {
            [[ZDKRMAFeedbackView appearance] setHeaderFont:font];
        }
        else if ([property isEqualToString:@"textEntryFont"]) {
            [[ZDKRMAFeedbackView appearance] setTextEntryFont:font];
        }
        else if ([property isEqualToString:@"buttonFont"]) {
            [[ZDKRMAFeedbackView appearance] setButtonFont:font];
        }
        else {
            NSLog(@"ERROR: _zendeskSetFont:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKRMADialogView"]) {
        if ([property isEqualToString:@"buttonFont"]) {
            [[ZDKRMADialogView appearance] setButtonFont:font];
        }
        else if ([property isEqualToString:@"headerFont"]) {
            [[ZDKRMADialogView appearance] setHeaderFont:font];
        }
        else {
            NSLog(@"ERROR: _zendeskSetFont:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKRequestListTableCell"]) {
        if ([property isEqualToString:@"descriptionFont"]) {
            [[ZDKRequestListTableCell appearance] setDescriptionFont:font];
        }
        else if ([property isEqualToString:@"createdAtFont"]) {
            [[ZDKRequestListTableCell appearance] setCreatedAtFont:font];
        }
        else {
            NSLog(@"ERROR: _zendeskSetFont:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKCreateRequestView"]) {
        if ([property isEqualToString:@"textEntryFont"]) {
            [[ZDKCreateRequestView appearance] setTextEntryFont:font];
        }
        else {
            NSLog(@"ERROR: _zendeskSetFont:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKEndUserCommentTableCell"]) {
        if ([property isEqualToString:@"bodyFont"]) {
            [[ZDKEndUserCommentTableCell appearance] setBodyFont:font];
        }
        else if ([property isEqualToString:@"timestampFont"]) {
            [[ZDKEndUserCommentTableCell appearance] setTimestampFont:font];
        }
        else {
            NSLog(@"ERROR: _zendeskSetFont:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else {
        NSLog(@"ERROR: _zendeskSetFont - Unrecognized className %@ and property %@", class, property);
    }
}

// TODO: Limit setting a UITableCell's separatorInset to >= NS_AVAILABLE_IOS(7_0) ?
// test if causes issues on < 7_0.
void _zendeskSetEdgeInsets(char *className, char *propertyName, float top, float left, float bottom, float right) {
    NSString *class = GetStringParam(className);
    NSString *property = GetStringParam(propertyName);
    UIEdgeInsets insets = UIEdgeInsetsMake(top, left, bottom, right);
    if ([class isEqualToString:@"ZDKAgentCommentTableCell"]) {
        if ([property isEqualToString:@"separatorInset"]) {
            [[ZDKAgentCommentTableCell appearance] setSeparatorInset:insets];
        }
        else {
            NSLog(@"ERROR: _zendeskSetEdgeInsets:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKCommentsListLoadingTableCell"]) {
        if ([property isEqualToString:@"separatorInset"]) {
            [[ZDKCommentsListLoadingTableCell appearance] setSeparatorInset:insets];
        }
        else {
            NSLog(@"ERROR: _zendeskSetEdgeInsets:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKRequestListLoadingTableCell"]) {
        if ([property isEqualToString:@"separatorInset"]) {
            [[ZDRequestListLoadingTableCell appearance] setSeparatorInset:insets];
        }
        else {
            NSLog(@"ERROR: _zendeskSetEdgeInsets:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKRequestListEmptyTableCell"]) {
        if ([property isEqualToString:@"separatorInset"]) {
            [[ZDRequestListEmptyTableCell appearance] setSeparatorInset:insets];
        }
        else {
            NSLog(@"ERROR: _zendeskSetEdgeInsets:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKSupportTableViewCell"]) {
        if ([property isEqualToString:@"separatorInset"]) {
            [[ZDKSupportTableViewCell appearance] setSeparatorInset:insets];
        }
        else {
            NSLog(@"ERROR: _zendeskSetEdgeInsets:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKSupportAttachmentCell"]) {
        if ([property isEqualToString:@"separatorInset"]) {
            [[ZDKSupportAttachmentCell appearance] setSeparatorInset:insets];
        }
        else {
            NSLog(@"ERROR: _zendeskSetEdgeInsets:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKSupportArticleTableViewCell"]) {
        if ([property isEqualToString:@"separatorInset"]) {
            [[ZDKSupportArticleTableViewCell appearance] setSeparatorInset:insets];
        }
        else {
            NSLog(@"ERROR: _zendeskSetEdgeInsets:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKRequestCommentTableCell"]) {
        if ([property isEqualToString:@"separatorInset"]) {
            [[ZDKRequestCommentTableCell appearance] setSeparatorInset:insets];
        }
        else {
            NSLog(@"ERROR: _zendeskSetEdgeInsets:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKRequestListTableCell"]) {
        if ([property isEqualToString:@"separatorInset"]) {
            [[ZDKRequestListTableCell appearance] setSeparatorInset:insets];
        }
        else {
            NSLog(@"ERROR: _zendeskSetEdgeInsets:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKRequestListTable"]) {
        if ([property isEqualToString:@"separatorInset"]) {
            [[ZDKRequestListTable appearance] setSeparatorInset:insets];
        }
        else {
            NSLog(@"ERROR: _zendeskSetEdgeInsets:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKSupportView"]) {
        if ([property isEqualToString:@"noResultsContactButtonEdgeInsets"]) {
            [[ZDKSupportView appearance] setNoResultsContactButtonEdgeInsets:[NSValue valueWithUIEdgeInsets:insets]];
        }
        else {
            NSLog(@"ERROR: _zendeskSetEdgeInsets:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKEndUserCommentTableCell"]) {
        if ([property isEqualToString:@"separatorInset"]) {
            [[ZDKEndUserCommentTableCell appearance] setSeparatorInset:insets];
        }
        else {
            NSLog(@"ERROR: _zendeskSetEdgeInsets:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKRequestCommentAttachmentTableCell"]) {
        if ([property isEqualToString:@"separatorInset"]) {
            [[ZDKRequestCommentAttachmentTableCell appearance] setSeparatorInset:insets];
        }
        else {
            NSLog(@"ERROR: _zendeskSetEdgeInsets:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKRequestCommentAttachmentLoadingTableCell"]) {
        if ([property isEqualToString:@"separatorInset"]) {
            [[ZDKRequestCommentAttachmentLoadingTableCell appearance] setSeparatorInset:insets];
        }
        else {
            NSLog(@"ERROR: _zendeskSetEdgeInsets:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else {
        NSLog(@"ERROR: _zendeskSetEdgeInsets - Unrecognized className %@ and property %@", class, property);
    }
}

void _zendeskSetBorderWidth(char * className, char * propertyName, float width) {
    NSString *class = GetStringParam(className);
    NSString *property = GetStringParam(propertyName);
    if ([class isEqualToString:@"ZDKCreateRequestView"]) {
        if ([property isEqualToString:@"attachmentButtonBorderWidth"]) {
            [[ZDKCreateRequestView appearance] setAttachmentButtonBorderWidth:@(width)];
        }
        else {
            NSLog(@"ERROR: _zendeskSetBorderWidth:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKSupportView"]) {
        if ([property isEqualToString:@"noResultsContactButtonBorderWidth"]) {
            [[ZDKSupportView appearance] setNoResultsContactButtonBorderWidth:@(width)];
        }
        else {
            NSLog(@"ERROR: _zendeskSetBorderWidth:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else {
        NSLog(@"ERROR: _zendeskSetBorderWidth - Unrecognized className %@ and property %@", class, property);
    }
}

void _zendeskSetCornerRadius(char * className, char * propertyName, float radius) {
    NSString *class = GetStringParam(className);
    NSString *property = GetStringParam(propertyName);
    if ([class isEqualToString:@"ZDKCreateRequestView"]) {
        if ([property isEqualToString:@"attachmentButtonCornerRadius"]) {
            [[ZDKCreateRequestView appearance] setAttachmentButtonCornerRadius:@(radius)];
        }
        else {
            NSLog(@"ERROR: _zendeskSetCornerRadius:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else if ([class isEqualToString:@"ZDKSupportView"]) {
        if ([property isEqualToString:@"noResultsContactButtonCornerRadius"]) {
            [[ZDKSupportView appearance] setNoResultsContactButtonCornerRadius:@(radius)];
        }
        else {
            NSLog(@"ERROR: _zendeskSetBorderWidth:%@ - Unrecognized propertyName %@", class, property);
        }
    }
    else {
        NSLog(@"ERROR: _zendeskSetCornerRadius - Unrecognized className %@ and property %@", class, property);
    }
}

void _zendeskSetAutomaticallyHideNavBarOnLandscape(char * className, int enabled) {
    NSString *class = GetStringParam(className);
    if ([class isEqualToString:@"ZDKSupportView"]) {
        [[ZDKSupportView appearance] setAutomaticallyHideNavBarOnLandscape:@((NSInteger)enabled)];
    }
    else if ([class isEqualToString:@"ZDKCreateRequestView"]) {
        [[ZDKCreateRequestView appearance] setAutomaticallyHideNavBarOnLandscape:@((NSInteger)enabled)];
    }
    else {
        NSLog(@"ERROR: _zendeskSetAutomaticallyHideNavBarOnLandscape - Unrecognized className %@", class);
    }
}

void _zendeskSetAvatarSize(char * className, float size) {
    NSString *class = GetStringParam(className);
    if ([class isEqualToString:@"ZDKAgentCommentTableCell"]) {
        [[ZDKAgentCommentTableCell appearance] setAvatarSize:@(size)];
    }
    else {
        NSLog(@"ERROR: _zendeskSetAvatarSize - Unrecognized className %@", class);
    }
}

void _zendeskSetVerticalMargin(char * className, float size) {
    NSString *class = GetStringParam(className);
    if ([class isEqualToString:@"ZDKRequestListTableCell"]) {
        [[ZDKRequestListTableCell appearance] setVerticalMargin:@(size)];
    }
    else {
        NSLog(@"ERROR: _zendeskSetVerticalMargin - Unrecognized className %@", class);
    }
}

void _zendeskSetDescriptionTimestampMargin(char * className, float size) {
    NSString *class = GetStringParam(className);
    if ([class isEqualToString:@"ZDKRequestListTableCell"]) {
        [[ZDKRequestListTableCell appearance] setDescriptionTimestampMargin:@(size)];
    }
    else {
        NSLog(@"ERROR: _zendeskSetDescriptionTimestampMargin - Unrecognized className %@", class);
    }
}

void _zendeskSetLeftInset(char * className, float size) {
    NSString *class = GetStringParam(className);
    if ([class isEqualToString:@"ZDKRequestListTableCell"]) {
        [[ZDKRequestListTableCell appearance] setLeftInset:@(size)];
    }
    else if ([class isEqualToString:@"ZDKCommentsListLoadingTableCell"]) {
        [[ZDKCommentsListLoadingTableCell appearance] setLeftInset:@(size)];
    }
    else {
        NSLog(@"ERROR: _zendeskSetLeftInset - Unrecognized className %@", class);
    }
}

void _zendeskSetSearchBarStyle(char * className, int style) {
    NSString *class = GetStringParam(className);
    if ([class isEqualToString:@"ZDKSupportView"]) {
        [[ZDKSupportView appearance] setSearchBarStyle:@(style)];
    }
    else {
        NSLog(@"ERROR: _zendeskSetSearchBarStyle - Unrecognized className %@", class);
    }
}

void _zendeskSetAttachmentActionSheetStyle(char * className, int style) {
    NSString *class = GetStringParam(className);
    if ([class isEqualToString:@"ZDKCreateRequestView"]) {
        [[ZDKCreateRequestView appearance] setAttachmentActionSheetStyle:@(style)];
    }
    else {
        NSLog(@"ERROR: _zendeskSetAttachmentActionSheetStyle - Unrecognized className %@", class);
    }
}

// TODO: need to init spinner or just set style on it?
void _zendeskSetSpinnerUIActivityIndicatorViewStyle(char * className, int style) {
    NSString *class = GetStringParam(className);
    UIActivityIndicatorView * spinner = [[UIActivityIndicatorView alloc] initWithFrame:CGRectMake(0, 0, 20, 20)];
    spinner.activityIndicatorViewStyle = style;
    if ([class isEqualToString:@"ZDKSupportView"]) {
        [[ZDKSupportView appearance] setSpinner:(id<ZDKSpinnerDelegate>)spinner];
    }
    else if ([class isEqualToString:@"ZDKCreateRequestView"]) {
        [[ZDKCreateRequestView appearance] setSpinner:(id<ZDKSpinnerDelegate>)spinner];
    }
    else if ([class isEqualToString:@"ZDKRMAFeedbackView"]) {
        [[ZDKRMAFeedbackView appearance] setSpinner:(id<ZDKSpinnerDelegate>)spinner];
    }
    else {
        NSLog(@"ERROR: _zendeskSetSpinnerUIActivityIndicatorViewStyle - Unrecognized className %@", class);
    }
}

void _zendeskSetAttachmentButtonImage(char * className, char * imageName, char * type) {
    NSString *class = GetStringParam(className);
    if ([class isEqualToString:@"ZDKCreateRequestView"]) {
        [[ZDKCreateRequestView appearance] setAttachmentButtonImage:[ZDKBundleUtils imageNamed:GetStringParam(imageName) ofType:GetStringParam(type)]];
    }
    else {
        NSLog(@"ERROR: _zendeskSetAttachmentButtonImage - Unrecognized className %@", class);
    }
}
