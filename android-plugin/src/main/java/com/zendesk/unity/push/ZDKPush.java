package com.zendesk.unity.push;

import com.zendesk.sdk.model.push.PushRegistrationResponse;
import com.zendesk.sdk.network.impl.ZendeskConfig;
import com.zendesk.unity.UnityComponent;


public class ZDKPush extends UnityComponent {

    public static ZDKPush _instance = new ZDKPush();
    public static Object instance(){
        return _instance;
    }

    public void enablePushWithIdentifier(final String gameObjectName, String callbackId, String identifier) {
        ZendeskUnityCallback<PushRegistrationResponse> callback = new ZendeskUnityCallback<>(gameObjectName, callbackId, "didEnablePushWithIdentifier");
        ZendeskConfig.INSTANCE.enablePushWithIdentifier(identifier, callback);
    }

    public void enablePushWithUAChannelId(final String gameObjectName, String identifier, String callbackId) {
        ZendeskUnityCallback<PushRegistrationResponse> callback = new ZendeskUnityCallback<>(gameObjectName, callbackId, "didEnablePushWithUAChannelId");
        ZendeskConfig.INSTANCE.enablePushWithUAChannelId(identifier, callback);
    }

    public void disablePush(final String gameObjectName, String callbackId, String identifier) {
        ZendeskUnityCallback<Void> callback = new ZendeskUnityCallback<>(gameObjectName, callbackId, "didPushDisable");
        ZendeskConfig.INSTANCE.disablePush(identifier, callback);
    }
}
