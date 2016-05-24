/*
 Copyright 2015 Urban Airship and Contributors
*/

package com.zendesk.unity;

import android.app.Activity;
import android.app.Application;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.os.Handler;
import android.os.Looper;
import android.util.Log;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;
import com.zendesk.sdk.network.impl.UserAgentHeaderUtil;
import com.zendesk.sdk.network.impl.ZendeskConfig;
import com.zendesk.unity.push.ZDKPush;

public class ZendeskInitializer {

    //Used to make sure the SDK is initialized to avoid errors with UI methods
    public static volatile boolean _initialized;

    public static void onResume() {
        Log.e("ZendeskApplication", "onResume component");
        Activity activity = UnityPlayer.currentActivity;
        if (activity == null) {
            Log.e("ZendeskApplication", "onResume: Unity activity is null!!");
            return;
        }

        initialize(activity.getApplication());

        ZDKPush.tryToInitializeGcm(activity);
    }

    public static synchronized boolean isInitialized() {
        return _initialized;
    }

    /** no need to call this method. */
    public static synchronized void initialize(Application app){
        Log.d("ZendeskPlugin", "should we initialize Zendesk config instance?");
        if (_initialized)
            return;
        Log.d("ZendeskPlugin", "initialize Zendesk config instance? yes");

        ZendeskConfig.INSTANCE.init(app,
                app.getString(R.string.zd_url),
                app.getString(R.string.zd_appid),
                app.getString(R.string.zd_oauth));
        _initialized = true;

        UserAgentHeaderUtil.addUnitySuffix();

        ZDKPush.attemptToInitializeUrbanAirship(app);

        // Setup handler for uncaught exceptions.
        // Something seems to get rid of stacktraces which are helpful for debugging!
        new Handler(Looper.getMainLooper()).postDelayed(new Runnable() {
            @Override
            public void run() {
                final Thread.UncaughtExceptionHandler prev = Thread.getDefaultUncaughtExceptionHandler();
                Thread.setDefaultUncaughtExceptionHandler(new Thread.UncaughtExceptionHandler() {
                    @Override
                    public void uncaughtException(Thread thread, Throwable e) {
                        e.printStackTrace();
                        prev.uncaughtException(thread, e);
                    }
                });
            }
        }, 1000);
    }

    /** true if we are using push but not Urban Airship */
    public static boolean weAreUsingGcm(Activity activity) {
        if (!"true".equals(activity.getResources().getString(R.string.zd_usepush)))
            return false;
        if ("true".equals(activity.getResources().getString(R.string.zd_useuapush)))
            return false;
        return true;
    }

    /** true if we are using push (GCM or UA) */
    public static boolean weAreUsingPush(Activity activity) {
        if ("true".equals(activity.getResources().getString(R.string.zd_usepush)))
            return true;
        if ("true".equals(activity.getResources().getString(R.string.zd_useuapush)))
            return true;
        return false;
    }

    /** return the launching intent of the Unity app */
    public static Intent getLaunchingIntent(Context context) {
        PackageManager pm = context.getPackageManager();

        try {
            Intent intent = pm.getLaunchIntentForPackage(context.getPackageName());
            if (intent != null)
                return intent;

            // backup
            intent = pm.getLaunchIntentForPackage("com.unity3d.player");
            if (intent != null)
                return intent;
        } catch (Exception ex) {
            //
        }

        return new Intent(context, UnityPlayerActivity.class);
    }
}

