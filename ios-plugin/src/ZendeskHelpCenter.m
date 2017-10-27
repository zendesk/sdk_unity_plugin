//
//  ZendeskHelpCenter.m
//  ZEN
//

#import "ZendeskCore.h"
#import "ZDKHelpCenterDelegate.h"

#pragma mark - ZDKHelpCenter

ZDKHelpCenterDelegate *delegate;

void _zendeskHelpCenterShowHelpCenter() {
    [ZDKHelpCenter setNavBarConversationsUIType: ZDKNavBarConversationsUITypeLocalizedLabel];
    ZendeskModalNavigationController *modalNavController = [[ZendeskModalNavigationController alloc] init];
    ZDKHelpCenterOverviewContentModel *defaultModel = [ZDKHelpCenterOverviewContentModel defaultContent];
    [ZDKHelpCenter pushHelpCenterOverview:modalNavController withContentModel:defaultModel];

    UIViewController *rootViewController = [modalNavController.viewControllers firstObject];
    rootViewController.navigationItem.leftBarButtonItem = [[UIBarButtonItem alloc] initWithTitle:@"Close"
                                                                                           style:UIBarButtonItemStyleBordered
                                                                                          target:modalNavController
                                                                                          action:@selector(close:)];

    [UnityGetGLViewController() presentViewController:modalNavController animated:YES completion:nil];
}


void _zendeskHelpCenterShowHelpCenterWithOptions(char* labels[], int labelsLength, BOOL includeAll,
                                      BOOL includeCategories, BOOL includeSections, char* ids[],
                                      int idsLength, int hideContactSupport, BOOL articleVoting) {

    ZDKHelpCenterOverviewContentModel *defaultModel = [ZDKHelpCenterOverviewContentModel defaultContent];

    // Populate labels
    if (labels && labelsLength > 0) {
        NSMutableArray *labelsArray = [NSMutableArray new];

        for (int i = 0; i < labelsLength; i++) {
            [labelsArray addObject:GetStringParam(labels[i])];
        }

        defaultModel.labels = labelsArray;
    }

    if (includeAll) {
        defaultModel.groupType = ZDKHelpCenterOverviewGroupTypeDefault;
    } else if (includeCategories) {
        defaultModel.groupType = ZDKHelpCenterOverviewGroupTypeCategory;
    } else if (includeSections) {
        defaultModel.groupType = ZDKHelpCenterOverviewGroupTypeSection;
    }

    if (ids && idsLength > 0) {
        NSMutableArray *idsArray = [NSMutableArray new];

        for (int i = 0; i < idsLength; i++) {
            [idsArray addObject:GetStringParam(ids[i])];
        }

        defaultModel.groupIds = idsArray;
    }
    
    [ZDKConfig instance].articleVotingEnabled = articleVoting;
    
    if (hideContactSupport == 0) {
        defaultModel.hideContactSupport = YES;
    } else {
        defaultModel.hideContactSupport = NO;
    }
    delegate = [[ZDKHelpCenterDelegate alloc] init];
    delegate.whereActive = hideContactSupport;
    [ZDKHelpCenter setUIDelegate:delegate];
    
    ZendeskModalNavigationController *modalNavController = [[ZendeskModalNavigationController alloc] init];
    [ZDKHelpCenter pushHelpCenterOverview:modalNavController withContentModel:defaultModel];

    UIViewController *rootViewController = [modalNavController.viewControllers firstObject];
    rootViewController.navigationItem.leftBarButtonItem = [[UIBarButtonItem alloc] initWithTitle:@"Close"
                                                                                           style:UIBarButtonItemStyleBordered
                                                                                          target:modalNavController
                                                                                          action:@selector(close:)];
    [UnityGetGLViewController() presentViewController:modalNavController animated:YES completion:nil];
}


void _zendeskHelpCenterViewArticle(char * articleId){
    NSString * articleIdString = GetStringParam(articleId);

    
    ZDKHelpCenterArticleViewModel *newArticle = [[ZDKHelpCenterArticleViewModel alloc]init];
    newArticle.articleId = articleIdString;
    
    ZDKArticleViewController *rootViewController = [[ZDKArticleViewController alloc] initWithArticleViewModel:newArticle];
    
    ZendeskModalNavigationController *navController = [[ZendeskModalNavigationController alloc] initWithRootViewController:rootViewController];
    rootViewController.navigationItem.leftBarButtonItem = [[UIBarButtonItem alloc] initWithTitle:@"Close"
                                                                                           style:UIBarButtonItemStyleBordered
                                                                                          target:navController
                                                                                          action:@selector(close:)];
    [UnityGetGLViewController() presentViewController:navController animated:YES completion:nil];
}

void _zendeskHelpCenterConfigureZDKRequests(char* requestSubject, char* tags[], int tagsLength, char* additionalInfo) {

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
