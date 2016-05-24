//
//  ZendeskBinding.m
//  ZEN
//

#import "ZendeskCore.h"

#pragma mark - ZDKHelpCenterProvider

void _zendeskHelpCenterProviderGetCategories(char * gameObjectName, char * callbackId) {
    ZDKHelpCenterProvider *provider = [ZDKHelpCenterProvider new];
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKHelpCenterCategoriesToJSON:result], "didHelpCenterProviderGetCategories")
    [provider getCategoriesWithCallback:callback];
}

void _zendeskHelpCenterProviderGetSectionsForCategory(char * gameObjectName, char * callbackId, char * categoryId) {
    ZDKHelpCenterProvider *provider = [ZDKHelpCenterProvider new];
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKHelpCenterSectionsToJSON:result], "didHelpCenterProviderGetSectionsForCategoryId")
    [provider getSectionsForCategoryId:GetStringParam(categoryId)
                          withCallback:callback];
}

void _zendeskHelpCenterProviderGetArticlesForSection(char * gameObjectName, char * callbackId, char * sectionId) {
    ZDKHelpCenterProvider *provider = [ZDKHelpCenterProvider new];
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKHelpCenterArticlesToJSON:result], "didHelpCenterProviderGetArticlesForSectionId")
    [provider getArticlesForSectionId:GetStringParam(sectionId)
                         withCallback:callback];
}

void _zendeskHelpCenterProviderSearchArticlesUsingQuery(char * gameObjectName, char * callbackId, char * query) {
    ZDKHelpCenterProvider *provider = [ZDKHelpCenterProvider new];
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKHelpCenterArticlesToJSON:result], "didHelpCenterProviderSearchForArticlesUsingQuery")
    [provider searchForArticlesUsingQuery:GetStringParam(query)
                             withCallback:callback];
}

void _zendeskHelpCenterProviderSearchArticlesUsingQueryAndLabels(char * gameObjectName, char * callbackId, char * query, char * labelsArray[], int labelsLength) {
    NSMutableArray * labels = @[].mutableCopy;
    for (int i = 0; i < labelsLength; i++) {
        [labels addObject:GetStringParam(labelsArray[i])];
    }
    ZDKHelpCenterProvider *provider = [ZDKHelpCenterProvider new];
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKHelpCenterArticlesToJSON:result], "didHelpCenterProviderSearchForArticlesUsingQueryAndLabels")
    [provider searchForArticlesUsingQuery:GetStringParam(query)
                                andLabels:labels
                             withCallback:callback];
}

void _zendeskHelpCenterProviderSearchArticlesUsingHelpCenterSearch(char * gameObjectName,
                                                                      char * callbackId, 
                                                                      char * query, 
                                                                      char * labelNames[], 
                                                                      int labelNamesLength, 
                                                                      char * locale,
                                                                      char * sideLoads[],
                                                                      int sideLoadsLength,
                                                                      int categoryId,
                                                                      int sectionId,
                                                                      int page,
                                                                      int resultsPerPage) {
    ZDKHelpCenterProvider *provider = [ZDKHelpCenterProvider new];
    ZDKHelpCenterSearch *helpCenterSearch = [ZDKHelpCenterSearch new];
    if (query != nil) {
          helpCenterSearch.query = GetStringParam(query);
    }
    if (labelNamesLength > 0 && labelNames != nil) {
          NSMutableArray * labelNamesArray = @[].mutableCopy;
          for (int i = 0; i < labelNamesLength; i++) {
                [labelNamesArray addObject:GetStringParam(labelNames[i])];
          }
          helpCenterSearch.labelNames = labelNamesArray;
    }
    if (locale != nil) {
          helpCenterSearch.locale = GetStringParam(locale);
    }
    if (sideLoads > 0 && sideLoads != nil) {
          NSMutableArray * sideLoadsArray = @[].mutableCopy;
          for (int i = 0; i < sideLoadsLength; i++) {
                [sideLoadsArray addObject:GetStringParam(sideLoads[i])];
          }
          helpCenterSearch.sideLoads = sideLoadsArray;
    }
    if (categoryId > -1) {
          helpCenterSearch.categoryId = [NSNumber numberWithInt:categoryId];
    }
    if (sectionId > -1) {
          helpCenterSearch.sectionId = [NSNumber numberWithInt:sectionId];
    }
    if (page > -1) {
          helpCenterSearch.page = [NSNumber numberWithInt:page];
    }
    if (resultsPerPage > -1) {
          helpCenterSearch.resultsPerPage = [NSNumber numberWithInt:resultsPerPage];
    }
    
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKHelpCenterArticlesToJSON:result], "didHelpCenterProviderSearchForArticlesUsingHelpCenterSearch")
    [provider searchArticles:helpCenterSearch
                withCallback:callback];
}

void _zendeskHelpCenterProviderGetAttachmentsForArticle(char * gameObjectName, char * callbackId, char * articleId, char * attachmentType) {
    ZDKHelpCenterProvider *provider = [ZDKHelpCenterProvider new];
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKHelpCenterArticlesToJSON:result], "didHelpCenterProviderGetAttachmentsForArticleId")
    [provider getAttachmentForArticleId:GetStringParam(articleId)
                           withCallback:callback];
}

void _zendeskHelpCenterProviderGetArticles(char * gameObjectName, char * callbackId, char * labelsArray[], int labelsLength,
                                           char * include, char * locale, int page, int resultsPerPage, int sortBy, int sortOrder) {
    NSMutableArray * labels = @[].mutableCopy;
    for (int i = 0; i < labelsLength; i++) {
        [labels addObject:GetStringParam(labelsArray[i])];
    }
    ZDKHelpCenterProvider *provider = [ZDKHelpCenterProvider new];
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKHelpCenterArticlesToJSON:result], "didHelpCenterGetArticlesByLabels")
    [provider getArticlesByLabels:labels
                     withCallback:callback];
}

void _zendeskHelpCenterProviderGetFlatArticles(char * gameObjectName, char * callbackId) {
    ZDKHelpCenterProvider *provider = [ZDKHelpCenterProvider new];
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKHelpCenterFlatArticlesToJSON:result], "didHelpCenterGetFlatArticlesByLabels")
    [provider getFlatArticlesWithCallback:callback];
}

void _zendeskHelpCenterProviderGetSuggestedArticles(char * gameObjectName,
                                                    char * callbackId,
                                                    char * query,
                                                    char * labelNames[],
                                                    int labelNamesLength,
                                                    char * locale,
                                                    int categoryId,
                                                    int sectionId) {
    ZDKHelpCenterProvider *provider = [ZDKHelpCenterProvider new];
    ZDKHelpCenterDeflection *helpCenterSearch = [ZDKHelpCenterDeflection new];
    if (query != nil) {
        helpCenterSearch.query = GetStringParam(query);
    }
    if (labelNamesLength > 0 && labelNames != nil) {
        NSMutableArray * labelNamesArray = @[].mutableCopy;
        for (int i = 0; i < labelNamesLength; i++) {
            [labelNamesArray addObject:GetStringParam(labelNames[i])];
        }
        helpCenterSearch.labelNames = labelNamesArray;
    }
    if (locale != nil) {
        helpCenterSearch.locale = GetStringParam(locale);
    }
    if (categoryId > -1) {
        helpCenterSearch.categoryId = [NSNumber numberWithInt:categoryId];
    }
    if (sectionId > -1) {
        helpCenterSearch.sectionId = [NSNumber numberWithInt:sectionId];
    }
    
    ZDKDefCallback(id, [ZendeskJSON serializeJSONObject:result], "didHelpCenterGetSuggestedArticles")
    [provider getSuggestedArticles: helpCenterSearch
                withCallback:callback];
}

void _zendeskHelpCenterProviderGetArticle(char * gameObjectName, char * callbackId, char * articleId) {
    ZDKHelpCenterProvider *provider = [ZDKHelpCenterProvider new];
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKHelpCenterArticlesToJSON:result], "didHelpCenterGetArticle")
    [provider getArticleById:GetStringParam(articleId) withCallback:callback];
}

void _zendeskHelpCenterProviderGetSection(char * gameObjectName, char * callbackId, char * sectionId) {
    ZDKHelpCenterProvider *provider = [ZDKHelpCenterProvider new];
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKHelpCenterSectionsToJSON:result], "didHelpCenterGetSection")
    [provider getSectionById:GetStringParam(sectionId) withCallback:callback];
}

void _zendeskHelpCenterProviderGetCategory(char * gameObjectName, char * callbackId, char * categoryId) {
    ZDKHelpCenterProvider *provider = [ZDKHelpCenterProvider new];
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKHelpCenterCategoriesToJSON:result], "didHelpCenterGetCategory")
    [provider getCategoryById:GetStringParam(categoryId) withCallback:callback];
}

void _zendeskHelpCenterProviderUpvoteArticle(char * gameObjectName, char * callbackId, char * articleId) {
    ZDKHelpCenterProvider *provider = [ZDKHelpCenterProvider new];
    ZDKArrayCallback([ZendeskJSON ZDKVoteResponseToJSON:(ZDKHelpCenterArticleVote*)result], "didHelpCenterUpvoteArticle")
    [provider upvoteArticleWithId:GetStringParam(articleId) withCallback:callback];
}

void _zendeskHelpCenterProviderDownvoteArticle(char * gameObjectName, char * callbackId, char * articleId) {
    ZDKHelpCenterProvider *provider = [ZDKHelpCenterProvider new];
    ZDKArrayCallback([ZendeskJSON ZDKVoteResponseToJSON:(ZDKHelpCenterArticleVote*)result], "didHelpCenterDownvoteArticle")
    [provider downvoteArticleWithId:GetStringParam(articleId) withCallback:callback];
}

void _zendeskHelpCenterProviderDeleteVote(char * gameObjectName, char * callbackId, char * voteId) {
    ZDKHelpCenterProvider *provider = [ZDKHelpCenterProvider new];
    ZDKDefCallback(id, [ZendeskJSON ZDKGenericResponseToJSON:result], "didHelpCenterDeleteVote")
    [provider deleteVoteWithId:GetStringParam(voteId) withCallback:callback];
}

void _zendeskHelpCenterProviderSubmitRecordArticleView(char * gameObjectName, char * callbackId, char * articleId, char * locale) {
    ZDKHelpCenterProvider *provider = [ZDKHelpCenterProvider new];
    ZDKDefCallback(id, [ZendeskJSON ZDKGenericResponseToJSON:result], "didHelpCenterSubmitRecordArticleView")
    [provider submitRecordArticleView:GetStringParam(articleId) locale:GetStringParam(locale) withCallback:callback];
}
