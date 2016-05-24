//
//  ZDKBWJSONMatcher.m
//  BWJSONMatcher
//
//  Created by wangruicheng on 10/14/15.
//  Copyright © 2015 Burrows.Wang ( https://github.com/BurrowsWang ).
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#import "ZDKBWJSONMatcher.h"

#import <objc/runtime.h>

static NSString* const kTypeNSDictionary        = @"NSDictionary";
static NSString* const kTypeNSArray             = @"NSArray";
static NSString* const kTypeNSString            = @"NSString";
static NSString* const kTypeNSDecimalNumber     = @"NSDecimalNumber";
static NSString* const kTypeNSNumber            = @"NSNumber";
static NSString* const kTypeNSDate              = @"NSDate";
static NSString* const kTypeNSData              = @"NSData";
static NSString* const kTypeNSValue             = @"NSValue";

static NSString* const kGetTypeInArray          = @"typeInProperty:";
static NSString* const kGetTypeInArrayLower     = @"typeinproperty:";   // in case of spelling mistake
static NSString* const kGetTypeInArrayUpper     = @"TYPEINPROPERTY:";   // in case of spelling mistake
static NSString* const kMatchDidFinish          = @"matchDidFinish";

#pragma mark - Global Methods

/*!
 * Check if the object passed in is nil or kind of NSNull
 * @param value The object that you want to check
 * @return      YES if the value is nil or NSNull
 */
static BOOL isNullValue(id value) {
    return !value || [value isEqual:[NSNull null]];
}

/*!
 * Convert Any Object to NSNumber
 * @param value The object that you want to convert
 * @return      NSNumber that converted from value
 */
static NSNumber* numberFromValue(id value) {
    NSNumberFormatter *numberFormatter = [[NSNumberFormatter alloc] init];
    [numberFormatter setNumberStyle:NSNumberFormatterDecimalStyle];
    
    return [numberFormatter numberFromString:[value description]];
}

id ZDKBWJSONObjectByRemovingKeysWithNullValues(id json, NSJSONReadingOptions options) {
    if ([json isKindOfClass:[NSArray class]]) {
        NSMutableArray *mutableArray = [NSMutableArray arrayWithCapacity:[(NSArray *)json count]];
        for (id value in (NSArray *)json) {
            [mutableArray addObject:ZDKBWJSONObjectByRemovingKeysWithNullValues(value, options)];
        }
        
        return (options & NSJSONReadingMutableContainers) ? mutableArray : [NSArray arrayWithArray:mutableArray];
    } else if ([json isKindOfClass:[NSDictionary class]]) {
        NSMutableDictionary *mutableDictionary = [NSMutableDictionary dictionaryWithDictionary:json];
        for (id<NSCopying> key in [(NSDictionary *)json allKeys]) {
            id value = [(NSDictionary *)json objectForKey:key];
            
            if (isNullValue(value)) {
                [mutableDictionary removeObjectForKey:key];
            } else if ([value isKindOfClass:[NSArray class]] || [value isKindOfClass:[NSDictionary class]]) {
                [mutableDictionary setObject:ZDKBWJSONObjectByRemovingKeysWithNullValues(value, options) forKey:key];
            }
        }
        
        return (options & NSJSONReadingMutableContainers) ? mutableDictionary : [NSDictionary dictionaryWithDictionary:mutableDictionary];
    }
    
    return json;
}


#pragma mark - ZDKBWJSONMatcher

@implementation ZDKBWJSONMatcher

+ (nullable id)matchJSON:(id)json withClass:(__unsafe_unretained Class)classType {
    // make sure the parameters are all safe
    if (classType == nil || (![json isKindOfClass:[NSDictionary class]] && ![json isKindOfClass:[NSArray class]])) return nil;
    
    if (classType == [NSDictionary class] || classType == [NSArray class]) {
        // if you want a data model of NSDictionary or NSArray, here you go
        return json;
    } else if ([json isKindOfClass:[NSArray class]]) {
        NSArray *jsonArray = (NSArray *)json;
        
        // match json array with class type of inside data
        return [self matchJSONArray:jsonArray withClass:classType];
    } else {
        // create a data model of the given class type
        id result = [[classType alloc] init];
        
        // iteratively match all properties of this class and its super class
        while (classType != nil && classType != [NSObject class]) {
            [self matchJSON:json withObject:result ofClass:classType];
            
            classType = class_getSuperclass(classType);
        }
        
        // tell the model that matching has been finished
        SEL matchDidFinish = NSSelectorFromString(kMatchDidFinish);
        if (result && [result respondsToSelector:matchDidFinish]) {
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Warc-performSelector-leaks"
            [result performSelector:matchDidFinish];
#pragma clang diagnostic pop
        }
        
        return result;
    }
}

+ (nullable id)matchJSONString:(NSString *)jsonString withClass:(__unsafe_unretained Class)classType {
    // make sure the json string is valid and convertible
    if (!jsonString || classType == nil) return nil;
    
    NSError *error = nil;
    NSData *jsonData = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
    // convert json string to json object in Objective-c first
    id jsonObj = [NSJSONSerialization JSONObjectWithData:jsonData options:0 error:&error];
    
    if (error) {
        NSLog(@"%@", [error localizedDescription]);
        
        // unconvertible json string
        return nil;
    } else {
        return [self matchJSON:jsonObj withClass:classType];
    }
}

/*!
 * Convert a json array to a array that contains data model objects
 * @param jsonArray The json array
 * @param classType To which type you want to convert the data in json array
 * @return A array that contains converted data models of the given class
 */
+ (NSArray *)matchJSONArray:(NSArray *)jsonArray withClass:(__unsafe_unretained Class)classType {
    if ([jsonArray count] > 0) {
        NSMutableArray *result = [NSMutableArray arrayWithCapacity:[jsonArray count]];
        
        for (id jsonObject in jsonArray) {
            if ([jsonObject isKindOfClass:[NSString class]]
                || [jsonObject isKindOfClass:[NSNumber class]]
                || [jsonObject isKindOfClass:[NSData class]]
                || [jsonObject isKindOfClass:[NSDate class]]) {
                [result addObject:jsonObject];
            } else {
                id parsedObject = [self matchJSON:jsonObject withClass:classType];
                
                if (!isNullValue(parsedObject)) {
                    [result addObject:parsedObject];
                }
            }
        }
        
        return [NSArray arrayWithArray:result];
    }
    
    return nil;
}

+ (void)matchJSON:(id)json withObject:(id)result ofClass:(__unsafe_unretained Class)classType {
    unsigned int propertyCount = 0;
    objc_property_t *properties = class_copyPropertyList(classType, &propertyCount);
    NSCharacterSet *quotes = [NSCharacterSet characterSetWithCharactersInString:@"@\""];
    
    // check if there are some properties of this classType need to be ignored
    NSArray *ignoredProperties = nil;
    SEL ignoredPropertiesSelector = NSSelectorFromString(@"ignoredProperties");
    if ([classType respondsToSelector:ignoredPropertiesSelector]) {
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Warc-performSelector-leaks"
        NSArray *propertyArray = [classType performSelector:ignoredPropertiesSelector];
        
        if ([propertyArray isKindOfClass:[NSArray class]] && [propertyArray count] > 0) {
            ignoredProperties = propertyArray;
        }
#pragma clang diagnostic pop
    }
    
    // iterate all the properties defined in classType
    for (unsigned int i = 0; i < propertyCount; i++) {
        @autoreleasepool {
            objc_property_t property = properties[i];
            
            // check if this property is defined as READONLY
            char *isReadOnly = property_copyAttributeValue(property, "R");
            if (isReadOnly) {
                free(isReadOnly);
                
                // if the property is READONLY, ignore it, do not need to parse this property
                continue;
            }
            
            // check if the json object has this property or not
            NSString *propertyName = [NSString stringWithCString:property_getName(property) encoding:NSUTF8StringEncoding];
            id rawValue = [json valueForKey:propertyName];
            if (isNullValue(rawValue)) {
                continue;
            }
            
            // check if we should ignore this property
            if (ignoredProperties && [ignoredProperties containsObject:propertyName]) {
                continue;
            }
            
            // get the type of this property
            char *type = property_copyAttributeValue(property, "T");
            NSString *propertyType = [NSString stringWithCString:type encoding:NSUTF8StringEncoding];
            if (type) {
                free(type);
            }
            
            // trim all the redundant chars in type
            NSString *trimmedType = [propertyType stringByTrimmingCharactersInSet:quotes];
            
            
            /* check the type of this property and extract the value from json object,
             * then set value for the property according to the type
             */
            if ([trimmedType isEqualToString:kTypeNSDictionary]) {        // NSDictionary
                if ([rawValue isKindOfClass:[NSDictionary class]]) {
                    [result setValue:rawValue forKey:propertyName];
                }
            } else if ([trimmedType isEqualToString:kTypeNSArray]) {    // NSArray
                if ([rawValue isKindOfClass:[NSArray class]]) {
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Warc-performSelector-leaks"
                    SEL typeSelector = NSSelectorFromString(kGetTypeInArray);
                    SEL lowerSelector = NSSelectorFromString(kGetTypeInArrayLower);
                    SEL upperSelector = NSSelectorFromString(kGetTypeInArrayUpper);
                    Class innerType = nil;
                    
                    if ([result respondsToSelector:typeSelector]) {
                        innerType = (Class)[result performSelector:typeSelector withObject:propertyName];
                    } else if ([result respondsToSelector:lowerSelector]) {
                        innerType = (Class)[result performSelector:lowerSelector withObject:propertyName];
                    } else if ([result respondsToSelector:upperSelector]) {
                        innerType = (Class)[result performSelector:upperSelector withObject:propertyName];
                    }
                    
                    if (innerType) {
                        /* if the type of this property is NSArray, we get the class type in this array from method typeInProperty:
                         * which should have had been defined in the parameter classType
                         */
                        NSArray *arrayValue = [self matchJSON:rawValue withClass:innerType];
                        [result setValue:arrayValue forKey:propertyName];
                    }
#pragma clang diagnostic pop
                }
            } else if ([trimmedType isEqualToString:kTypeNSString]) {    // NSString
                NSString *stringValue = [[rawValue description] copy];
                [result setValue:stringValue forKey:propertyName];
            } else if ([trimmedType isEqualToString:kTypeNSDecimalNumber]
                       || [trimmedType isEqualToString:kTypeNSNumber]) {    // NSNumber or NSDecimalNumber
                NSDecimalNumber *numberValue = [NSDecimalNumber decimalNumberWithString:[rawValue description]];
                [result setValue:numberValue forKey:propertyName];
            } else if ([trimmedType isEqualToString:@"i"]
                       || [trimmedType isEqualToString:@"s"]
                       || [trimmedType isEqualToString:@"I"]
                       || [trimmedType isEqualToString:@"S"]) { // int or unsigned int
                NSInteger intValue = 0;
                
                if ([rawValue respondsToSelector:@selector(integerValue)]) {
                    intValue = [rawValue integerValue];
                } else {
                    NSNumber *intNumber = numberFromValue(rawValue);
                    intValue = [intNumber integerValue];
                }
                
                if ([trimmedType isEqualToString:@"I"]
                    || [trimmedType isEqualToString:@"S"]) {    // unsigned int
                    intValue = ABS(intValue);
                }
                
                [result setValue:@(intValue) forKey:propertyName];
            } else if ([trimmedType isEqualToString:@"l"]
                       || [trimmedType isEqualToString:@"L"]) {    // long or unsigned long
                long longValue = 0;
                
                if ([rawValue respondsToSelector:@selector(longValue)]) {
                    longValue = [rawValue longValue];
                } else {
                    NSNumber *longNumber = numberFromValue(rawValue);
                    longValue = [longNumber longValue];
                }
                
                if ([trimmedType isEqualToString:@"L"]) {   // unsigned long
                    longValue = labs(longValue);
                }
                
                [result setValue:@(longValue) forKey:propertyName];
            } else if ([trimmedType isEqualToString:@"q"]
                       || [trimmedType isEqualToString:@"Q"]) { // long long or unsigned long long
                long long longLongValue = 0;
                
                if ([rawValue respondsToSelector:@selector(longLongValue)]) {
                    longLongValue = [rawValue longLongValue];
                } else {
                    NSNumber *longLongNumber = numberFromValue(rawValue);
                    longLongValue = [longLongNumber longLongValue];
                }
                
                if ([trimmedType isEqualToString:@"Q"]) {
                    longLongValue = llabs(longLongValue);    // unsigned long long
                }
                
                [result setValue:@(longLongValue) forKey:propertyName];
            } else if ([trimmedType isEqualToString:@"f"]) {    // float
                float floatValue = 0.0f;
                
                if ([rawValue respondsToSelector:@selector(floatValue)]) {
                    floatValue = [rawValue floatValue];
                } else {
                    NSNumber *floatNumber = numberFromValue(rawValue);
                    floatValue = [floatNumber floatValue];
                }
                
                [result setValue:@(floatValue) forKey:propertyName];
            } else if ([trimmedType isEqualToString:@"d"]) {    // double
                double doubleValue = 0.0f;
                
                if ([rawValue respondsToSelector:@selector(doubleValue)]) {
                    doubleValue = [rawValue doubleValue];
                } else {
                    NSNumber *doubleNumber = numberFromValue(rawValue);
                    doubleValue = [doubleNumber doubleValue];
                }
                
                [result setValue:@(doubleValue) forKey:propertyName];
            } else if ([trimmedType isEqualToString:@"B"]
                       || [trimmedType isEqualToString:@"c"]
                       || [trimmedType isEqualToString:@"C"]) {    // boolean
                BOOL boolValue = NO;
                
                if ([rawValue isEqual:@"true"] || [rawValue isEqual:@"TRUE"] || [rawValue isEqual:@"YES"]) {
                    boolValue = YES;
                } else if ([rawValue respondsToSelector:@selector(boolValue)]) {
                    boolValue = [rawValue boolValue];
                } else {
                    NSNumber *boolNumber = numberFromValue(rawValue);
                    boolValue = [boolNumber boolValue];
                }
                
                [result setValue:@(boolValue) forKey:propertyName];
            } else if ([trimmedType isEqualToString:@""]) {    // id
                [result setValue:rawValue forKey:propertyName];
            } else if ([trimmedType isEqualToString:@"v"]
                       || [trimmedType isEqualToString:@"*"]
                       || [trimmedType isEqualToString:@"#"]
                       || [trimmedType isEqualToString:@":"]
                       || [trimmedType isEqualToString:@"?"]
                       || [trimmedType isEqualToString:kTypeNSDate]
                       || [trimmedType isEqualToString:kTypeNSData]
                       || [trimmedType isEqualToString:kTypeNSValue]
                       || [trimmedType hasPrefix:@"{"]
                       || [trimmedType hasPrefix:@"("]
                       || [trimmedType hasPrefix:@"b"]
                       || [trimmedType hasPrefix:@"^"]) {
                /*
                 * all of these types will be ignored, such as NSDate, NSData, NSValue etc.
                 * so please do not define a property as such types to match something in a json object
                 */
            } else {    // Other customized type
                id parsedObject = [self matchJSON:rawValue withClass:NSClassFromString(trimmedType)];
                
                if (parsedObject) {
                    [result setValue:parsedObject forKey:propertyName];
                }
            }
        }
    }
    
    free(properties);
}

+ (nullable id)convertObjectToJSON:(id)object {
    if (isNullValue(object)) return nil;
    
    if ([object isKindOfClass:[NSString class]]
        || [object isKindOfClass:[NSNumber class]]
        || [object isKindOfClass:[NSData class]]
        || [object isKindOfClass:[NSDate class]]) {
        return object;
    } else if ([object isKindOfClass:[NSDictionary class]]) {
        NSDictionary *dictionary = (NSDictionary *)object;
        NSArray *allKeys = [dictionary allKeys];
        NSMutableDictionary *result = [NSMutableDictionary dictionaryWithCapacity:[allKeys count]];
        
        for (id<NSCopying> key in allKeys) {
            id value = [dictionary objectForKey:key];
            
            id parsedJson = [self convertObjectToJSON:value];
            if (!isNullValue(parsedJson)) {
                [result setObject:parsedJson forKey:key];
            }
        }
        
        return [NSDictionary dictionaryWithDictionary:result];
    } else if ([object isKindOfClass:[NSArray class]]) {
        NSMutableArray *result = [NSMutableArray array];
        NSArray *source = (NSArray *)object;
        
        [source enumerateObjectsUsingBlock:^(id obj, NSUInteger idx, BOOL *stop) {
            id parsedJson = [self convertObjectToJSON:obj];
            
            if (!isNullValue(parsedJson)) {
                [result addObject:parsedJson];
            }
        }];
        
        return [NSArray arrayWithArray:result];
    } else {
        NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
        Class classType = object_getClass(object);
        
        while (classType != nil && classType != [NSObject class]) {
            [self convertObject:object toDictionary:dictionary withClass:classType];
            
            classType = class_getSuperclass(classType);
        }
        
        return [NSDictionary dictionaryWithDictionary:dictionary];
    }
}

+ (nullable id)convertObjectToJSONString:(id)object {
    // convert the object to an json object in Objective-c first
    id jsonObj = [self convertObjectToJSON:object];
    if (!jsonObj) {
        return nil;
    }
    
    NSError *error = nil;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:jsonObj options:0 error:&error];
    
    if (error) {
        NSLog(@"%@", [error localizedDescription]);
        
        return @"";
    } else {
        // return a string with encoding utf-8
        return [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    }
}

+ (void)convertObject:(id)object toDictionary:(NSMutableDictionary *)dictionary withClass:(Class)classType{
    unsigned int propertyCount = 0;
    objc_property_t *properties = class_copyPropertyList(classType, &propertyCount);
    NSCharacterSet *quotes = [NSCharacterSet characterSetWithCharactersInString:@"@\""];
    
    // check if there are some properties of this classType need to be ignored
    NSArray *ignoredProperties = nil;
    SEL ignoredPropertiesSelector = NSSelectorFromString(@"ignoredProperties");
    if ([classType respondsToSelector:ignoredPropertiesSelector]) {
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Warc-performSelector-leaks"
        NSArray *propertyArray = [classType performSelector:ignoredPropertiesSelector];
        
        if ([propertyArray isKindOfClass:[NSArray class]] && [propertyArray count] > 0) {
            ignoredProperties = propertyArray;
        }
#pragma clang diagnostic pop
    }
    
    // iterate all the properties of parameter object
    for (unsigned int i = 0; i < propertyCount; i++) {
        @autoreleasepool {
            objc_property_t property = properties[i];
            
            // check if this property is defined as READONLY
            char *isReadOnly = property_copyAttributeValue(property, "R");
            if (isReadOnly) {
                free(isReadOnly);
                continue;
            }
            
            // get the name and value of this property
            NSString *propertyName = [NSString stringWithCString:property_getName(property) encoding:NSUTF8StringEncoding];
            id rawValue = [object valueForKey:propertyName];
            if (isNullValue(rawValue)) {
                continue;
            }
            
            // check if we should ignore this property
            if (ignoredProperties && [ignoredProperties containsObject:propertyName]) {
                continue;
            }
            
            // get the type of this property
            char *type = property_copyAttributeValue(property, "T");
            NSString *propertyType = [NSString stringWithCString:type encoding:NSUTF8StringEncoding];
            if (type) {
                free(type);
            }
            
            // trim all the redundant chars in type
            NSString *trimmedType = [propertyType stringByTrimmingCharactersInSet:quotes];
            
            if ([trimmedType isEqualToString:kTypeNSDictionary]
                || [trimmedType isEqualToString:kTypeNSString]
                || [trimmedType isEqualToString:kTypeNSDecimalNumber]
                || [trimmedType isEqualToString:kTypeNSNumber]
                || [trimmedType isEqualToString:@"i"]
                || [trimmedType isEqualToString:@"s"]
                || [trimmedType isEqualToString:@"l"]
                || [trimmedType isEqualToString:@"q"]
                || [trimmedType isEqualToString:@"I"]
                || [trimmedType isEqualToString:@"S"]
                || [trimmedType isEqualToString:@"L"]
                || [trimmedType isEqualToString:@"Q"]
                || [trimmedType isEqualToString:@"f"]
                || [trimmedType isEqualToString:@"d"]
                || [trimmedType isEqualToString:@"B"]
                || [trimmedType isEqualToString:@"c"]
                || [trimmedType isEqualToString:@"C"]) {
                // all the basic types of objective-c will be assigned directly
                [dictionary setValue:rawValue forKey:propertyName];
            } else if ([trimmedType isEqualToString:@"v"]
                       || [trimmedType isEqualToString:@"*"]
                       || [trimmedType isEqualToString:@"#"]
                       || [trimmedType isEqualToString:@":"]
                       || [trimmedType isEqualToString:@"?"]
                       || [trimmedType isEqualToString:kTypeNSDate]
                       || [trimmedType isEqualToString:kTypeNSData]
                       || [trimmedType isEqualToString:kTypeNSValue]
                       || [trimmedType hasPrefix:@"{"]
                       || [trimmedType hasPrefix:@"("]
                       || [trimmedType hasPrefix:@"b"]
                       || [trimmedType hasPrefix:@"^"]) {
                /*
                 * all of these types will be ignored, such as NSDate, NSData, NSValue etc.
                 * so please do not define a property as such types of a data model
                 */
            } else {
                // other customized class type
                id jsonObject = [self convertObjectToJSON:rawValue];
                
                if (!isNullValue(jsonObject)) {
                    [dictionary setValue:jsonObject forKey:propertyName];
                }
            }
        }
    }
    
    free(properties);
}

@end
