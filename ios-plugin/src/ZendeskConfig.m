//
//  ZendeskConfig.m
//  ZEN
//

#import "ZendeskCore.h"

#pragma mark - ZDKConfig

// should be called before _zendeskConfigInitialize
void _zendeskConfigSetUserLocale(char *locale) {
    [ZDKConfig instance].userLocale = GetStringParam(locale);
}

void _zendeskConfigSetCoppaEnabled(BOOL coppa) {
    [ZDKConfig instance].coppaEnabled = coppa;
}

void _zendeskConfigAuthenticateAnonymousIdentity(char * name, char * email, char * externalId) {
    ZDKAnonymousIdentity *identity = [ZDKAnonymousIdentity new];
    identity.name = GetStringParam(name);
    identity.email = GetStringParam(email);
    identity.externalId = GetStringParam(externalId);
    [ZDKConfig instance].userIdentity = identity;
}

void _zendeskConfigAuthenticateJwtUserIdentity(char * jwtUserIdentityString) {
    ZDKJwtIdentity * jwtUserIdentity = [[ZDKJwtIdentity alloc] initWithJwtUserIdentifier:GetStringParam(jwtUserIdentityString)];
    [ZDKConfig instance].userIdentity = jwtUserIdentity;
}

void _zendeskConfigSetContactConfiguration(char * rawTags[], int tagsLength, char * rawAdditionalInfo, char * rawRequestSubject) {
    NSString *additionalInfo = GetStringParam(rawAdditionalInfo);
    NSString *requestSubject = GetStringParam(rawRequestSubject);

    NSMutableArray * tags = @[].mutableCopy;
    for (int i = 0; i < tagsLength; i++) {
        [tags addObject:GetStringParam(rawTags[i])];
    }

    [ZDKRequests configure:^(ZDKAccount *account, ZDKRequestCreationConfig *config) {
        config.tags = tags;
        config.additionalRequestInfo = additionalInfo;
        config.subject = requestSubject;
    }];
}

void _zendeskConfigSetCustomFields(char * customFieldData) {
    NSDictionary *fields = [NSJSONSerialization JSONObjectWithData:[GetStringParam(customFieldData) dataUsingEncoding:NSUTF8StringEncoding] options:kNilOptions error:NULL];

    NSMutableArray *customFields = [[NSMutableArray alloc] initWithCapacity:fields.allKeys.count];
    [fields enumerateKeysAndObjectsUsingBlock:^(id  _Nonnull key, id  _Nonnull obj, BOOL * _Nonnull stop) {
        ZDKCustomField *customField = [[ZDKCustomField alloc] initWithFieldId:key andValue:obj];
        [customFields addObject: customField];
    }];

    [ZDKConfig instance].customTicketFields = customFields;
}

const char * _zendeskConfigGetCustomFields() {
    NSArray *customFields = [ZDKConfig instance].customTicketFields;

    NSMutableDictionary *fieldsMap = [[NSMutableDictionary alloc] initWithCapacity:customFields.count];

    [customFields enumerateObjectsUsingBlock:^(id  _Nonnull obj, NSUInteger idx, BOOL * _Nonnull stop) {
        ZDKCustomField *field = obj;
        [fieldsMap setObject:field.value forKey:field.fieldId];

    }];

    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:fieldsMap options:kNilOptions error:NULL];
    NSString *jsonFieldsString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];

    return ZDKMakeStringCopy([jsonFieldsString UTF8String]);
}

void _zendeskConfigReload() {
    [[ZDKConfig instance] reload];
}
