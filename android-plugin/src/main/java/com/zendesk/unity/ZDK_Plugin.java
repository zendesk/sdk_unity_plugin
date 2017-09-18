package com.zendesk.unity;

import android.app.Activity;
import android.util.Log;

import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.unity3d.player.UnityPlayer;
import com.zendesk.logger.Logger;
import com.zendesk.sdk.feedback.WrappedZendeskFeedbackConfiguration;
import com.zendesk.sdk.feedback.ZendeskFeedbackConfiguration;
import com.zendesk.sdk.feedback.ui.ContactZendeskActivity;
import com.zendesk.sdk.model.access.AnonymousIdentity;
import com.zendesk.sdk.model.access.Identity;
import com.zendesk.sdk.model.access.JwtIdentity;
import com.zendesk.sdk.model.helpcenter.Article;
import com.zendesk.sdk.model.request.CustomField;
import com.zendesk.sdk.network.impl.UserAgentHeaderUtil;
import com.zendesk.sdk.network.impl.ZendeskConfig;
import com.zendesk.sdk.requests.RequestActivity;
import com.zendesk.sdk.support.ContactUsButtonVisibility;
import com.zendesk.sdk.support.SupportActivity;
import com.zendesk.sdk.support.ViewArticleActivity;
import com.zendesk.service.ErrorResponse;
import com.zendesk.service.ZendeskCallback;
import com.zendesk.util.CollectionUtils;
import com.zendesk.util.StringUtils;

import java.lang.reflect.Modifier;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Locale;
import java.util.Map;

/**
 * Zendesk Plugin
 */
public class ZDK_Plugin extends UnityComponent {

    private static final String LOG_TAG = "ZDK_Plugin";

    private static final int CONTACT_US_BUTTON_VISIBILITY_OFF = 0;
    private static final int CONTACT_US_BUTTON_VISIBILITY_ARTICLE_LIST_ONLY = 1;
    private static final int CONTACT_US_BUTTON_VISIBILITY_ARTICLE_LIST_ARTICLE_VIEW = 2;
    
    public static ZDK_Plugin _instance;
    public static Object instance(){
        _instance = new ZDK_Plugin();
        return _instance;
    }

    //Used for RMA to tell RateMyAppActivity which method to show
    public static boolean _rmaShowAlways;

    // this field is only useful when doing direct tests outside of Unity
    public Activity _activity;

    // Fetches the current Activity that the Unity player is using
    @Override
    protected Activity getActivity() {
        if (_activity != null)
            return _activity;

        return super.getActivity();
    }


    // ##### ##### ##### ##### ##### ##### ##### #####
    // ZDKConfig
    // ##### ##### ##### ##### ##### ##### ##### #####


    public void initialize(String zendeskUrl, String applicationId, String oauthClientId) {

        Activity activity = UnityPlayer.currentActivity;
        if (activity == null) {
            Log.e(LOG_TAG, "initialize: Unity activity is null!!");
            return;
        }

        ZendeskConfig.INSTANCE.init(activity.getApplication(), zendeskUrl, applicationId, oauthClientId);
        UserAgentHeaderUtil.addUnitySuffix();
    }

    //authenticate anonymous identity with details
    public void authenticateAnonymousIdentity(String name, String email){
        Identity anonymousIdentity = new AnonymousIdentity.Builder().withEmailIdentifier(email)
                .withNameIdentifier(name)
                .build();
        ZendeskConfig.INSTANCE.setIdentity(anonymousIdentity);
    }

    public void authenticateJwtUserIdentity(String jwtUserIdentity){
        Identity jwtIdentity = new JwtIdentity(jwtUserIdentity);
        ZendeskConfig.INSTANCE.setIdentity(jwtIdentity);
    }

    public void setCoppaEnabled(boolean enabled) {
        ZendeskConfig.INSTANCE.setCoppaEnabled(enabled);
    }

    public void setCustomFields(String jsonFields) {

        Map<String, String> fields = getGson().fromJson(jsonFields, Map.class);

        List<CustomField> customFields = new ArrayList<>(fields.entrySet().size());
        for (Map.Entry<String, String> field: fields.entrySet()) {
            customFields.add(new CustomField(Long.valueOf(field.getKey()), field.getValue()));
        }
        ZendeskConfig.INSTANCE.setCustomFields(customFields);
    }

    public String getCustomFields() {
        return getGson().toJson(ZendeskConfig.INSTANCE.getCustomFields());
    }

    public void setUserLocale(String locale) {
        try {
            ZendeskConfig.INSTANCE.setDeviceLocale(locale != null ? new Locale(locale) : null);
        } catch (Exception ex) {
            Log.e("Zendesk", "failed setting user locale", ex);
        }
    }


    // ##### ##### ##### ##### ##### ##### ##### #####
    // ZDKLogger
    // ##### ##### ##### ##### ##### ##### ##### #####

    public void enableLogger(boolean boolString){
            Logger.setLoggable(boolString);
    }


    // ##### ##### ##### ##### ##### ##### ##### #####
    // ZDKHelpCenter
    // ##### ##### ##### ##### ##### ##### ##### #####

    public void showHelpCenter(){
        if(!checkInitialized())
            return;
        getActivity().runOnUiThread(new Runnable() {
            public void run() {
                new SupportActivity.Builder()
                        .show(getActivity());
            }
        });
    }

    public void showHelpCenter(boolean collapseCategories, final boolean showContactUsButton, String[] labelNames, long[] sectionIds, long[] categoryIds, final String[] tags, final String additionalInfo, final String requestSubject) {
        
            showHelpCenter(collapseCategories, CONTACT_US_BUTTON_VISIBILITY_ARTICLE_LIST_ARTICLE_VIEW, labelNames, sectionIds, categoryIds, tags, additionalInfo, requestSubject, true);
        }
    
    public void showHelpCenter(boolean collapseCategories, final int contactUsButtonVisibility,
                                    String[] labelNames, long[] sectionIds, long[] categoryIds,
                                    final String[] tags, final String additionalInfo, final String requestSubject,
                                    boolean articleVoting) {

        if(!checkInitialized()) {
            return;
        }

        final SupportActivity.Builder builder = new SupportActivity.Builder();
            
        ContactUsButtonVisibility visibility = ContactUsButtonVisibility.ARTICLE_LIST_AND_ARTICLE;
        if (contactUsButtonVisibility == CONTACT_US_BUTTON_VISIBILITY_OFF) {
            visibility = ContactUsButtonVisibility.OFF;
        } else if (contactUsButtonVisibility == CONTACT_US_BUTTON_VISIBILITY_ARTICLE_LIST_ONLY) {
            visibility = ContactUsButtonVisibility.ARTICLE_LIST_ONLY;
        }

        builder
                .withCategoriesCollapsed(collapseCategories)
                .withContactUsButtonVisibility(visibility)
                .withLabelNames(labelNames)
                .withArticlesForSectionIds(sectionIds)
                .withArticlesForCategoryIds(categoryIds)
                .withArticleVoting(articleVoting);


        if (StringUtils.hasLength(additionalInfo) || StringUtils.hasLength(requestSubject) || CollectionUtils.isNotEmpty(tags)) {

            builder.withContactConfiguration(new ZendeskFeedbackConfiguration() {
                @Override
                public List<String> getTags() {
                    return tags == null ? new ArrayList<String>() : Arrays.asList(tags);
                }

                @Override
                public String getAdditionalInfo() {
                    return additionalInfo;
                }

                @Override
                public String getRequestSubject() {
                    return requestSubject;
                }
            });
        }

        getActivity().runOnUiThread(new Runnable() {

            @Override
            public void run() {
                builder.show(getActivity());
            }
        });

    }

    private boolean checkInitialized() {
        if (ZendeskConfig.INSTANCE.isInitialized()) {
            return true;
        }
        Log.e("Zendesk Unity", "Zendesk SDK must be initialized before doing anything else! Did you call ZendeskSDK.ZDKConfig.Initialize(...)?");
        return false;
    }

    public void viewArticle(final String id){
        Long idLong = Long.valueOf(id);
        com.zendesk.sdk.network.HelpCenterProvider provider = ZendeskConfig.INSTANCE.provider().helpCenterProvider();

        provider.getArticle(idLong, new ZendeskCallback<Article>() {
            @Override
            public void onSuccess(Article article) {
                ViewArticleActivity.startActivity(getActivity(), article);
            }

            @Override
            public void onError(ErrorResponse errorResponse) {
                Log.i("Zendesk Unity", "Something went wrong in trying to view article");
            }
        });
    }


    // ##### ##### ##### ##### ##### ##### ##### #####
    // ZDKRequests
    // ##### ##### ##### ##### ##### ##### ##### #####

    public void showRequestCreation(){
        if(!checkInitialized())
            return;

        getActivity().runOnUiThread(new Runnable() {
            public void run() {
                ContactZendeskActivity.startActivity(getActivity(), null);
            }
        });
    }

    public void showRequestCreationWithConfig(final String requestSubject, final String[] tags, final String additionalInfo){
        if(!checkInitialized())
            return;

        getActivity().runOnUiThread(new Runnable() {
            public void run() {

                ContactZendeskActivity.startActivity(getActivity(), new WrappedZendeskFeedbackConfiguration(
                        new ZendeskFeedbackConfiguration() {
                            @Override
                            public List<String> getTags() {
                                return tags == null ? null : Arrays.asList(tags);
                            }

                            @Override
                            public String getAdditionalInfo() {
                                return additionalInfo;
                            }

                            @Override
                            public String getRequestSubject() {
                                return requestSubject;
                            }
                        }
                ));
            }
        });
    }

    public void showRequestList() {
        if(!checkInitialized())
            return;

        getActivity().runOnUiThread(new Runnable() {
            public void run() {
                RequestActivity.startActivity(getActivity(), null);
            }
        });
    }

    /**
     *  Form id for ticket creation.
     *
     *  The ticket form id will be ignored if your Zendesk doesn't support it.  Currently
     *  Enterprise and higher plans support this.
     *
     *  @param ticketFormId the form id for ticket creation
     *
     *  @see <a href="https://developer.zendesk.com/embeddables/docs/ios/providers#using-custom-fields-and-custom-forms">Custom fields and forms documentation</a>
     *  @since 1.0.0.1
     */
    public void setTicketFormId(String ticketFormId) {
        if (!checkInitialized()) {
            return;
        }

        Long formId = null;

        try {
            formId = Long.valueOf(ticketFormId);
        } catch (NumberFormatException e) {
            Logger.e(LOG_TAG, "The supplied ticketFormId was not a number", e);
        }

        if (formId != null) {
            ZendeskConfig.INSTANCE.setTicketFormId(formId);
        }
    }

    private Gson getGson() {
        return new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .excludeFieldsWithModifiers(Modifier.TRANSIENT)
                .create();
    }
}
