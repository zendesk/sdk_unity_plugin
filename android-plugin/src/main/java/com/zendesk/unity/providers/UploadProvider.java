package com.zendesk.unity.providers;

import android.os.Environment;
import android.util.Base64;
import android.util.Log;


import com.zendesk.logger.Logger;
import com.zendesk.sdk.model.request.UploadResponse;
import com.zendesk.sdk.network.impl.ZendeskConfig;
import com.zendesk.unity.UnityComponent;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;



public class UploadProvider extends UnityComponent {

    private static final String LOG_TAG = "BridgeUploadProvider";

    public static UploadProvider _instance;
    public static Object instance(){
        _instance = new UploadProvider();
        return _instance;
    }

    //uploadAttachment provider method
    public void uploadAttachment(final String gameObjectName, String callbackId, String attachment, String filename, String contentType){
        File fileUpload = new File(Environment.getExternalStorageDirectory()+"/"+filename);

        if (fileUpload.exists()) {
            Logger.d(LOG_TAG, "File already exists. Attempting to delete");
            if (fileUpload.delete()) {
                Logger.d(LOG_TAG, "File was deleted.");
            } else {
                Logger.d(LOG_TAG, "File was not deleted.");
            }
        }

        byte[] decoded;
        if(contentType.equals("text") || contentType.equals("txt")){
            decoded = attachment.getBytes();
        } else {
            decoded = Base64.decode(attachment, 0);
        }

        try{
            FileOutputStream os = new FileOutputStream(fileUpload, false);
            os.write(decoded);
            os.flush();
            os.close();
        }
        catch(IOException ioe){
            Log.i("Zendesk Bridge", "Exception while reading the file");
        }

        com.zendesk.sdk.network.UploadProvider provider = ZendeskConfig.INSTANCE.provider().uploadProvider();

        provider.uploadAttachment(filename, fileUpload, contentType,
                new ZendeskUnityCallback<UploadResponse>(gameObjectName, callbackId, "didUploadProviderUploadAttachment"));
    }

    //deleteAttachment provider method
    public void deleteUpload(final String gameObjectName, String callbackId, String uploadToken){
        com.zendesk.sdk.network.UploadProvider provider = ZendeskConfig.INSTANCE.provider().uploadProvider();

        provider.deleteAttachment(uploadToken,
                new ZendeskUnityCallback<Void>(gameObjectName, callbackId, "didUploadProviderDeleteUpload"));
    }

}
