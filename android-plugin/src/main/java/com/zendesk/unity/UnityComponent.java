package com.zendesk.unity;

import android.app.Activity;
import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonPrimitive;
import com.google.gson.JsonSerializationContext;
import com.google.gson.JsonSerializer;
import com.unity3d.player.UnityPlayer;
import com.zendesk.service.ErrorResponse;
import com.zendesk.service.ZendeskCallback;

import java.lang.reflect.Type;
import java.util.Date;
import java.util.List;

public class UnityComponent {

    private static final String CALLBACK_NAME = "OnZendeskCallback";

    private static Gson _gson;

    /** Fetches the current Activity that the Unity player is using */
    protected Activity getActivity() {
        return UnityPlayer.currentActivity;
    }

    protected Gson gson() {
        if (_gson == null) {
            _gson = new GsonBuilder().registerTypeAdapter(Date.class, makeDateTimeSerializer()).create();
        }
        return _gson;
    }

    protected String handleError(ErrorResponse errorResponse, JsonObject overallJson){
        JsonObject errorJson = new JsonObject();
        errorJson.addProperty("isNetworkError",errorResponse.isNetworkError());
        errorJson.addProperty("reason", errorResponse.getReason());
        errorJson.addProperty("status", errorResponse.getStatus());
        overallJson.addProperty("error", gson().toJson(errorJson));
        return gson().toJson(overallJson);
    }

    //Used for callbacks from Java to Unity
    protected void UnitySendMessage(String go, String m, String p){
        try{
            UnityPlayer.UnitySendMessage(go, m, p);
        } catch(Exception e){
            Log.i("Zendesk", "UnitySendMessage error: " + e.getMessage());
            Log.i("Zendesk", "UnitySendMessage: " + go + ", " + m + ", " + p);
        }
    }

    // Date
    private static JsonSerializer<Date> makeDateTimeSerializer() {
        return new JsonSerializer<Date>() {
            @Override
            public JsonElement serialize(Date src, Type typeOfSrc, JsonSerializationContext context) {
                return src == null ? null : new JsonPrimitive(src.getTime());
            }
        };
    }

    public class ZendeskUnityCallback<T> extends ZendeskCallback<T> {
        private final String gameObjectName, callbackName;
        public JsonObject overallJson;

        public ZendeskUnityCallback(String gameObjectName, String callbackId, String callbackName) {
            overallJson = new JsonObject();
            overallJson.addProperty("callbackId", callbackId);
            overallJson.addProperty("methodName", callbackName);
            this.gameObjectName = gameObjectName;
            this.callbackName = callbackName;
        }

        @Override
        public void onSuccess(T response) {
            if (response instanceof List)
                overallJson.addProperty("type", "list");
            overallJson.addProperty("result", gson().toJson(response));
            String finalJson = gson().toJson(overallJson);
            UnitySendMessage(gameObjectName, CALLBACK_NAME, finalJson);
        }

        @Override
        public void onError(ErrorResponse errorResponse) {
            String finalJson = handleError(errorResponse, overallJson);
            UnitySendMessage(gameObjectName, CALLBACK_NAME, finalJson);
        }
    }
}
