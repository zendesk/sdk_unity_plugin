//
//  ZendeskApperanceBinding.m
//  ZEN
//

#import "ZendeskCore.h"

#pragma mark - Appearance

ZDKTheme *baseTheme;

void _zendeskThemeStartWithBaseTheme() {
    baseTheme = [ZDKTheme baseTheme];
}

void _zendeskThemeApplyTheme() {
    if (baseTheme) {
        [baseTheme apply];
    }
}

void _zendeskThemeSetPrimaryTextColor(float red, float green, float blue, float alpha) {
    if (baseTheme) {
        UIColor *color = [UIColor colorWithRed:red green:green blue:blue alpha:alpha];
        baseTheme.primaryTextColor = color;
    }
}

void _zendeskThemeSetSecondaryTextColor(float red, float green, float blue, float alpha) {
    if (baseTheme) {
        UIColor *color = [UIColor colorWithRed:red green:green blue:blue alpha:alpha];
        baseTheme.secondaryTextColor = color;
    }
}

void _zendeskThemeSetPrimaryBackgroundColor(float red, float green, float blue, float alpha) {
    if (baseTheme) {
        UIColor *color = [UIColor colorWithRed:red green:green blue:blue alpha:alpha];
        baseTheme.primaryBackgroundColor = color;
    }
}

void _zendeskThemeSetSecondaryBackgroundColor(float red, float green, float blue, float alpha) {
    if (baseTheme) {
        UIColor *color = [UIColor colorWithRed:red green:green blue:blue alpha:alpha];
        baseTheme.secondaryBackgroundColor = color;
    }
}

void _zendeskThemeSetEmptyBackgroundColor(float red, float green, float blue, float alpha) {
    if (baseTheme) {
        UIColor *color = [UIColor colorWithRed:red green:green blue:blue alpha:alpha];
        baseTheme.emptyBackgroundColor = color;
    }
}

void _zendeskThemeSetMetaTextColor(float red, float green, float blue, float alpha) {
    if (baseTheme) {
        UIColor *color = [UIColor colorWithRed:red green:green blue:blue alpha:alpha];
        baseTheme.metaTextColor = color;
    }
}

void _zendeskThemeSetSeparatorColor(float red, float green, float blue, float alpha) {
    if (baseTheme) {
        UIColor *color = [UIColor colorWithRed:red green:green blue:blue alpha:alpha];
        baseTheme.separatorColor = color;
    }
}

void _zendeskThemeSetInputFieldColor(float red, float green, float blue, float alpha) {
    if (baseTheme) {
        UIColor *color = [UIColor colorWithRed:red green:green blue:blue alpha:alpha];
        baseTheme.inputFieldTextColor = color;
    }
}

void _zendeskThemeSetInputFieldBackgroundColor(float red, float green, float blue, float alpha) {
    if (baseTheme) {
        UIColor *color = [UIColor colorWithRed:red green:green blue:blue alpha:alpha];
        baseTheme.inputFieldBackgroundColor = color;
    }
}

void _zendeskThemeSetFontName(char* fontName) {
    if (baseTheme && fontName) {
        baseTheme.fontName = GetStringParam(fontName);
    }
}

void _zendeskThemeSetBoldFontName(char* boldFontName) {
    if (baseTheme && boldFontName) {
        baseTheme.boldFontName = GetStringParam(boldFontName);
    }
}