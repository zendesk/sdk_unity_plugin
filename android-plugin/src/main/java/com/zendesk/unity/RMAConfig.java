package com.zendesk.unity;

import java.io.Serializable;

/**
 * RMAConfig object to store information that the user wants to include in RMA
 */
public class RMAConfig implements Serializable{
    public String[] tags;
    public String requestSubject;
    public Boolean[] dialogAction;
    public String additionalInfo;
    public Boolean showAlways;
    public Boolean showConfig;

    public RMAConfig(){
        tags = null;
        requestSubject = null;
        dialogAction = new Boolean[3];
        /**initialized as false so that the user sets what buttons to use
         * Each index represents a button
         * [0] = StoreButton
         * [1] = SendFeedbackButton
         * [2] = DontAskMeAgainButton
         */
        for(int i = 0; i < dialogAction.length; i ++){
            dialogAction[i] = true;
        }
        additionalInfo = null;
        showAlways = false;
        showConfig = false;
    }

}
