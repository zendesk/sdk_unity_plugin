package com.zendesk.unity.providers;

import android.os.Environment;
import android.util.Base64;
import android.util.Log;

import com.zendesk.sdk.model.network.UploadResponse;
import com.zendesk.sdk.network.impl.ZendeskUploadProvider;
import com.zendesk.unity.UnityComponent;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;

import retrofit.client.Response;

public class UploadProvider extends UnityComponent {

    public static UploadProvider _instance;
    public static Object instance(){
        _instance = new UploadProvider();
        return _instance;
    }

    //uploadAttachment provider method
    public void uploadAttachment(final String gameObjectName, String callbackId, String attachment, String filename, String contentType){
        File fileUpload = new File(Environment.getExternalStorageDirectory()+"/"+filename);
        byte[] decoded;
        if(contentType.equals("text") || contentType.equals("txt")){
            decoded = attachment.getBytes();
        }
        else{
            decoded = Base64.decode(attachment, 0);
        }

        try{
            FileOutputStream os = new FileOutputStream(fileUpload, true);
            os.write(decoded);
            //maybe os.flush();
            os.close();
        }
        catch(IOException ioe){
            Log.i("Zendesk Bridge", "Exception while reading the file");
        }

        com.zendesk.sdk.network.UploadProvider provider = new ZendeskUploadProvider();

        provider.uploadAttachment(filename, fileUpload, contentType,
                new ZendeskUnityCallback<UploadResponse>(gameObjectName, callbackId, "didUploadProviderUploadAttachment"));
    }

    //deleteAttachment provider method
    public void deleteUpload(final String gameObjectName, String callbackId, String uploadToken){
        com.zendesk.sdk.network.UploadProvider provider = new ZendeskUploadProvider();

        provider.deleteAttachment(uploadToken,
                new ZendeskUnityCallback<Response>(gameObjectName, callbackId, "didUploadProviderDeleteUpload"));
    }

}
