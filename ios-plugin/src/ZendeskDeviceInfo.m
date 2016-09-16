//
//  ZendeskDeviceInfo.m
//  ZEN
//

#import "ZendeskCore.h"

#pragma mark - ZDKDeviceInfo

char* _zendeskDeviceType() {
    return ZDKMakeStringCopy([ZDKDeviceInfo deviceType].UTF8String);
}

double _zendeskTotalDeviceMemory() {
    return [ZDKDeviceInfo totalDeviceMemory];
}

double _zendeskFreeDiskspace() {
    return [ZDKDeviceInfo freeDiskspace];
}

double _zendeskTotalDiskspace() {
    return [ZDKDeviceInfo totalDiskspace];
}

float _zendeskBatteryLevel() {
    return [ZDKDeviceInfo batteryLevel];
}

char* _zendeskRegion() {
    return ZDKMakeStringCopy([ZDKDeviceInfo region].UTF8String);
}

char* _zendeskLanguage() {
    return ZDKMakeStringCopy([ZDKDeviceInfo language].UTF8String);
}

char* _zendeskDeviceInfoDictionary() {
    // ZDKDeviceInfo:deviceInfoDictionary calls may cause crash.
    NSDictionary *result = [ZDKDeviceInfo deviceInfoDictionary];
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:result options:0 error:NULL];
    NSString *json = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    return ZDKMakeStringCopy(json.UTF8String);
}
