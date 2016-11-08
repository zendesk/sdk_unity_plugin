package com.zendesk.unity.providers;

import com.zendesk.sdk.model.settings.SafeMobileSettings;
import com.zendesk.sdk.network.SdkSettingsProvider;
import com.zendesk.sdk.network.impl.ZendeskConfig;
import com.zendesk.unity.UnityComponent;

public class SettingsProvider extends UnityComponent {

    public static SettingsProvider _instance;
    public static Object instance(){
        _instance = new SettingsProvider();
        return _instance;
    }

    public void getSettings(final String gameObjectName, String callbackId){
        SdkSettingsProvider provider = ZendeskConfig.INSTANCE.provider().sdkSettingsProvider();

        provider.getSettings(
                new ZendeskUnityCallback<SafeMobileSettings>(gameObjectName, callbackId, "didSettingsProviderGetSettings"));
    }

}
