//
//  ZendeskProviderSettings.h
//  ZEN
//

#import "ZendeskCore.h"

#pragma mark - ZDKSettingsProvider

void _zendeskSettingsProviderGetSettings(char * gameObjectName, char * callbackId) {
    ZDKSettingsProvider *provider = [ZDKSettingsProvider new];
    ZDKDefCallback(ZDKSettings*, [ZendeskJSON ZDKSettingsToJSON:result], "didSettingsProviderGetSettings")
    [provider getSdkSettingsWithCallback:callback];
}

void _zendeskSettingsProviderGetSettingsWithLocale(char * gameObjectName, char * callbackId, char * locale) {
    ZDKSettingsProvider *provider = [ZDKSettingsProvider new];
    ZDKDefCallback(ZDKSettings*, [ZendeskJSON ZDKSettingsToJSON:result], "didSettingsProviderGetSettingsWithLocale")
    [provider getSdkSettingsWithLocale:GetStringParam(locale)
                           andCallback:callback];
}
