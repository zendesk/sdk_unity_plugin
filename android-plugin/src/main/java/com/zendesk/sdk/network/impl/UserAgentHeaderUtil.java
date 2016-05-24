package com.zendesk.sdk.network.impl;

import android.util.Pair;

import com.zendesk.unity.BuildConfig;

public class UserAgentHeaderUtil {

    private static final String UNITY_USER_AGENT_HEADER = "Unity";

    public static void addUnitySuffix() {
        ZendeskConfig.INSTANCE.addUserAgentHeaderSuffix(new Pair<>(UNITY_USER_AGENT_HEADER, BuildConfig.VERSION_NAME));
    }

}
