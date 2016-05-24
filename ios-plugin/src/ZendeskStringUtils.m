//
//  ZendeskStringUtils.m
//  ZEN
//

#import "ZendeskCore.h"

#pragma mark - ZDKStringUtil

char* _zendeskCsvStringFromArray(char *charArray[], int length) {
    NSMutableArray * newStrings = @[].mutableCopy;
    for (int i = 0; i < length; i++) {
        [newStrings addObject:GetStringParam(charArray[i])];
    }
    return ZDKMakeStringCopy([ZDKStringUtil csvStringFromArray:[newStrings copy]].UTF8String);
}
