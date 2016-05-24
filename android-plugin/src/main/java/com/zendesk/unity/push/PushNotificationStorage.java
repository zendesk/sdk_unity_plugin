package com.zendesk.unity.push;

import android.content.Context;
import android.content.SharedPreferences;

public class PushNotificationStorage {

    private SharedPreferences mStorage;

    private static final String MY_DATES_STORE = "ZendeskUnity_push_storage";

    private static final String IDENTIFIER = "identifier";
    public static final String IDENTIFIER_FALLBACK = "#noidentifier#";


    public PushNotificationStorage(Context context) {
        mStorage = context.getSharedPreferences(MY_DATES_STORE, Context.MODE_PRIVATE);
    }


    public void storePushIdentifier(String identity){
        mStorage.edit()
                .putString(IDENTIFIER, identity)
                .apply();
    }

    public String getPushIdentifier(){
        return mStorage.getString(IDENTIFIER, IDENTIFIER_FALLBACK);
    }

    public boolean hasPushIdentifier(){
        return !IDENTIFIER_FALLBACK.equals(getPushIdentifier());
    }

    public void clear(){
        mStorage.edit().clear().apply();
    }
}
