package com.urbanairship.unityplugin.zendesk;

import android.app.NotificationManager;
import android.content.Context;
import android.content.Intent;
import android.support.annotation.Nullable;
import android.util.Log;

import com.urbanairship.push.BaseIntentReceiver;
import com.urbanairship.push.PushMessage;
import com.zendesk.sdk.deeplinking.ZendeskDeepLinking;
import com.zendesk.sdk.network.impl.ZendeskConfig;
import com.zendesk.unity.ZendeskInitializer;
import com.zendesk.util.StringUtils;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Locale;

public class IntentReceiver extends BaseIntentReceiver {
    
    private final static String LOG_TAG = IntentReceiver.class.getSimpleName();
    
    @Override
    protected void onChannelRegistrationSucceeded(Context context, String channelId) {
        // Intentionally empty
    }
    
    @Override
    protected void onChannelRegistrationFailed(Context context) {
        // Intentionally empty
    }
    
    @Override
    protected void onPushReceived(Context context, PushMessage message, int notificationId) {
        
        // Extract ticket id
        final String tid = message.getPushBundle().getString("tid");
        
        // Check if ticket id is valid
        if(!StringUtils.hasLength(tid)){
            Log.e(LOG_TAG, String.format(Locale.US, "No valid ticket id found: '%s'", tid));
            return;
        }
        
        // Try to refresh the comment stream if visible
        final boolean refreshed = ZendeskDeepLinking.INSTANCE.refreshComments(tid);
        
        // If stream was successfully refreshed, we can cancel the notification
        if(refreshed){
            final NotificationManager nm = (NotificationManager) context.getSystemService(Context.NOTIFICATION_SERVICE);
            nm.cancel(notificationId);
        }
    }
    
    @Override
    protected void onBackgroundPushReceived(Context context, PushMessage message) {
        // Intentionally empty
    }
    
    @Override
    protected boolean onNotificationActionOpened(Context context, PushMessage message, int notificationId, String buttonId, boolean isForeground) {
        // Intentionally empty
        // Return false to let UA handle launching the launch activity
        return false;
    }
    
    @Override
    protected void onNotificationDismissed(Context context, PushMessage message, int notificationId) {
        // Intentionally empty
    }
    
    @Override
    protected boolean onNotificationOpened(Context context, PushMessage message, int notificationId) {
        
        // Extract ticket id
        final String tid = message.getPushBundle().getString("tid");
        
        // Check if ticket id is valid
        if(!StringUtils.hasLength(tid)){
            Log.e(LOG_TAG, String.format(Locale.US, "No valid ticket id found: '%s'", tid));
            return false;
        }
        
        Log.d(LOG_TAG, String.format(Locale.US, "Ticket Id found: %s", tid));
        
        // Create an Intent for showing RequestActivity
        final Intent zendeskDeepLinkIntent = getZendeskDeepLinkIntent(context.getApplicationContext(), tid);
        
        // Check if Intent is valid
        if(zendeskDeepLinkIntent == null){
            Log.e(LOG_TAG, "Error zendeskDeepLinkIntent is 'null'");
            return false;
        }
        
        // Show RequestActivity
        context.sendBroadcast(zendeskDeepLinkIntent);
        
        return true;
    }
    
    @Nullable
    private Intent getZendeskDeepLinkIntent(Context context, String ticketId){
        
        // Make sure that Zendesk is initialized
        if(!ZendeskConfig.INSTANCE.isInitialized()) {
            ZendeskConfig.INSTANCE.init(context,
                                        context.getString(R.string.zd_url),
                                        context.getString(R.string.zd_appid),
                                        context.getString(R.string.zd_oauth));
        }
        
        // Initialize a back stack
        final Intent mainActivity = ZendeskInitializer.getLaunchingIntent(context);
        final ArrayList<Intent> backStack = new ArrayList<>(Arrays.asList(mainActivity));
        
        return ZendeskDeepLinking.INSTANCE.getRequestIntent(context, ticketId, null, backStack, mainActivity);
    }
}
