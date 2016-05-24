package com.zendesk.unity.push;

import android.app.Notification;
import android.app.PendingIntent;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.support.v4.app.NotificationCompat;
import android.support.v4.app.NotificationManagerCompat;

import com.zendesk.unity.R;
import com.zendesk.unity.ZendeskInitializer;
import com.zendesk.util.StringUtils;

public class LocalNotification extends BroadcastReceiver {

    @Override
    public void onReceive(Context context, Intent intent) {
        String message = intent.getExtras().getString("message");
        if (message == null) {
            message = StringUtils.EMPTY_STRING;
        }
        final PendingIntent pendingIntent = PendingIntent.getActivity(context, 0, ZendeskInitializer.getLaunchingIntent(context), 0);

        // customize the Android notification
        // change the title, set an icon, and anything else you want to do
        final Notification notification = new NotificationCompat.Builder(context)
                .setContentTitle(context.getString(R.string.push_title))
                // .setSmallIcon(R.drawable.ic_notification)
                .setContentText(message)
                .setContentIntent(pendingIntent)
                .setAutoCancel(true)
                .build();

        NotificationManagerCompat.from(context).notify(0, notification);
    }
}
