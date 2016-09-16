package com.zendesk.unity;

import android.content.Intent;
import android.content.res.Resources;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.MotionEvent;

import com.unity3d.player.UnityPlayer;
import com.zendesk.logger.Logger;
import com.zendesk.sdk.feedback.ZendeskFeedbackConfiguration;

import com.zendesk.sdk.feedback.ZendeskFeedbackConnector;
import com.zendesk.sdk.network.SubmissionListener;
import com.zendesk.sdk.network.impl.ZendeskConfig;
import com.zendesk.sdk.rating.impl.RateMyAppDontAskAgainButton;
import com.zendesk.sdk.rating.impl.RateMyAppSendFeedbackButton;
import com.zendesk.sdk.rating.impl.RateMyAppStoreButton;
import com.zendesk.sdk.rating.ui.RateMyAppButtonContainer;
import com.zendesk.sdk.rating.ui.RateMyAppDialog;
import com.zendesk.service.ErrorResponse;

import java.util.Arrays;
import java.util.List;

/**
 * This Activity serves to allow RMA to show since RMA only shows in a Fragment Activity.
 * The Unity Activity is not a Fragment Activity which is why we got to start a new
 * Activity on top of the Unity. In this class there's a lot of check to ensure that
 * this Activity closes when RMA closes so it goes back to the Unity Activity.
 */
public class RateMyAppActivity extends AppCompatActivity {

    public static final String EXTRA_CONFIG_OBJECT = "ConfigObject";

    private static final String TAG = "RateMyAppActivity";
    private static final String IDENTIFIER_NAME = "activity_rate_my_app";
    private static final String IDENTIFIER_DEFTYPE = "layout";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Resources res = getApplicationContext().getResources();
        setContentView(res.getIdentifier(IDENTIFIER_NAME, IDENTIFIER_DEFTYPE, UnityPlayer.currentActivity.getPackageName()));

        //Lets get our config object back
        final RMAConfig config = getRmaConfig();

        RateMyAppDialog dialog = buildRateMyAppDialog(config);

        //This was determined by the user in c#
        if (config.showAlways){
            dialog.showAlways(this);
        }
        else {
            dialog.show(this);
        }
    }

    private RateMyAppDialog buildRateMyAppDialog(RMAConfig config) {
        //Feedbackbutton with additional information if present in the config object
        //if not they will be null
        RateMyAppSendFeedbackButton feedbackButton = buildSendFeedbackButton(config);

        //Listener so we know when to close the Activity
        feedbackButton.setFeedbackListener(feedbackListener);

        RateMyAppDontAskAgainButton dontAskAgainButton = new RateMyAppDontAskAgainButton(this);
        RateMyAppStoreButton storeButton = new RateMyAppStoreButton(this);

        //IDs so that we can figure out what button was pressed so we can close appropriately
        final int dontAskAgainButtonId = dontAskAgainButton.getId();
        final int storeButtonId = storeButton.getId();

        //This is so we can close the Activity when no longer needed
        RateMyAppDialog.Builder builder = new RateMyAppDialog.Builder(this).withSelectionListener(
                new RateMyAppButtonContainer.RateMyAppSelectionListener() {
                    @Override
                    public void selectionMade(int i) {
                        //Close if the buttons don't open new dialog
                        if(dontAskAgainButtonId == i || storeButtonId == i){
                            RateMyAppActivity.this.finish();
                        }
                    }
                });

        //If showConfig then customize the button
        if(config.showConfig){
            //the first boolean represents adding a store button
            if(config.dialogAction[0]){
                builder = builder.withButton(storeButton);
            }
            //Represents feedback button
            if(config.dialogAction[1]){
                builder = builder.withButton(feedbackButton);
            }
            //Represents the dontAskAgain button
            if(config.dialogAction[2]){
                builder = builder.withButton(dontAskAgainButton);
            }
        }
        //else build the generic Dialog which includes all three buttons
        else{
            builder = builder.withButton(storeButton).withButton(feedbackButton).
                    withButton(dontAskAgainButton);
        }
        return builder.build();
    }

    private RMAConfig getRmaConfig() {
        Intent i = getIntent();
        return (RMAConfig)i.getSerializableExtra(EXTRA_CONFIG_OBJECT);
    }

    private SubmissionListener feedbackListener = new SubmissionListener() {
        @Override
        public void onSubmissionStarted() {
        }

        @Override
        public void onSubmissionCompleted() {
            RateMyAppActivity.this.finish();
        }

        @Override
        public void onSubmissionCancel() {
            RateMyAppActivity.this.finish();
        }

        @Override
        public void onSubmissionError(ErrorResponse errorResponse) {
            Logger.e(TAG, errorResponse);
        }
    };

    private RateMyAppSendFeedbackButton buildSendFeedbackButton(final RMAConfig config) {
        //Feedbackbutton with additional information if present in the config object
        //if not they will be null

        ZendeskFeedbackConnector feedbackConnector = ZendeskFeedbackConnector.defaultConnector(
                this,
                new ZendeskFeedbackConfiguration() {
                    @Override
                    public List<String> getTags() {
                        return (config.tags == null) ? null : Arrays.asList(config.tags);
                    }

                    @Override
                    public String getAdditionalInfo() {
                        return config.additionalInfo;
                    }

                    @Override
                    public String getRequestSubject() {
                        return config.requestSubject;
                    }
                },
                ZendeskConfig.INSTANCE.getMobileSettings().getContactZendeskTags());

        return new RateMyAppSendFeedbackButton(this, feedbackConnector);
    }

    @Override
    public boolean onTouchEvent(MotionEvent event) {
        this.finish();
        return true;
    }
}
