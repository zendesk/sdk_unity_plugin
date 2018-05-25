package com.zendesk.unity.providers;


import com.zendesk.sdk.model.request.Comment;
import com.zendesk.sdk.model.request.CommentsResponse;
import com.zendesk.sdk.model.request.CreateRequest;
import com.zendesk.sdk.model.request.EndUserComment;
import com.zendesk.sdk.model.request.Request;
import com.zendesk.sdk.model.request.RequestUpdates;
import com.zendesk.sdk.model.request.fields.TicketForm;
import com.zendesk.sdk.network.impl.ZendeskConfig;
import com.zendesk.unity.UnityComponent;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class RequestProvider extends UnityComponent {

    public static RequestProvider _instance;
    public static Object instance(){
        _instance = new RequestProvider();
        return _instance;
    }

    public void createRequest(final String gameObjectName, String callbackId, String subject,
                                                                   String description, String email, String[] tags,
                                                                   int tagsLength, String[] attachments, int attachmentsLength){
        ArrayList<String> attachmentsList = attachments != null ? new ArrayList<>(Arrays.asList(attachments)) : null;
        ArrayList<String> tagsList = tags != null ? new ArrayList<>(Arrays.asList(tags)) : null;

        CreateRequest createRequest = new CreateRequest();
        createRequest.setEmail(email);
        createRequest.setSubject(subject);
        createRequest.setDescription(description);
        createRequest.setTags(tagsList);
        createRequest.setAttachments(attachmentsList);

        com.zendesk.sdk.network.RequestProvider provider = ZendeskConfig.INSTANCE.provider().requestProvider();
        provider.createRequest(createRequest,
                new ZendeskUnityCallback<CreateRequest>(gameObjectName, callbackId, "didRequestProviderCreateRequest"));
    }

    public void getAllRequests(final String gameObjectName, String callbackId){
        com.zendesk.sdk.network.RequestProvider provider = ZendeskConfig.INSTANCE.provider().requestProvider();
        provider.getAllRequests(
                new ZendeskUnityCallback<List<Request>>(gameObjectName, callbackId, "didRequestProviderGetAllRequests"));
    }

    public void getRequestsByStatus(final String gameObjectName, String callbackId, String status){
        com.zendesk.sdk.network.RequestProvider provider = ZendeskConfig.INSTANCE.provider().requestProvider();
        //Status is a comma separated list of status to filter the results by status
        provider.getRequests(status,
                new ZendeskUnityCallback<List<Request>>(gameObjectName, callbackId, "didRequestProviderGetAllRequestsByStatus"));
    }

    public void getCommentsWithRequestId(final String gameObjectName, String callbackId, String requestId){
        com.zendesk.sdk.network.RequestProvider provider = ZendeskConfig.INSTANCE.provider().requestProvider();
        provider.getComments(requestId,
                new ZendeskUnityCallback<CommentsResponse>(gameObjectName, callbackId, "didRequestProviderGetCommentsWithRequestId"));
    }

    public void getRequestWithId(final String gameObjectName, String callbackId, String requestId){
        com.zendesk.sdk.network.RequestProvider provider = ZendeskConfig.INSTANCE.provider().requestProvider();
        
        provider.getRequest(requestId,
                new ZendeskUnityCallback<Request>(gameObjectName, callbackId, "didRequestProviderGetRequestWithId"));
    }

    public void addComment(final String gameObjectName, String callbackId, String comment, String requestId){
        EndUserComment endUserComment = new EndUserComment();
        endUserComment.setValue(comment);

        com.zendesk.sdk.network.RequestProvider provider = ZendeskConfig.INSTANCE.provider().requestProvider();
        provider.addComment(requestId, endUserComment,
                new ZendeskUnityCallback<Comment>(gameObjectName, callbackId, "didRequestProviderAddComment"));
    }

    public void addCommentWithAttachments(final String gameObjectName, String callbackId, String comment,
                                                                String requestId, String[] attachments, int attachmentsLength){
        ArrayList<String> attachmentsList = new ArrayList<>(Arrays.asList(attachments));
        EndUserComment endUserComment = new EndUserComment();
        endUserComment.setAttachments(attachmentsList);
        endUserComment.setValue(comment);

        com.zendesk.sdk.network.RequestProvider provider = ZendeskConfig.INSTANCE.provider().requestProvider();
        provider.addComment(requestId, endUserComment,
                new ZendeskUnityCallback<Comment>(gameObjectName, callbackId, "didRequestProviderAddCommentWithAttachments"));
    }

    public void getTicketFormWithIds(final String gameObjectName, String callbackId, long[] ticketFormsIds, int formsCount){
        
        Long[] result = new Long[formsCount];
        for (int i = 0; i < formsCount; i++) {
            result[i] = Long.valueOf(ticketFormsIds[i]);
        }

        ArrayList<Long> ids = new ArrayList<>(Arrays.asList(result));

        com.zendesk.sdk.network.RequestProvider provider = ZendeskConfig.INSTANCE.provider().requestProvider();
        provider.getTicketFormsById(ids,
                        new ZendeskUnityCallback<List<TicketForm>>(gameObjectName, callbackId, "didRequestProviderGetTicketFormWithIds"));
    }
    
    public void getUpdatesForDevice(String gameObjectName, String callbackId) {
        ZendeskConfig.INSTANCE.provider().requestProvider().getUpdatesForDevice(new ZendeskUnityCallback<RequestUpdates>(gameObjectName, callbackId, "didRequestProviderGetUpdatesForDevice"));
    }
    
    public void markRequestAsRead(String requestId) {
        ZendeskConfig.INSTANCE.storage().requestStorage().markRequestAsRead(requestId);
    }

}
