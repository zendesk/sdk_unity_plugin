package com.zendesk.unity.providers;

import com.zendesk.sdk.model.helpcenter.Article;
import com.zendesk.sdk.model.helpcenter.ArticleVote;
import com.zendesk.sdk.model.helpcenter.Attachment;
import com.zendesk.sdk.model.helpcenter.AttachmentType;
import com.zendesk.sdk.model.helpcenter.Category;
import com.zendesk.sdk.model.helpcenter.FlatArticle;
import com.zendesk.sdk.model.helpcenter.HelpCenterSearch;
import com.zendesk.sdk.model.helpcenter.ListArticleQuery;
import com.zendesk.sdk.model.helpcenter.SearchArticle;
import com.zendesk.sdk.model.helpcenter.Section;
import com.zendesk.sdk.model.helpcenter.SortBy;
import com.zendesk.sdk.model.helpcenter.SortOrder;
import com.zendesk.sdk.model.helpcenter.SuggestedArticleSearch;
import com.zendesk.sdk.model.network.GenericResponse;
import com.zendesk.sdk.model.network.SuggestedArticleResponse;
import com.zendesk.sdk.network.impl.ZendeskHelpCenterProvider;
import com.zendesk.unity.UnityComponent;

import java.util.List;
import java.util.Locale;

public class HelpCenterProvider extends UnityComponent {

    public static HelpCenterProvider _instance;
    public static Object instance(){
        _instance = new HelpCenterProvider();
        return _instance;
    }

    private Long parseLong(String value) {
        try {
            return Long.valueOf(value);
        } catch (NumberFormatException ex) {
            //
        }
        return 0L;
    }

    public void getCategories(final String gameObjectName, String callbackId){
        com.zendesk.sdk.network.HelpCenterProvider provider = new ZendeskHelpCenterProvider();

        provider.getCategories(
                new ZendeskUnityCallback<List<Category>>(gameObjectName, callbackId, "didHelpCenterProviderGetCategories"));
    }

    public void getSectionsForCategory(final String gameObjectName, String callbackId, String categoryId){
        com.zendesk.sdk.network.HelpCenterProvider provider = new ZendeskHelpCenterProvider();

        provider.getSections(parseLong(categoryId),
                new ZendeskUnityCallback<List<Section>>(gameObjectName, callbackId, "didHelpCenterProviderGetSectionsForCategoryId"));
    }

    public void getArticlesForSection(final String gameObjectName, String callbackId, String sectionId){
        com.zendesk.sdk.network.HelpCenterProvider provider = new ZendeskHelpCenterProvider();

        provider.getArticles(parseLong(sectionId),
                new ZendeskUnityCallback<List<Article>>(gameObjectName, callbackId, "didHelpCenterProviderGetArticlesForSectionId"));
    }

    public void searchArticlesUsingQuery(final String gameObjectName, String callbackId, String query){
        com.zendesk.sdk.network.HelpCenterProvider provider = new ZendeskHelpCenterProvider();
        HelpCenterSearch helpCenterSearch = new HelpCenterSearch.Builder().withQuery(query).build();

        provider.searchArticles(helpCenterSearch,
                new ZendeskUnityCallback<List<SearchArticle>>(gameObjectName, callbackId, "didHelpCenterProviderSearchForArticlesUsingQuery"));
    }

    public void searchArticlesUsingQueryAndLabels(final String gameObjectName, String callbackId, String query, String[] labelsArray, int labelsLength){
        com.zendesk.sdk.network.HelpCenterProvider provider = new ZendeskHelpCenterProvider();
        HelpCenterSearch helpCenterSearch = new HelpCenterSearch.Builder().withQuery(query).withLabelNames(labelsArray).build();

        provider.searchArticles(helpCenterSearch,
                new ZendeskUnityCallback<List<SearchArticle>>(gameObjectName, callbackId, "didHelpCenterProviderSearchForArticlesUsingQueryAndLabels"));
    }

    public void searchArticlesUsingHelpCenterSearch(final String gameObjectName, String callbackId,
                                                                                String query, String[] labelNames, int labelLength, String locale,
                                                                                String[] sideLoads, int sideLoadsLength, int categoryId, int sectionId,
                                                                                int page, int per_page){
        com.zendesk.sdk.network.HelpCenterProvider provider = new ZendeskHelpCenterProvider();
        HelpCenterSearch.Builder helpCenterSearchBuilder = new HelpCenterSearch.Builder();

        //Check to see if values where actually passed in to build HelpCenterSearch
        if(query != null){
            helpCenterSearchBuilder = helpCenterSearchBuilder.withQuery(query);
        }
        if(labelNames != null){
            helpCenterSearchBuilder = helpCenterSearchBuilder.withLabelNames(labelNames);
        }
        if(locale != null){
            Locale localeObject = new Locale(locale);
            helpCenterSearchBuilder = helpCenterSearchBuilder.forLocale(localeObject);
        }
        if(sideLoads != null){
            helpCenterSearchBuilder = helpCenterSearchBuilder.withIncludes(sideLoads);
        }
        if(categoryId != -1){
            helpCenterSearchBuilder = helpCenterSearchBuilder.withCategoryId((long)categoryId);
        }
        if(sectionId != -1){
            helpCenterSearchBuilder = helpCenterSearchBuilder.withSectionId((long)sectionId);
        }
        if(page != -1){
            helpCenterSearchBuilder = helpCenterSearchBuilder.page(page);
        }
        if(per_page != -1){
            helpCenterSearchBuilder = helpCenterSearchBuilder.perPage(per_page);
        }

        HelpCenterSearch helpCenterSearch = helpCenterSearchBuilder.build();

        provider.searchArticles(helpCenterSearch,
                new ZendeskUnityCallback<List<SearchArticle>>(gameObjectName, callbackId, "didHelpCenterProviderSearchForArticlesUsingHelpCenterSearch"));
    }

    public void getAttachmentsForArticle(final String gameObjectName, String callbackId, String articleId, String type){
        com.zendesk.sdk.network.HelpCenterProvider provider = new ZendeskHelpCenterProvider();

        AttachmentType attachmentType = null;
        if ("inline".equalsIgnoreCase(type))
            attachmentType = AttachmentType.INLINE;
        else if ("block".equalsIgnoreCase(type))
            attachmentType = AttachmentType.BLOCK;

        provider.getAttachments(parseLong(articleId), attachmentType,
                new ZendeskUnityCallback<List<Attachment>>(gameObjectName, callbackId, "didHelpCenterProviderGetAttachmentsForArticleId"));
    }

    public void getArticles(final String gameObjectName, String callbackId, String[] labelsArray, int labelsLength,
                            String include, String locale, int page, int resultsPerPage, int sortBy, int sortOrder){
        com.zendesk.sdk.network.HelpCenterProvider provider = new ZendeskHelpCenterProvider();

        ListArticleQuery query = new ListArticleQuery();
        query.setLabelNames(labelsArray);
        query.setInclude(include);
        if (locale != null)
            query.setLocale(new Locale(locale));
        query.setPage(page);
        query.setResultsPerPage(resultsPerPage);
        query.setSortBy(sortBy >= 0 && sortBy < SortBy.values().length ? SortBy.values()[sortBy] : null);
        query.setSortOrder(sortOrder >= 0 && sortOrder < SortOrder.values().length ? SortOrder.values()[sortOrder] : null);

        provider.listArticles(query,
                new ZendeskUnityCallback<List<SearchArticle>>(gameObjectName, callbackId, "didHelpCenterGetArticlesByLabels"));
    }

    public void getFlatArticles(final String gameObjectName, String callbackId, String[] labelsArray, int labelsLength,
                                String include, String locale, int page, int resultsPerPage, int sortBy, int sortOrder){
        com.zendesk.sdk.network.HelpCenterProvider provider = new ZendeskHelpCenterProvider();

        ListArticleQuery query = new ListArticleQuery();
        query.setLabelNames(labelsArray);
        query.setInclude(include);
        if (locale != null)
            query.setLocale(new Locale(locale));
        query.setPage(page);
        query.setResultsPerPage(resultsPerPage);
        query.setSortBy(sortBy >= 0 && sortBy < SortBy.values().length ? SortBy.values()[sortBy] : null);
        query.setSortOrder(sortOrder >= 0 && sortOrder < SortOrder.values().length ? SortOrder.values()[sortOrder] : null);

        provider.listArticlesFlat(query,
                new ZendeskUnityCallback<List<FlatArticle>>(gameObjectName, callbackId, "didHelpCenterGetFlatArticlesByLabels"));
    }

    public void getSuggestedArticles(final String gameObjectName, String callbackId, String query,
                                                              String[] labelsArray, int labelsLength, String locale, int categoryId, int sectionId){
        com.zendesk.sdk.network.HelpCenterProvider provider = new ZendeskHelpCenterProvider();

        SuggestedArticleSearch.Builder queryBuilder = new SuggestedArticleSearch.Builder()
                .withQuery(query)
                .withLabelNames(labelsArray)
                .forLocale(locale != null ? new Locale(locale) : null);
        if(categoryId != -1){
            queryBuilder = queryBuilder.withCategoryId((long)categoryId);
        }
        if(sectionId != -1){
            queryBuilder = queryBuilder.withSectionId((long)sectionId);
        }
        SuggestedArticleSearch search = queryBuilder.build();

        provider.getSuggestedArticles(search,
                new ZendeskUnityCallback<SuggestedArticleResponse>(gameObjectName, callbackId, "didHelpCenterGetSuggestedArticles"));
    }

    public void getArticle(final String gameObjectName, String callbackId, final String id){
        com.zendesk.sdk.network.HelpCenterProvider provider = new ZendeskHelpCenterProvider();

        provider.getArticle(parseLong(id),
                new ZendeskUnityCallback<Article>(gameObjectName, callbackId, "didHelpCenterGetArticle"));
    }

    public void getSection(final String gameObjectName, String callbackId, final String id){
        com.zendesk.sdk.network.HelpCenterProvider provider = new ZendeskHelpCenterProvider();

        provider.getSection(parseLong(id),
                new ZendeskUnityCallback<Section>(gameObjectName, callbackId, "didHelpCenterGetSection"));
    }

    public void getCategory(final String gameObjectName, String callbackId, final String id){
        com.zendesk.sdk.network.HelpCenterProvider provider = new ZendeskHelpCenterProvider();

        provider.getCategory(parseLong(id),
                new ZendeskUnityCallback<Category>(gameObjectName, callbackId, "didHelpCenterGetCategory"));
    }

    public void upvoteArticle(final String gameObjectName, String callbackId, final String id){
        com.zendesk.sdk.network.HelpCenterProvider provider = new ZendeskHelpCenterProvider();

        provider.upvoteArticle(parseLong(id),
                new ZendeskUnityCallback<ArticleVote>(gameObjectName, callbackId, "didHelpCenterUpvoteArticle"));
    }

    public void downvoteArticle(final String gameObjectName, String callbackId, final String id){
        com.zendesk.sdk.network.HelpCenterProvider provider = new ZendeskHelpCenterProvider();

        provider.downvoteArticle(parseLong(id),
                new ZendeskUnityCallback<ArticleVote>(gameObjectName, callbackId, "didHelpCenterDownvoteArticle"));
    }

    public void deleteVote(final String gameObjectName, String callbackId, final String id){
        com.zendesk.sdk.network.HelpCenterProvider provider = new ZendeskHelpCenterProvider();

        provider.deleteVote(parseLong(id),
                new ZendeskUnityCallback<GenericResponse>(gameObjectName, callbackId, "didHelpCenterDeleteVote"));
    }

    public void submitRecordArticleView(final String gameObjectName, String callbackId, final String id, final String localeId){
        Locale locale = localeId != null ? new Locale(localeId) : null;
        com.zendesk.sdk.network.HelpCenterProvider provider = new ZendeskHelpCenterProvider();

        provider.submitRecordArticleView(parseLong(id), locale,
                new ZendeskUnityCallback<GenericResponse>(gameObjectName, callbackId, "didHelpCenterSubmitRecordArticleView"));
    }

}
