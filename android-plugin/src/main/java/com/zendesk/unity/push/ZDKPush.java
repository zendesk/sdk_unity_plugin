package com.zendesk.unity.push;

import android.app.Activity;
import android.app.Application;
import android.os.Handler;
import android.os.Looper;
import android.util.Log;

import com.unity3d.player.UnityPlayer;
import com.zendesk.sdk.model.network.PushRegistrationResponse;
import com.zendesk.sdk.network.impl.ZendeskConfig;
import com.zendesk.service.ErrorResponse;
import com.zendesk.service.ErrorResponseAdapter;
import com.zendesk.service.ZendeskCallback;
import com.zendesk.unity.R;
import com.zendesk.unity.UnityComponent;
import com.zendesk.unity.ZendeskInitializer;

import java.util.ArrayList;
import java.util.List;

import retrofit.client.Response;

public class ZDKPush extends UnityComponent {

    public static ZDKPush _instance = new ZDKPush();
    public static Object instance(){
        return _instance;
    }

    private static List<ZendeskUnityCallback<PushRegistrationResponse>> waitingForEnable = new ArrayList<>();
    private static List<ZendeskUnityCallback<?>> waitingCallbacks = new ArrayList<>();
    public static volatile boolean gettingPushId = false;
    public static volatile ErrorResponse uaFailure = null;

    private static PushNotificationStorage sPushStorage = null;

    /** this is called by ZDK_Plugin.initialize(), don't worry about calling it */
    public static void attemptToInitializeUrbanAirship(final Application app) {
        if (sPushStorage == null) {
            sPushStorage = new PushNotificationStorage(app);
        }
        new Handler(Looper.getMainLooper()).post(new Runnable() {
            @Override
            public void run() {
                Class<?> zdkUa;
                try {
                    zdkUa = Class.forName("com.urbanairship.unityplugin.zendesk.ZDKUA");
                } catch (ClassNotFoundException e) {
                    return;
                }
                Log.d("ZendeskApplication", "Urban Airship is packaged, now initializing...");

                // don't use a cached channel ID, like we use a cached GCM sender ID
                sPushStorage.storePushIdentifier(null);

                try {
                    zdkUa.getDeclaredMethod("initialize", Application.class).invoke(null, app);
                } catch (Exception e) {
                    Log.e("ZendeskApplication", "Failed to initialize Urban Airship", e);
                }
            }
        });
    }

    public static synchronized String getPushId(Activity activity) {
        if (sPushStorage == null)
            sPushStorage = new PushNotificationStorage(activity.getApplication());
        return sPushStorage.hasPushIdentifier() ? sPushStorage.getPushIdentifier() : null;
    }

    public static synchronized void tryToInitializePush(Activity activity) {
        if (ZendeskInitializer.weAreUsingGcm(activity)) {
            tryToInitializeGcm(activity);
        }
    }

    public static synchronized String tryToInitializeGcm(Activity activity) {
        if (!ZendeskInitializer.weAreUsingGcm(activity))
            return null;

        Log.d("ZendeskApplication", "attemptToInitializeGcm");
        ZDKPush.instance();
        if (ZDKPush.gettingPushId)
            return null;
        ZDKPush.gettingPushId = true;

        // Check if we already saved the device' push identifier.
        // If not, enable push.
        if (sPushStorage == null)
            sPushStorage = new PushNotificationStorage(activity.getApplication());
        if(!sPushStorage.hasPushIdentifier()) {
            Log.d("ZendeskApplication", "attemptToInitializeGcm: needs push ID");
            if(GcmUtil.checkPlayServices(activity)){
                Log.d("ZendeskApplication", "attemptToInitializeGcm: play services are available");
                RegistrationIntentService.start(activity);
            }
            return null;
        } else {
            Log.d("ZendeskApplication", "attemptToInitializeGcm: has push ID already -- " + sPushStorage.getPushIdentifier());
            return sPushStorage.getPushIdentifier();
        }
    }

    public void enable(final String gameObjectName, String callbackId){
        synchronized (ZendeskInitializer.class) {
            ZendeskUnityCallback<PushRegistrationResponse> callback = new ZendeskUnityCallback<>(gameObjectName, callbackId, "didPushEnable");

            if (!ZendeskInitializer.weAreUsingPush(UnityPlayer.currentActivity)) {
                callback.onError(new ErrorResponseAdapter("Zendesk Unity SDK was not built with push notification support. Please rebuild the Zendesk Unityt SDK."));
                return;
            }

            String pushId = getPushId(UnityPlayer.currentActivity);
            if (pushId != null) {
                enablePush(pushId, callback);
            } else {
                if (uaFailed()) {
                    callback.onError(uaFailure);
                } else {
                    waitingForEnable.add(callback);
                    waitingCallbacks.add(callback);
                    tryToInitializePush(UnityPlayer.currentActivity);
                }
            }
        }
    }

    public void disable(final String gameObjectName, String callbackId){
        synchronized (ZendeskInitializer.class) {
            ZendeskUnityCallback<Response> callback = new ZendeskUnityCallback<>(gameObjectName, callbackId, "didPushDisable");

            if (!ZendeskInitializer.weAreUsingPush(UnityPlayer.currentActivity)) {
                callback.onError(new ErrorResponseAdapter("Zendesk Unity SDK was not built with push notification support. Please rebuild the Zendesk Unityt SDK."));
                return;
            }

            String pushId = getPushId(UnityPlayer.currentActivity);
            if (pushId != null) {
                ZendeskConfig.INSTANCE.disablePush(pushId, callback);
            } else {
                if (uaFailed()) {
                    callback.onError(uaFailure);
                } else {
                    waitingCallbacks.add(callback);
                    tryToInitializePush(UnityPlayer.currentActivity);
                }
            }
        }
    }

    private boolean uaFailed() {
        return uaFailure != null;
    }

    public static void onPushIdReceived(boolean success, String pushId, ErrorResponse error) {
        if (sPushStorage == null)
            sPushStorage = new PushNotificationStorage(UnityPlayer.currentActivity.getApplication());
        sPushStorage.storePushIdentifier(pushId);
        if (!success && "true".equals(UnityPlayer.currentActivity.getResources().getString(R.string.zd_useuapush))) {
            if (error != null)
                uaFailure = error;
            else
                uaFailure = new ErrorResponseAdapter("error setting up Urban Airship");
        }
        ((ZDKPush) instance()).onPushIdComplete(success, pushId, error);
    }

    @SuppressWarnings("unchecked")
    public void onPushIdComplete(boolean success, String pushId, ErrorResponse error) {
        synchronized (ZendeskInitializer.class) {
            gettingPushId = false;
            for (int i = 0; i < waitingCallbacks.size(); i++) {
                ZendeskUnityCallback<?> callback = waitingCallbacks.get(i);
                boolean enableCallback = waitingForEnable.contains(callback);
                if (success) {
                    if (enableCallback) {
                        enablePush(pushId, (ZendeskUnityCallback<PushRegistrationResponse>) callback);
                    } else
                        ZendeskConfig.INSTANCE.disablePush(pushId, (ZendeskCallback<Response>) callback);
                } else {
                    waitingCallbacks.get(i).onError(error);
                }
            }
            waitingCallbacks.clear();
            waitingForEnable.clear();
        }
    }

    private void enablePush(String pushId, ZendeskUnityCallback<PushRegistrationResponse> callback) {
        if (ZendeskInitializer.weAreUsingGcm(UnityPlayer.currentActivity)) {
            ZendeskConfig.INSTANCE.enablePushWithIdentifier(pushId, callback);
        } else {
            ZendeskConfig.INSTANCE.enablePushWithUAChannelId(pushId, callback);
        }
    }
}
