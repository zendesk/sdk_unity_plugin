//
//  ZDKBWJSONValueObject.h
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

#ifndef ZDKBWJSONValueObject_h
#define ZDKBWJSONValueObject_h

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

#pragma mark - ZDKBWJSONHasArrayProperties

/*!
 * The protocol indicates that your JSON value object has several NSArray properties.
 */
@protocol ZDKBWJSONHasArrayProperties <NSObject>

@required

/*!
 * If your JSON value object contains NSArray properties, this method should be implemented,
 * if not, the data contained in this array will not be matched properly
 * @param property Property name of the array
 * @return Type of data that contained in this property which should be an array
 */
- (Class)typeInProperty:(NSString *)property;

@end


#pragma mark - ZDKBWJSONHasIgnoredProperties

/*!
 * The protocol indicates that your JSON value object has several properties need to be ignored when matching with JSON data or JSON string.
 */
@protocol ZDKBWJSONHasIgnoredProperties <NSObject>

@required

/*!
 * In some cases, there will be certain properties which don't need to be extracted from json data.
 * Provide all property names that you want to ignore here.
 * @return An array that contains all property name you want to ignore
 */
+ (NSArray *)ignoredProperties;

@end


#pragma mark - ZDKBWJSONHasToAmendProperties

/*!
 * The protocol indicates that your JSON value object has several properties need to be amended after been matched up with JSON data or JSON string.
 */
@protocol ZDKBWJSONHasToAmendProperties <NSObject>

@required

/*!
 * You should never call this method directly. This method will be invoked automatically after all
 * properties have been matched. If you have to prune or amend some values of your json object
 * according to the business logic, place the code here
 */
- (void)matchDidFinish;

@end


#pragma mark - ZDKBWJSONValueObject (Deprecated)

/*!
 * Deprecated. We strongly recommend that all of your data models should be descendants of NSObject,
 * and conform to this protocol as well
 *
 * This protocol have been deprecated since version 1.1.0 and will be removed in a later release.
 * Please use ZDKBWJSONHasArrayProperties, ZDKBWJSONHasIgnoredProperties and ZDKBWJSONHasToAmendProperties instead.
 */
__deprecated_msg("Use ZDKBWJSONHasArrayProperties, ZDKBWJSONHasIgnoredProperties and ZDKBWJSONHasToAmendProperties instead.")
@protocol ZDKBWJSONValueObject <NSObject>

@optional

/*!
 * If your data model contains a property of NSArray, this method should be implemented,
 * if not, the data contained in this array will not be matched properly
 * @param property Property name of the array
 * @return Type of data that contained in this property which should be an array
 */
- (Class)typeInProperty:(NSString *)property;

/*!
 * You should never call this method directly. This method will be invoked automatically after all 
 * properties have been matched. If you have to prune or amend some values of your json object
 * according to the business logic, place the code here
 */
- (void)matchDidFinish;

/*!
 * In some cases, there will be certain properties which don't need to be extracted from json data.
 * Provide all property names that you want to ignore here.
 * @return An array that contains all property name you want to ignore
 */
+ (NSArray *)ignoredProperties;

@end

NS_ASSUME_NONNULL_END

#endif /* ZDKBWJSONValueObject_h */
