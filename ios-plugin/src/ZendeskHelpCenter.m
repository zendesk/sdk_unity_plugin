//
//  ZendeskHelpCenter.m
//  ZEN
//

#import "ZendeskCore.h"

#pragma mark - ZDKHelpCenter

void _zendeskHelpCenterShowHelpCenter() {
    [ZDKHelpCenter setNavBarConversationsUIType: ZDKNavBarConversationsUITypeLocalizedLabel];
    ZendeskModalNavigationController *modalNavController = [[ZendeskModalNavigationController alloc] init];
    [ZDKHelpCenter showHelpCenterWithNavController:modalNavController];
    UIViewController *rootViewController = [modalNavController.viewControllers firstObject];
    rootViewController.navigationItem.leftBarButtonItem = [[UIBarButtonItem alloc] initWithTitle:@"Close"
                                                                                           style:UIBarButtonItemStyleBordered
                                                                                          target:modalNavController
                                                                                          action:@selector(close:)];
    [UnityGetGLViewController() presentViewController:modalNavController animated:YES completion:nil];
}

void _zendeskHelpCenterShowHelpCenterWithOptions(BOOL listCats, int listSections, int listArticles, char *listLabels[], int listLabelsLength, BOOL showContactUsButton, char *requestSubject) {
    [ZDKHelpCenter setNavBarConversationsUIType: showContactUsButton ? ZDKNavBarConversationsUITypeLocalizedLabel : ZDKNavBarConversationsUITypeNone];
    
    ZendeskModalNavigationController *modalNavController = [[ZendeskModalNavigationController alloc] init];
    
    if (listSections != 0) {
        [ZDKHelpCenter showHelpCenterWithNavController:modalNavController filterByCategoryId:@(listSections).stringValue categoryName:nil layoutGuide:ZDKLayoutRespectTop];
    } else if (listArticles != 0) {
        [ZDKHelpCenter showHelpCenterWithNavController:modalNavController filterBySectionId:@(listArticles).stringValue sectionName:nil layoutGuide:ZDKLayoutRespectTop];
    } else if (listLabelsLength > 0) {
        NSMutableArray * newStrings = @[].mutableCopy;
        for (int i = 0; i < listLabelsLength; i++) {
            [newStrings addObject:GetStringParam(listLabels[i])];
        }
        [ZDKHelpCenter showHelpCenterWithNavController:modalNavController filterByArticleLabels:[newStrings copy]];
    } else { // if (listCats)
        [ZDKHelpCenter showHelpCenterWithNavController:modalNavController];
    }
    
    UIViewController *rootViewController = [modalNavController.viewControllers firstObject];
    rootViewController.navigationItem.leftBarButtonItem = [[UIBarButtonItem alloc] initWithTitle:@"Close"
                                                                                           style:UIBarButtonItemStyleBordered
                                                                                          target:modalNavController
                                                                                          action:@selector(close:)];
    [UnityGetGLViewController() presentViewController:modalNavController animated:YES completion:nil];
}

void _zendeskHelpCenterShowHelpCenterFilterByArticleLabels(char *charArray[], int length) {
    NSMutableArray * newStrings = @[].mutableCopy;
    for (int i = 0; i < length; i++) {
        [newStrings addObject:GetStringParam(charArray[i])];
    }
    ZendeskModalNavigationController *modalNavController = [[ZendeskModalNavigationController alloc] init];
    [ZDKHelpCenter showHelpCenterWithNavController:modalNavController filterByArticleLabels:[newStrings copy]];
    UIViewController *rootViewController = [modalNavController.viewControllers firstObject];
    rootViewController.navigationItem.leftBarButtonItem = [[UIBarButtonItem alloc] initWithTitle:@"Close"
                                                                                           style:UIBarButtonItemStyleBordered
                                                                                          target:modalNavController
                                                                                          action:@selector(close:)];
    [UnityGetGLViewController() presentViewController:modalNavController animated:YES completion:nil];
}

void _zendeskHelpCenterViewArticle(char * jsonData){
    NSString * jsonString = GetStringParam(jsonData);
    NSData *objectData = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
    NSDictionary *json = [NSJSONSerialization JSONObjectWithData:objectData
                                                         options:0
                                                           error:nil];
    
    ZDKHelpCenterArticle *newArticle = [[ZDKHelpCenterArticle alloc]init];
    [newArticle setSid: [json objectForKey:@"sid"]];
    [newArticle setSection_id:[json objectForKey:@"section_id"]];
    [newArticle setTitle:[json objectForKey:@"title"]];
    [newArticle setBody:[json objectForKey:@"body"]];
    [newArticle setAuthor_name:[json objectForKey:@"author_name"]];
    [newArticle setArticle_details:[json objectForKey:@"article_details"]];
    NSDate* date = [NSDate date];
    [newArticle setCreated_at:date];
    
    ZDKArticleViewController *rootViewController = [[ZDKArticleViewController alloc] init];
    rootViewController = [rootViewController initWithArticle:newArticle];
    
    ZendeskModalNavigationController *navController = [[ZendeskModalNavigationController alloc] initWithRootViewController:rootViewController];
    rootViewController.navigationItem.leftBarButtonItem = [[UIBarButtonItem alloc] initWithTitle:@"Close"
                                                                                           style:UIBarButtonItemStyleBordered
                                                                                          target:navController
                                                                                          action:@selector(close:)];
    [UnityGetGLViewController() presentViewController:navController animated:YES completion:nil];
}
