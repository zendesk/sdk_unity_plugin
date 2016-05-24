//
//  ZendeskLogger.m
//  ZEN
//

#import "ZendeskCore.h"

#pragma mark - ZDKLogger

void _zendeskLoggerSetLogLevel(int level) {
    [ZDKLogger setLogLevel:level];
}

void _zendeskLoggerEnableLogger(BOOL enabled) {
    [ZDKLogger enable:enabled];
}
