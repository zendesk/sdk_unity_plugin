//
//  ZendeskBinding.m
//  ZEN
//

#import "ZendeskCore.h"

#pragma mark - ZDKHelpCenterProvider

NSString* locale();

void _zendeskHelpCenterProviderGetCategories(char * gameObjectName, char * callbackId) {
    ZDKHelpCenterProvider *provider = [[ZDKHelpCenterProvider alloc] initWithLocale:locale()];
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKHelpCenterCategoriesToJSON:result], "didHelpCenterProviderGetCategories")
    [provider getCategoriesWithCallback:callback];
}

void _zendeskHelpCenterProviderGetSectionsForCategory(char * gameObjectName, char * callbackId, char * categoryId) {
    ZDKHelpCenterProvider *provider = [[ZDKHelpCenterProvider alloc] initWithLocale:locale()];
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKHelpCenterSectionsToJSON:result], "didHelpCenterProviderGetSectionsForCategoryId")
    [provider getSectionsForCategoryId:GetStringParam(categoryId)
                          withCallback:callback];
}

void _zendeskHelpCenterProviderGetArticlesForSection(char * gameObjectName, char * callbackId, char * sectionId) {
    ZDKHelpCenterProvider *provider = [[ZDKHelpCenterProvider alloc] initWithLocale:locale()];
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKHelpCenterArticlesToJSON:result], "didHelpCenterProviderGetArticlesForSectionId")
    [provider getArticlesForSectionId:GetStringParam(sectionId)
                         withCallback:callback];
}

void _zendeskHelpCenterProviderSearchArticlesUsingQuery(char * gameObjectName, char * callbackId, char * query) {
    ZDKHelpCenterProvider *provider = [[ZDKHelpCenterProvider alloc] initWithLocale:locale()];
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKHelpCenterArticlesToJSON:result], "didHelpCenterProviderSearchForArticlesUsingQuery")
    [provider searchForArticlesUsingQuery:GetStringParam(query)
                             withCallback:callback];
}

void _zendeskHelpCenterProviderSearchArticlesUsingQueryAndLabels(char * gameObjectName, char * callbackId, char * query, char * labelsArray[], int labelsLength) {
    NSMutableArray * labels = @[].mutableCopy;
    for (int i = 0; i < labelsLength; i++) {
        [labels addObject:GetStringParam(labelsArray[i])];
    }
    ZDKHelpCenterProvider *provider = [[ZDKHelpCenterProvider alloc] initWithLocale:locale()];
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
                                                                   char * categoryIds[],
                                                                   int categoryIdsLength,
                                                                   char * sectionIds[],
                                                                   int sectionIdsLength,
                                                                   int page,
                                                                   int resultsPerPage) {
    ZDKHelpCenterProvider *provider = [[ZDKHelpCenterProvider alloc] initWithLocale:GetStringParam(locale)];
    ZDKHelpCenterSearch *helpCenterSearch = [ZDKHelpCenterSearch new];

    if (query) {
        helpCenterSearch.query = GetStringParam(query);
    }

    if (labelNames && labelNamesLength > 0) {
        NSMutableArray * labelNamesArray = @[].mutableCopy;
        for (int i = 0; i < labelNamesLength; i++) {
            [labelNamesArray addObject:GetStringParam(labelNames[i])];
        }
        helpCenterSearch.labelNames = labelNamesArray;
    }

    if (locale) {
        helpCenterSearch.locale = GetStringParam(locale);
    }

    if (sideLoads && sideLoadsLength > 0) {
        NSMutableArray * sideLoadsArray = [NSMutableArray new];
        for (int i = 0; i < sideLoadsLength; i++) {
            [sideLoadsArray addObject:GetStringParam(sideLoads[i])];
        }
        helpCenterSearch.sideLoads = sideLoadsArray;
    }

    if (categoryIds && categoryIdsLength > 0) {
        NSMutableArray *categoryIdsArray = [NSMutableArray new];
        for (int i = 0; i < categoryIdsLength; i++) {
            [categoryIdsArray addObject:GetStringParam(categoryIds[i])];
        }

        helpCenterSearch.categoryIds = categoryIdsArray;
    }

    if (sectionIds && sectionIdsLength > 0) {
        NSMutableArray *sectionIdsArray = [NSMutableArray new];
        for (int i = 0; i < sectionIdsLength; i++) {
            [sectionIdsArray addObject:GetStringParam(sectionIds[i])];
        }

        helpCenterSearch.sectionIds = sectionIdsArray;
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
    ZDKHelpCenterProvider *provider = [[ZDKHelpCenterProvider alloc] initWithLocale:locale()];
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
    ZDKHelpCenterProvider *provider = [[ZDKHelpCenterProvider alloc] initWithLocale:GetStringParam(locale)];
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKHelpCenterArticlesToJSON:result], "didHelpCenterGetArticlesByLabels")
    [provider getArticlesByLabels:labels
                     withCallback:callback];
}

void _zendeskHelpCenterProviderGetFlatArticles(char * gameObjectName, char * callbackId) {
    ZDKHelpCenterProvider *provider = [[ZDKHelpCenterProvider alloc] initWithLocale:locale()];
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
    ZDKHelpCenterProvider *provider = [[ZDKHelpCenterProvider alloc] initWithLocale:GetStringParam(locale)];
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
    ZDKHelpCenterProvider *provider = [[ZDKHelpCenterProvider alloc] initWithLocale:locale()];
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKHelpCenterArticlesToJSON:result], "didHelpCenterGetArticle")
    [provider getArticleById:GetStringParam(articleId) withCallback:callback];
}

void _zendeskHelpCenterProviderGetSection(char * gameObjectName, char * callbackId, char * sectionId) {
    ZDKHelpCenterProvider *provider = [[ZDKHelpCenterProvider alloc] initWithLocale:locale()];
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKHelpCenterSectionsToJSON:result], "didHelpCenterGetSection")
    [provider getSectionById:GetStringParam(sectionId) withCallback:callback];
}

void _zendeskHelpCenterProviderGetCategory(char * gameObjectName, char * callbackId, char * categoryId) {
    ZDKHelpCenterProvider *provider = [[ZDKHelpCenterProvider alloc] initWithLocale:locale()];
    ZDKArrayCallback([ZendeskJSON NSArrayOfZDKHelpCenterCategoriesToJSON:result], "didHelpCenterGetCategory")
    [provider getCategoryById:GetStringParam(categoryId) withCallback:callback];
}

void _zendeskHelpCenterProviderUpvoteArticle(char * gameObjectName, char * callbackId, char * articleId) {
    ZDKHelpCenterProvider *provider = [[ZDKHelpCenterProvider alloc] initWithLocale:locale()];
    ZDKArrayCallback([ZendeskJSON ZDKVoteResponseToJSON:(ZDKHelpCenterArticleVote*)result], "didHelpCenterUpvoteArticle")
    [provider upvoteArticleWithId:GetStringParam(articleId) withCallback:callback];
}

void _zendeskHelpCenterProviderDownvoteArticle(char * gameObjectName, char * callbackId, char * articleId) {
    ZDKHelpCenterProvider *provider = [[ZDKHelpCenterProvider alloc] initWithLocale:locale()];
    ZDKArrayCallback([ZendeskJSON ZDKVoteResponseToJSON:(ZDKHelpCenterArticleVote*)result], "didHelpCenterDownvoteArticle")
    [provider downvoteArticleWithId:GetStringParam(articleId) withCallback:callback];
}

void _zendeskHelpCenterProviderDeleteVote(char * gameObjectName, char * callbackId, char * voteId) {
    ZDKHelpCenterProvider *provider = [[ZDKHelpCenterProvider alloc] initWithLocale:locale()];
    ZDKDefCallback(id, [ZendeskJSON ZDKGenericResponseToJSON:result], "didHelpCenterDeleteVote")
    [provider deleteVoteWithId:GetStringParam(voteId) withCallback:callback];
}

void _zendeskHelpCenterProviderSubmitRecordArticleView(char * gameObjectName, char * callbackId, char * articleId, char * htmlUrl, char * title) {
    ZDKHelpCenterProvider *provider = [[ZDKHelpCenterProvider alloc] initWithLocale:locale()];
    ZDKDefCallback(id, [ZendeskJSON ZDKGenericResponseToJSON:result], "didHelpCenterSubmitRecordArticleView")
    ZDKHelpCenterArticle *article = [ZDKHelpCenterArticle alloc];
    article.sid = GetStringParam(articleId);
    article.title = GetStringParam(title);
    article.htmlUrl = GetStringParam(htmlUrl);
    
    [provider submitRecordArticleView:article withCallback:callback];
}

NSString* locale() {
    NSString *locale = [ZDKConfig instance].userLocale;

    if ( ! locale ) {
        locale = [NSLocale preferredLanguages].firstObject;
    }

    return locale;
}
