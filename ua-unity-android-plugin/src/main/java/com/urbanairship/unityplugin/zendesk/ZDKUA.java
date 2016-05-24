/*
 Copyright 2015 Urban Airship and Contributors
*/

package com.urbanairship.unityplugin.zendesk;

import android.app.Application;
import android.util.Log;

import com.urbanairship.AirshipConfigOptions;
import com.urbanairship.UAirship;
import com.urbanairship.push.notifications.DefaultNotificationFactory;
import com.zendesk.unity.push.ZDKPush;

public class ZDKUA {

    public static void initialize(Application app) {
        // Load Urban Airship configuration from 'airshipconfig.properties'
        final AirshipConfigOptions options = AirshipConfigOptions.loadDefaultOptions(app);

        // Initialize Urban Airship
        UAirship.takeOff(app, options, new UAirship.OnReadyCallback() {
                    @Override
                    public void onAirshipReady(UAirship uAirship) {
                        String channelId = uAirship.getPushManager().getChannelId();
                        Log.d("ZDKUA", "Urban Airship is initialized, channel ID: " + channelId);
                        ZDKPush.onPushIdReceived(true, channelId, null);
                    }
                });

        // Enable push notification
        UAirship.shared().getPushManager().setUserNotificationsEnabled(true);

        // Configure push notifications (for example, setting the notification icon
         final DefaultNotificationFactory defaultNotificationFactory = new DefaultNotificationFactory(app.getApplicationContext());
         defaultNotificationFactory.setSmallIconId(R.drawable.ic_conversations);
         UAirship.shared().getPushManager().setNotificationFactory(defaultNotificationFactory);
    }
}
