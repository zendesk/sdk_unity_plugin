package com.zendesk.unity;

import android.app.Activity;
import android.content.Intent;
import android.util.Log;

import com.zendesk.logger.Logger;
import com.zendesk.sdk.feedback.ZendeskFeedbackConfiguration;
import com.zendesk.sdk.feedback.impl.BaseZendeskFeedbackConfiguration;
import com.zendesk.sdk.feedback.ui.ContactZendeskActivity;
import com.zendesk.sdk.model.CustomField;
import com.zendesk.sdk.model.helpcenter.Article;
import com.zendesk.sdk.model.network.AnonymousIdentity;
import com.zendesk.sdk.model.network.Identity;
import com.zendesk.sdk.model.network.JwtIdentity;
import com.zendesk.sdk.network.impl.ZendeskConfig;
import com.zendesk.sdk.network.impl.ZendeskGsonProvider;
import com.zendesk.sdk.network.impl.ZendeskHelpCenterProvider;
import com.zendesk.sdk.requests.RequestActivity;
import com.zendesk.sdk.support.SupportActivity;
import com.zendesk.sdk.support.ViewArticleActivity;
import com.zendesk.service.ErrorResponse;
import com.zendesk.service.ZendeskCallback;
import com.zendesk.util.StringUtils;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Locale;
import java.util.Map;

/**
 * Zendesk Plugin
 */
public class ZDK_Plugin extends UnityComponent {

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

    public void onResume() {
        ZendeskInitializer.onResume();
    }

    //authenticate anonymous identity with details
    public void authenticateAnonymousIdentity(String name, String email, String externalId){
        Identity anonymousIdentity = new AnonymousIdentity.Builder().withEmailIdentifier(email)
                .withExternalIdentifier(externalId)
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
        Map<String, String> fields = ZendeskGsonProvider.INSTANCE.getZendeskGson()
                .fromJson(jsonFields, Map.class);

        List<CustomField> customFields = new ArrayList<>(fields.entrySet().size());
        for (Map.Entry<String, String> field: fields.entrySet()) {
            customFields.add(new CustomField(Long.valueOf(field.getKey()), field.getValue()));
        }
        ZendeskConfig.INSTANCE.setCustomFields(customFields);
    }

    public String getCustomFields() {
        return ZendeskGsonProvider.INSTANCE.getZendeskGson()
                .toJson(ZendeskConfig.INSTANCE.getCustomFields());
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

        checkForContactConfiguration();

        getActivity().runOnUiThread(new Runnable() {
            public void run() {
                new SupportActivity.Builder()
                        .showContactUsButton(true)
                        .show(getActivity());
            }
        });
    }

    private void checkForContactConfiguration() {
        if (ZendeskConfig.INSTANCE.getContactConfiguration() == null) {
            ZendeskConfig.INSTANCE.setContactConfiguration(new BaseZendeskFeedbackConfiguration() {
                @Override
                public String getRequestSubject() {
                    return "App ticket";
                }
            });
        }
    }

    public void showHelpCenterWithOptions(final boolean listCats, final int listSections, final int listArticles,
                               final String[] listLabels, int listLabelsLenth, final boolean showContactUsButton){
        if(!checkInitialized())
            return;

        checkForContactConfiguration();

        getActivity().runOnUiThread(new Runnable() {
            public void run() {
                SupportActivity.Builder b = new SupportActivity.Builder();
                if (listCats)
                    b.listCategories();
                else if (listSections != 0)
                    b.listSections(listSections);
                else if (listArticles != 0)
                    b.listArticles(listArticles);
                else if (listLabels != null)
                    b.listArticlesByLabels(listLabels);
                b.showContactUsButton(showContactUsButton)
                        .show(getActivity());
            }
        });
    }

    public void setContactConfiguration(String[] tags, int tagsLength, final String additionalRequestInfo,
                                  final String requestSubject) {

        final ArrayList<String> listTags = new ArrayList<String>(Arrays.asList(tags));

        ZendeskConfig.INSTANCE.setContactConfiguration(new ZendeskFeedbackConfiguration() {
            @Override
            public List<String> getTags() {
                return listTags;
            }

            @Override
            public String getAdditionalInfo() {
                return additionalRequestInfo;
            }

            @Override
            public String getRequestSubject() {
                return requestSubject;
            }
        });
    }

    private boolean checkInitialized() {
        if (ZendeskInitializer.isInitialized())
            return true;
        Log.e("Zendesk Unity", "Zendesk SDK must be initialized before doing anything else! Did you call ZendeskSDK.ZDKConfig.Initialize(gameObject)?");
        return false;
    }

    public void viewArticle(final String id){
        Long idLong = Long.valueOf(id);
        com.zendesk.sdk.network.HelpCenterProvider provider = new ZendeskHelpCenterProvider();

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

        checkForContactConfiguration();

        getActivity().runOnUiThread(new Runnable() {
            public void run() {

                Intent requestIntent = new Intent(getActivity().getApplicationContext(), ContactZendeskActivity.class);
                getActivity().startActivity(requestIntent);
            }
        });
    }

    public void showRequestList(){
        if(!checkInitialized())
            return;

        getActivity().runOnUiThread(new Runnable() {
            public void run() {

                Intent requestIntent = new Intent(getActivity().getApplicationContext(), RequestActivity.class);
                getActivity().startActivity(requestIntent);
            }
        });
    }


    // ##### ##### ##### ##### ##### ##### ##### #####
    // ZDKRMA
    // ##### ##### ##### ##### ##### ##### ##### #####

    //Method to show RateMyApp
    public void showInView(boolean showAlways){
        if(!checkInitialized())
            return;
        final RMAConfig config= new RMAConfig();
        //This will determine if which method the RateMyAppActivity will call
        config.showAlways = showAlways;
        config.dialogAction = null;

        getActivity().runOnUiThread(new Runnable() {
            public void run() {
                //Need to open a Fragment Activity to be able to show RateMyApp dialogue
                Intent requestIntent = new Intent(getActivity().getApplicationContext(), RateMyAppActivity.class);
                //Lets put away the config object so we can use it later
                requestIntent.putExtra("ConfigObject", config);
                getActivity().startActivity(requestIntent);
            }
        });
    }

    //Method to show RateMyApp With Configurations
    public void showInViewWithConfig(boolean showAlways, String[] tags, String additionalInfo,
                                     boolean[] dialogActions, String requestSubject){
        if(!checkInitialized())
            return;
        final RMAConfig config= new RMAConfig();
        config.showAlways = showAlways;
        //This object will contain extra information
        config.showConfig = true;
        //If it's not null then the user passed in values to the dialogAction array
        if(dialogActions != null){
            for(int i = 0; i < dialogActions.length; i++){
                config.dialogAction[i] = dialogActions[i];
            }
        }
        //Match the rest of the information passed in. It's fine if they are null
        config.tags = tags;
        config.additionalInfo = additionalInfo;
        config.requestSubject = requestSubject;

        getActivity().runOnUiThread(new Runnable() {
            public void run() {
                //Need to open a Fragment Activity to be able to show RateMyApp dialogue
                Intent requestIntent = new Intent(getActivity().getApplicationContext(), RateMyAppActivity.class);
                //Lets put away the config object so we can use it later
                requestIntent.putExtra(RateMyAppActivity.EXTRA_CONFIG_OBJECT, config);
                getActivity().startActivity(requestIntent);
            }
        });
    }


    // ##### ##### ##### ##### ##### ##### ##### #####
    // String Util
    // ##### ##### ##### ##### ##### ##### ##### #####

    public String csvStringFromArray(String[] strings, int length){
        return StringUtils.toCsvString(strings);
    }
}
