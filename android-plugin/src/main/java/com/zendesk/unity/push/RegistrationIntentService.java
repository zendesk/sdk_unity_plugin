package com.zendesk.unity.push;

import android.app.IntentService;
import android.content.Context;
import android.content.Intent;
import android.util.Log;

import com.zendesk.sdk.network.impl.ZendeskConfig;
import com.zendesk.service.ErrorResponse;
import com.zendesk.service.ZendeskCallback;

import retrofit.client.Response;

public class RegistrationIntentService extends IntentService {

    private final static String LOG_TAG = RegistrationIntentService.class.getSimpleName();


    public RegistrationIntentService() {
        super(RegistrationIntentService.class.getSimpleName());
    }


    public static void start(Context context){
        Log.d(LOG_TAG, "Starting service RegistrationIntentService...");
        context.startService(new Intent(context, RegistrationIntentService.class));
    }

    @Override
    protected void onHandleIntent(final Intent intent) {
        Log.d(LOG_TAG, "Started service RegistrationIntentService!");
        final PushNotificationStorage mPushStorage = new PushNotificationStorage(RegistrationIntentService.this);

        GcmUtil.getInstanceId(this, new ZendeskCallback<String>() {
            @Override
            public void onSuccess(final String s) {

                final boolean hasPushIdentifier = mPushStorage.hasPushIdentifier();

                if (hasPushIdentifier) {
                    ZendeskConfig.INSTANCE.disablePush(mPushStorage.getPushIdentifier(), new ZendeskCallback<Response>() {
                        @Override
                        public void onSuccess(final Response response) {
                            Log.d(LOG_TAG, "Successfully unregistered");
                            enablePush(mPushStorage);
                        }

                        @Override
                        public void onError(final ErrorResponse errorResponse) {
                            Log.d(LOG_TAG, "Error during unregister");
                            ZDKPush.onPushIdReceived(false, null, errorResponse);
                        }
                    });
                } else {
                    enablePush(mPushStorage);
                }
            }

            @Override
            public void onError(final ErrorResponse errorResponse) {
                Log.d(LOG_TAG, "Error getInstance: " + errorResponse.getReason());
                ZDKPush.onPushIdReceived(false, null, errorResponse);
            }
        });
    }

    void enablePush(final PushNotificationStorage pushNotificationStorage){
        Log.d(LOG_TAG, "Retrieving instance ID second time...");
        GcmUtil.getInstanceId(this, new ZendeskCallback<String>() {
            @Override
            public void onSuccess(final String result) {
                Log.e(LOG_TAG, "Retrieved instance ID enabling push ready!!! " + result);
                ZDKPush.onPushIdReceived(true, result, null);
            }

            @Override
            public void onError(final ErrorResponse errorResponse) {
                Log.e(LOG_TAG, "Failed during enabling push notifications: " + errorResponse.getReason());
                ZDKPush.onPushIdReceived(false, null, errorResponse);
            }
        });
    }
}
